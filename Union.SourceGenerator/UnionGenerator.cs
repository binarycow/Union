using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Union.SourceGenerator
{
    [Generator]
    public class UnionGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new Receiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not Receiver receiver)
                return;
            foreach (var item in receiver.Structs.Select(x => CreateSemantic(x, context)))
            {
                if (item.Members.Count == 0)
                {
                    Diag.NoStructMembers.Report(context, item.Syntax.GetLocation());
                    continue;
                }
                context.AddSource(StructGenerator.Instance.Generate(item));
            }
            context.AddSource(new (Attributes.ATTRIBUTE_TEXT, "UnionAttributes"));
        }

        private SemanticStructInfo CreateSemantic(StructInfo item, GeneratorExecutionContext context)
        {
            var model = context.Compilation.GetSemanticModel(item.Syntax.SyntaxTree);
            var options = GetOptions(item, context, model);
            var members = CreateSemantic(
                item,
                context,
                model,
                out var indexFieldName,
                ref options
            );
            return new SemanticStructInfo(
                item.Syntax,
                item.StructName,
                item.Namespace,
                members,
                indexFieldName,
                options
            );
        }

        private UnionOptions GetOptions(StructInfo item, GeneratorExecutionContext context, SemanticModel model)
        {
            var args = item.GenerateAttribute.ArgumentList?.Arguments;
            if (args is null || args.Value.Count == 0)
                return UnionOptions.Default;
            var options = UnionOptions.Index;
            GetOptions(args.Value[0].Expression, ref options, context, model);
            return options;
        }

        private void GetOptions(
            ExpressionSyntax expression, 
            ref UnionOptions options, 
            GeneratorExecutionContext context, 
            SemanticModel model
        )
        {
            switch (expression)
            {
                case BinaryExpressionSyntax { OperatorToken: { Text: "|" } } binary:
                    GetOptions(binary.Left, ref options, context, model);
                    GetOptions(binary.Right, ref options, context, model);
                    break;
                case IdentifierNameSyntax ident when Enum.TryParse<UnionOptions>(ident.Identifier.Text, out var flag):
                    options.SetFlags(flag);
                    break;
                case MemberAccessExpressionSyntax 
                { 
                    Expression: IdentifierNameSyntax
                    {
                        Identifier:
                        {
                            Text: "UnionOptions",
                        },
                    } ident, 
                    OperatorToken:
                    {
                        Text: ".",
                    },
                } member when Enum.TryParse<UnionOptions>(member.Name.Identifier.Text, out var flag):
                    options.SetFlags(flag);
                    break;
            }
        }

        private IReadOnlyList<SemanticStructMemberInfo> CreateSemantic(
            StructInfo item,
            GeneratorExecutionContext context,
            SemanticModel model, 
            out string indexFieldName,
            ref UnionOptions options
        )
        {
            indexFieldName = "index";
            options.SetFlags(UnionOptions.Index);
            var list = new List<SemanticStructMemberInfo>();
            var indexFieldNames = new List<string>();
            var usedTypes = new HashSet<string>();
            foreach (var member in item.Members)
            {
                if (IsIndexField(member.Field, indexFieldNames))
                {
                    options.ClearFlags(UnionOptions.Index);
                    continue;
                }
                var semantic = CreateSemantic(member, context, model);
                if (semantic is null)
                    continue;
                if (member.Field.Declaration.Variables.Count > 1 || usedTypes.Add(semantic.TypeName) == false)
                {
                    Diag.ConflictingTypes.Report(context, member.Field.GetLocation(), item.StructName, semantic.TypeName);
                }
                list.Add(semantic);
            }

            if (indexFieldNames.Count == 0)
            {
                var indexField = list.FirstOrDefault(x => x.FieldName == "index");
                if (indexField is not null)
                {
                    Diag.IndexFieldWithoutAttribute.Report(context, indexField.Field.GetLocation(), item.StructName);
                }
            }
            
            if (indexFieldNames.Count > 0)
            {
                indexFieldName = indexFieldNames.First();
                options.ClearFlags(UnionOptions.Index);
            }
            if (indexFieldNames.Count > 1)
            {
                Diag.MultipleIndexFields.Report(context, item.Syntax.GetLocation(), item.StructName, string.Join(", ", indexFieldNames));
            }
            
            return list.AsReadOnly();
        }

        private static bool IsIndexField(FieldDeclarationSyntax field, ICollection<string> indexFieldNames)
        {
            if (!field.TryGetAttribute("UnionIndexAttribute"))
                return false;
            foreach (var decl in field.Declaration.Variables)
            {
                indexFieldNames.Add(decl.Identifier.Text);
            }
            return true;
        }

        private SemanticStructMemberInfo? CreateSemantic(StructMemberInfo item, GeneratorExecutionContext context, SemanticModel model)
        {
            var fieldName = item.Field.Declaration.Variables[0].Identifier.Text;
            var typeInfo = item.Field.GetFieldType(model);
            var symbol = typeInfo.Type;
            if (symbol is null)
            {
                Diag.TypeNameNotResolved.Report(context, item.Field.GetLocation(), item.Field.Declaration.Type);
                return null;
            }
            var canBeNull = symbol.IsReferenceType || typeInfo.Nullability.Annotation == NullableAnnotation.Annotated;
            var typeName = symbol.ToDisplayString();
            return new SemanticStructMemberInfo(
                item.Index,
                item.Field,
                fieldName,
                PascalCase(fieldName),
                typeName,
                typeInfo.Nullability.Annotation,
                symbol.IsReferenceType,
                canBeNull
            );
        }

        private string PascalCase(string fieldName)
        {
            if (fieldName.StartsWith("_"))
                return fieldName.Substring(1);
            if(char.IsUpper(fieldName[0]))
                throw new System.NotImplementedException();
            return char.ToUpper(fieldName[0]) + fieldName.Substring(1);
        }
    }
}
