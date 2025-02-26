using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumSourceGenerator;

[Generator]
public class ReadonlyStructGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Find all record structs
        IncrementalValuesProvider<INamedTypeSymbol?> recordStructs = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => IsRecordStruct(node),
                transform: static (context, _) => GetSemanticTargetForGeneration(context))
            .Where(static symbol => symbol is not null);

        // Combine all record structs into a single collection
        IncrementalValueProvider<(Compilation Left, System.Collections.Immutable.ImmutableArray<INamedTypeSymbol?> Right)> compilationAndStructs = context.CompilationProvider
            .Combine(recordStructs.Collect());

        // Generate source code
        context.RegisterSourceOutput(compilationAndStructs, static (context, source) =>
        {
            (Compilation compilation, System.Collections.Immutable.ImmutableArray<INamedTypeSymbol> structs) = source;

            foreach (INamedTypeSymbol? recordStruct in structs.Distinct())
            {
                if (recordStruct is not INamedTypeSymbol symbol) continue;

                // Ensure it implements INamed
                if (!symbol.Interfaces.Any(i => i.Name == "INamed")) continue;

                // Generate the readonly struct
                string namespaceName = symbol.ContainingNamespace.ToDisplayString();
                string structName = $"G{symbol.Name}";
                IEnumerable<(string, string Name)> properties = symbol
                    .GetMembers()
                    .OfType<IPropertySymbol>()
                    .Select(p => (p.Type.ToDisplayString(), p.Name));

                string sourceCode = GenerateReadonlyStructCode(namespaceName, structName, properties);

                // Add the generated source
                context.AddSource($"{structName}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
            }
        });
    }

    private static bool IsRecordStruct(SyntaxNode node) 
        => node is RecordDeclarationSyntax recordDecl &&
               recordDecl.IsKind(SyntaxKind.RecordStructDeclaration);

    private static INamedTypeSymbol? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        if (context.Node is not RecordDeclarationSyntax recordDecl) return null;

        // Get the declared symbol and ensure it's a record struct
        SemanticModel model = context.SemanticModel;
        INamedTypeSymbol? symbol = model.GetDeclaredSymbol(recordDecl);

        return symbol is INamedTypeSymbol namedSymbol && namedSymbol.IsValueType ? namedSymbol : null;
    }

    private static string GenerateReadonlyStructCode(
        string namespaceName,
        string structName,
        IEnumerable<(string Type, string Name)> properties)
    {
        string propertyDeclarations = string.Join("\n        ", properties.Select(p =>
            $"public {p.Type} {p.Name} {{ get; }}"));

        string constructorParameters = string.Join(", ", properties.Select(p =>
            $"{p.Type} {char.ToLower(p.Name[0])}{p.Name.Substring(1)}"));

        string constructorAssignments = string.Join("\n            ", properties.Select(p =>
            $"{p.Name} = {char.ToLower(p.Name[0])}{p.Name.Substring(1)};"));

        const string equalityLogic = "Name";
        return $@" using ConsoleHero.Generator;
namespace ConsoleHero
{{
    public readonly struct {structName} : INamed
    {{
        {propertyDeclarations}

        public {structName}({constructorParameters})
        {{
            {constructorAssignments}
        }}

        public override bool Equals(object? obj) =>
            obj is {structName} other && {equalityLogic} == other.{equalityLogic};

        public override int GetHashCode() => {equalityLogic}.GetHashCode();
    }}
}}
";
    }
}
