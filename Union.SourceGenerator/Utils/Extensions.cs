using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

#nullable enable

namespace Union.SourceGenerator
{
    internal static class Extensions
    {    
        public static string GetMsBuildProperty(
            this GeneratorExecutionContext context,
            string name,
            string defaultValue = "")
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{name}", out var value);
            return value ?? defaultValue;
        }
        public static TypeInfo GetFieldType(this FieldDeclarationSyntax field, SemanticModel model) 
            => model.GetTypeInfo(field.Declaration.Type);

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items)
        {
            foreach (var item in items)
            {
                if (item is not null)
                    yield return item;
            }
        }

        private const char NESTED_CLASS_DELIMITER = '+';
        private const char NAMESPACE_CLASS_DELIMITER = '.';
        private const char TYPEPARAMETER_CLASS_DELIMITER = '`';
        
        public static void AddSource(this GeneratorExecutionContext context, GeneratedSource source)
            => context.AddSource(source.FileName, SourceText.From(source.SourceCode, Encoding.UTF8));

        public static bool TryGetAttribute(this MemberDeclarationSyntax syntax, string attributeName)
            => TryGetAttribute(syntax, attributeName, out _);
        public static bool TryGetAttribute(this MemberDeclarationSyntax syntax, string attributeName, [NotNullWhen(true)] out AttributeSyntax? attribute)
        {
            attribute = syntax.GetAttribute(attributeName);
            return attribute is not null;
        }

        public static AttributeSyntax? GetAttribute(this MemberDeclarationSyntax syntax, string attributeName)
        {
            var shortName = attributeName.EndsWith("Attribute")
                ? attributeName.Substring(0, attributeName.Length - "Attribute".Length)
                : attributeName;
            var longName = attributeName.EndsWith("Attribute")
                ? attributeName
                : $"{attributeName}Attribute";
            
            return syntax.AttributeLists
                .SelectMany(al => al.Attributes)
                .FirstOrDefault(a => (a.Name is  IdentifierNameSyntax ident) &&
                    (ident.Identifier.Text == shortName || ident.Identifier.Text == longName));
        }
        public static string GetNamespace(this MemberDeclarationSyntax source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            var namespaces = new LinkedList<NamespaceDeclarationSyntax>();
            for (var parent = source.Parent; parent != null; parent = parent.Parent)
            {
                if (parent is NamespaceDeclarationSyntax @namespace)
                    namespaces.AddFirst(@namespace);
            }
            return string.Join(NAMESPACE_CLASS_DELIMITER.ToString(), namespaces.Select(n => n.Name));
        }
        
        public static string GetFullName(this TypeDeclarationSyntax source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            var namespaces = new LinkedList<NamespaceDeclarationSyntax>();
            var types = new LinkedList<TypeDeclarationSyntax>();
            for (var parent = source.Parent; parent != null; parent = parent.Parent)
            {
                if (parent is NamespaceDeclarationSyntax @namespace)
                    namespaces.AddFirst(@namespace);
                else if (parent is TypeDeclarationSyntax type)
                    types.AddFirst(type);
            }

            var result = new StringBuilder();
            for (var item = namespaces.First; item != null; item = item.Next)
            {
                result.Append(item.Value.Name).Append(NAMESPACE_CLASS_DELIMITER);
            }
            for (var item = types.First; item != null; item = item.Next)
            {
                var type = item.Value;
                if (type is not null)
                {
                    AppendName(result, type);
                    result.Append(NESTED_CLASS_DELIMITER);
                }
            }
            AppendName(result, source);

            return result.ToString();
        }
        
        private static void AppendName(StringBuilder builder, TypeDeclarationSyntax type)
        {
            builder.Append(type.Identifier.Text);
            var typeArguments = type.TypeParameterList?.ChildNodes()
                .Count(node => node is TypeParameterSyntax) ?? 0;
            if (typeArguments != 0)
                builder.Append(TYPEPARAMETER_CLASS_DELIMITER).Append(typeArguments);
        }
    }
}