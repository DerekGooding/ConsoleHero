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
        var recordStructs = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => IsRecordStruct(node),
                transform: static (context, _) => GetSemanticTargetForGeneration(context))
            .Where(static symbol => symbol is not null);

        // Combine all record structs into a single collection
        var compilationAndStructs = context.CompilationProvider
            .Combine(recordStructs.Collect());

        // Generate source code
        context.RegisterSourceOutput(compilationAndStructs, static (context, source) =>
        {
            (var compilation, var structs) = source;

            foreach (var recordStruct in structs.Distinct())
            {
                if (recordStruct is not INamedTypeSymbol symbol) continue;

                // Ensure it implements INamed
                if (!symbol.Interfaces.Any(i => i.Name == "INamed")) continue;

                // Generate the readonly struct
                var namespaceName = symbol.ContainingNamespace.ToDisplayString();
                var structName = $"G{symbol.Name}";
                var properties = symbol
                    .GetMembers()
                    .OfType<IPropertySymbol>()
                    .Select(p => (p.Type.ToDisplayString(), p.Name));

                var sourceCode = GenerateReadonlyStructCode(namespaceName, structName, properties);

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
        var model = context.SemanticModel;
        var symbol = model.GetDeclaredSymbol(recordDecl);

        return symbol is INamedTypeSymbol namedSymbol && namedSymbol.IsValueType ? namedSymbol : null;
    }

    private static string GenerateReadonlyStructCode(
        string namespaceName,
        string structName,
        IEnumerable<(string Type, string Name)> properties)
    {
        var propertyDeclarations = string.Join("\n        ", properties.Select(p =>
            $"public {p.Type} {p.Name} {{ get; }}"));

        var constructorParameters = string.Join(", ", properties.Select(p =>
            $"{p.Type} {char.ToLower(p.Name[0])}{p.Name.Substring(1)}"));

        var constructorAssignments = string.Join("\n            ", properties.Select(p =>
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
