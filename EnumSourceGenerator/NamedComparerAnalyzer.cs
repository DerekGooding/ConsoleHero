using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace EnumSourceGenerator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class NamedComparerAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "NC001";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Dictionary<TKey, TValue> must use NamedComparer<T> for INamed keys",
        "Dictionary<{0}, {1}> should specify NamedComparer<{0}> as a comparer",
        "Usage",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeObjectCreation, SyntaxKind.ObjectCreationExpression);
    }

    private static void AnalyzeObjectCreation(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is ObjectCreationExpressionSyntax objectCreation &&
            objectCreation.Type is GenericNameSyntax genericType &&
            genericType.Identifier.Text == "Dictionary" &&
            genericType.TypeArgumentList.Arguments.Count == 2)
        {
            // Extract key type
            var keyType = context.SemanticModel.GetTypeInfo(genericType.TypeArgumentList.Arguments[0]).Type;
            if (keyType == null)
                return;

            // Check if key type implements INamed
            var namedInterface = context.Compilation.GetTypeByMetadataName("ConsoleHero.Generator.INamed");
            if (namedInterface == null || !keyType.AllInterfaces.Contains(namedInterface))
                return;

            // Check if a comparer is provided (should be 2nd or 3rd constructor argument)
            var argumentList = objectCreation.ArgumentList?.Arguments;
            if (!argumentList.HasValue || argumentList.Value.Count < 3)
            {
                var diagnostic = Diagnostic.Create(Rule, objectCreation.GetLocation(),
                    keyType.Name, genericType.TypeArgumentList.Arguments[1].ToString());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
