using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#nullable enable

namespace Union.SourceGenerator
{
    public record StructInfo(StructDeclarationSyntax Syntax,
        string StructName,
        string Namespace,
        IReadOnlyList<StructMemberInfo> Members, 
        AttributeSyntax GenerateAttribute
    );
    
    public record StructMemberInfo(
        int Index,
        FieldDeclarationSyntax Field
    );

    public record SemanticStructInfo(
        StructDeclarationSyntax Syntax,
        string Name,
        string Namespace,
        IReadOnlyList<SemanticStructMemberInfo> Members,
        string IndexFieldName,
        UnionOptions Options
    );
    
    public record SemanticStructMemberInfo(
        int Index,
        FieldDeclarationSyntax Field,
        string FieldName,
        string PascalName,
        string TypeName,
        NullableAnnotation Nullable,
        bool IsReferenceType,
        bool CanBeNull
    );
}