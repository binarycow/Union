using Microsoft.CodeAnalysis;

#nullable enable

namespace Union.SourceGenerator
{
    public static class Diag
    {
        public static readonly DiagnosticDescriptor UnknownError = new (
            id: "UNION001",
            title: "Unknown Error",
            messageFormat: "Unknown Error: {0}",
            category: "Union",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor TypeNameNotResolved = new (
            id: "UNION002",
            title: "Could not resolve type",
            messageFormat: "Could not resolve type name: {0}",
            category: "Union",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor NoStructMembers = new (
            id: "UNION003",
            title: "No members",
            messageFormat: "Struct '{0}' does not have any fields defined",
            category: "Union",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor MultipleIndexFields = new (
            id: "UNION004",
            title: "Multiple index fields",
            messageFormat: "Struct '{0}' has multiple index fields: {1}",
            category: "Union",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor ConflictingTypes = new (
            id: "UNION005",
            title: "Conflicting types",
            messageFormat: "Struct '{0}' has multiple fields defined for type '{1}'",
            category: "Union",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor IndexFieldWithoutAttribute = new (
            id: "UNION005",
            title: "Index field without attribute",
            messageFormat: "Struct '{0}' has a field named 'index' without a 'UnionIndexAttribute' applied.",
            category: "Union",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
        public static readonly DiagnosticDescriptor UnknownWarning = new (
            id: "UNION006",
            title: "Unknown Warning",
            messageFormat: "Unknown Warning: {0}",
            category: "Union",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );
        
        public static void Report(
            this DiagnosticDescriptor descriptor,
            GeneratorExecutionContext context,
            Location location,
            params object[] parameters
        )
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor, location, parameters)
            );
        }
    }
}