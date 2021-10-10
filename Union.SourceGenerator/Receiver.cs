using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

#nullable enable

namespace Union.SourceGenerator
{
    
    internal class Receiver : ISyntaxReceiver
    {
        public List<StructInfo> Structs { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            switch (syntaxNode)
            {
                case StructDeclarationSyntax sds when sds.TryGetAttribute("GenerateUnionAttribute", out var attr):
                    ProcessStruct(sds, attr);
                    break;
            }
        }

        private void ProcessStruct(StructDeclarationSyntax sds, AttributeSyntax generateAttribute)
        {
            if (!sds.Modifiers.Any(SyntaxKind.PartialKeyword))
                return;
            if (!sds.Modifiers.Any(SyntaxKind.ReadOnlyKeyword))
                return;
            var members = sds.Members
                .OfType<FieldDeclarationSyntax>()
                .Where(IsValidStructMember)
                .Select((syntax, index) => new StructMemberInfo(index, syntax));

            Structs.Add(new StructInfo(
                Syntax: sds, 
                StructName: sds.Identifier.Text,
                Namespace: sds.GetNamespace(),
                Members: members.ToList().AsReadOnly(),
                generateAttribute
            ));
        }


        private static bool IsValidStructMember(MemberDeclarationSyntax member) 
            => member.Modifiers.Any(SyntaxKind.ReadOnlyKeyword);
    }
}