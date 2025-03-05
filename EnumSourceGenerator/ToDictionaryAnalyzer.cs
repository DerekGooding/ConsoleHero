using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace EnumSourceGenerator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ToDictionaryAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "TND001";
    private static readonly LocalizableString Title = "Use ToNamedDictionary for INamed keys";
    private static readonly LocalizableString MessageFormat = "Replace 'ToDictionary' with 'ToNamedDictionary' when using INamed keys";
    private static readonly LocalizableString Description = "INamed keys should use ToNamedDictionary to ensure correct equality comparison.";
    private const string Category = "Usage";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Description
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        var invocationExpr = (InvocationExpressionSyntax)context.Node;

        // Ensure it's a ToDictionary() call
        if (invocationExpr.Expression is not MemberAccessExpressionSyntax memberAccess)
            return;

        if (memberAccess.Name.Identifier.Text != "ToDictionary")
            return;

        var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpr).Symbol as IMethodSymbol;
        if (methodSymbol == null || methodSymbol.ContainingType.Name != "Enumerable")
            return;

        // Get the first generic argument (the key type)
        if (methodSymbol.TypeArguments.Length == 0)
            return;

        var keyType = methodSymbol.TypeArguments[0];

        // Check if the key type implements INamed
        if (!keyType.AllInterfaces.Any(i => i.Name == "INamed"))
            return;

        // Report diagnostic
        var diagnostic = Diagnostic.Create(Rule, memberAccess.Name.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}