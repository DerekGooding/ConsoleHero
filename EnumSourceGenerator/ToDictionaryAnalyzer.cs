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
    private static readonly LocalizableString _title = "Use ToNamedDictionary for INamed keys";
    private static readonly LocalizableString _messageFormat = "Replace 'ToDictionary' with 'ToNamedDictionary' when using INamed keys";
    private static readonly LocalizableString _description = "INamed keys should use ToNamedDictionary to improve lookup performance.";
    private const string _category = "Usage";

    private static readonly DiagnosticDescriptor _rule = new(
        DiagnosticId,
        _title,
        _messageFormat,
        _category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: _description
    );

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [_rule];

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

        if (context.SemanticModel.GetSymbolInfo(invocationExpr).Symbol is not IMethodSymbol methodSymbol || methodSymbol.ContainingType.Name != "Enumerable")
            return;

        // Get the first generic argument (the key type)
        if (methodSymbol.TypeArguments.Length == 0)
            return;

        var keyType = methodSymbol.TypeArguments[0];

        // Check if the key type implements INamed
        if (!keyType.AllInterfaces.Any(i => i.Name == "INamed"))
            return;

        // Report diagnostic
        var diagnostic = Diagnostic.Create(_rule, memberAccess.Name.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }
}