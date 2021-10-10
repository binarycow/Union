using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

#nullable enable

namespace Union.SourceGenerator
{
    internal class StructGenerator : CommonGenerator<SemanticStructInfo>
    {
        public static StructGenerator Instance { get; } = new ();
        public GeneratedSource Generate(SemanticStructInfo item)
        {
            using var text = new StringWriter();
            using var writer = new IndentedTextWriter(text);
            using var fileWriter = new FileWriter(writer);
            Generate(fileWriter, item);
            return new (text.ToString(), item.Name);
        }

        private void WriteRegion(
            CodeWriter writer, 
            SemanticStructInfo item, 
            string regionHeader,
            Action<CodeWriter, SemanticStructInfo> action,
            UnionOptions? options = null
        )
        {
            if (!(options is null || item.Options.HasAnyFlag(options.Value))) return;
            writer.WriteLine(string.Empty);
            writer.WriteLine(string.Empty);
            using var r = writer.WriteRegion(regionHeader);
            action(r, item);
        }

        private void Generate(CodeWriter writer, SemanticStructInfo item)
        {
            writer.WriteLine("#nullable enable");
            writer.WriteLine(string.Empty);
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("using System.Diagnostics.CodeAnalysis;");
            writer.WriteLine(string.Empty);
            using var ns = writer.WriteBlock($"namespace {item.Namespace}");
            using var str = ns.WriteBlock($"partial struct {item.Name} : IEquatable<{item.Name}>");
            writer.WriteLine(string.Empty);
            if (item.Options.HasAnyFlag(UnionOptions.Index))
            {
                writer.WriteLine($"private readonly int {item.IndexFieldName};");
            }
            WriteRegion(writer, item, "Constructors", WriteConstructors);
            WriteRegion(writer, item, "Equality", WriteEqualityRegion);
            
            WriteRegion(writer, item, "Misc", WriteMisc, UnionOptions.ToString | UnionOptions.Value);
            WriteRegion(writer, item, "Switch", WriteSwitch, UnionOptions.Switch);
            WriteRegion(writer, item, "As Methods", WriteAs, UnionOptions.As);
            WriteRegion(writer, item, "Is Methods", WriteIs, UnionOptions.Is);
            WriteRegion(writer, item, "From Methods", WriteFrom, UnionOptions.From);
            WriteRegion(writer, item, "Conversion Methods", WriteConversion, UnionOptions.Conversions);
        }

        private void WriteEqualityRegion(CodeWriter writer, SemanticStructInfo item)
        {
            WriteEquals(writer, item);
            writer.WriteLine(string.Empty);
            WriteGetHashCode(writer, item);
            writer.WriteLine($"public override bool Equals(object? obj) => obj is {item.Name} other && Equals(other);");
            writer.WriteLine($"public static bool operator ==({item.Name} left, {item.Name} right) => left.Equals(right);");
            writer.WriteLine($"public static bool operator !=({item.Name} left, {item.Name} right) => !left.Equals(right);");
            
            static void WriteEquals(CodeWriter writer, SemanticStructInfo item)
            {
                using var b = writer.WriteBlock($"public bool Equals({item.Name} other)");
                using var s = b.WriteBlockStatement($"return this.{item.IndexFieldName} switch");
                s.WriteLine($"{{ }} when this.{item.IndexFieldName} != other.{item.IndexFieldName} => false,");
                foreach (var member in item.Members)
                {
                    s.WriteLine($"{member.Index.ToString()} => EqualityComparer<{member.TypeName}>.Default.Equals(this.{member.FieldName}, other.{member.FieldName}),");
                }
                s.WriteLine($"_ => throw new InvalidOperationException($\"Invalid index {{this.{item.IndexFieldName}}}\"),");
            }
            
            static void WriteGetHashCode(CodeWriter writer, SemanticStructInfo item)
            {
                using var b = writer.WriteBlock($"public override int GetHashCode()");
                using var s = b.WriteBlockStatement($"return this.{item.IndexFieldName} switch");
                foreach (var member in item.Members)
                {
                    s.WriteLine($"{member.Index.ToString()} => HashCode.Combine(this.{item.IndexFieldName}, this.{member.FieldName}),");
                }
                s.WriteLine($"_ => throw new InvalidOperationException($\"Invalid index {{this.{item.IndexFieldName}}}\"),");
            }
        }
        private void WriteConstructors(CodeWriter writer, SemanticStructInfo item)
        {
            WritePrivateConstructor(writer, item);
            foreach (var member in item.Members)
            {
                writer.WriteLine(string.Empty);
                WriteMemberConstructor(writer, item, member);
            }
            
            static void WritePrivateConstructor(CodeWriter writer, SemanticStructInfo item)
            {
                writer.Write($"private {item.Name}(int {item.IndexFieldName}");
                foreach (var member in item.Members)
                {
                    writer.Write($", {member.TypeName}");
                    if (member.CanBeNull)
                        writer.Write("?");
                    writer.Write($" {member.FieldName} = default");
                }
                using var ctor = writer.WriteBlock(")");
                ctor.WriteLine($"this.{item.IndexFieldName} = {item.IndexFieldName};");
                foreach (var member in item.Members)
                {
                    ctor.WriteLine($"this.{member.FieldName} = {member.FieldName};");
                }
                using var sw = writer.WriteSwitch($"this.{item.IndexFieldName}");
                foreach (var member in item.Members)
                {
                    var line = $"this.{member.FieldName} = {member.FieldName}";
                    if (member.CanBeNull)
                    {
                        line += $" ?? throw new ArgumentNullException(nameof({member.FieldName}))";
                    }
                    sw.WriteCaseLine(member.Index.ToString(), $"{line};");
                }
                sw.WriteDefaultLine($"throw new ArgumentOutOfRangeException(nameof({item.IndexFieldName}));", isThrow: true);
            }
            static void WriteMemberConstructor(CodeWriter writer, SemanticStructInfo item, SemanticStructMemberInfo member)
            {
                writer.Write($"public {item.Name}({member.TypeName} value) : this({member.Index.ToString()}, {member.FieldName}: value");
                if (member.CanBeNull)
                {
                    writer.Write(" ?? throw new ArgumentNullException(nameof(value))");
                }
                writer.WriteLine(") { }");
            }
        }
        
        private void WriteMisc(CodeWriter writer, SemanticStructInfo item)
        {
            if(item.Options.HasAnyFlag(UnionOptions.Value))
                WriteValue(writer, item);
            writer.WriteLine(string.Empty);
            if(item.Options.HasAnyFlag(UnionOptions.ToString))
                WriteToString(writer, item);

            static void WriteValue(CodeWriter writer, SemanticStructInfo item)
            {
                using var block = writer.WriteBlock("public object Value");
                using var get = writer.WriteBlock("get");
                using var sw = get.WriteBlockStatement($"return this.{item.IndexFieldName} switch");
                foreach (var member in item.Members)
                {
                    sw.WriteLine(member.CanBeNull
                        ? $"{member.Index} when {member.FieldName} is not null => {member.FieldName},"
                        : $"{member.Index} => {member.FieldName},");
                }
                sw.WriteLine("_ => throw new InvalidOperationException(),");
            }
            static void WriteToString(CodeWriter writer, SemanticStructInfo item)
            {
                using var block = writer.WriteBlock("public override string ToString()");
                using var sw = block.WriteBlockStatement($"return this.{item.IndexFieldName} switch");
                foreach (var member in item.Members)
                {
                    sw.WriteLine($"{member.Index.ToString()} => $\"{{typeof({member.TypeName})}}: {{{member.FieldName}}}\",");
                }
                sw.WriteLine("_ => throw new InvalidOperationException(),");
            }
        }
        

        private void WriteSwitch(CodeWriter writer, SemanticStructInfo item)
        {
            WriteVoid(writer, item);
            writer.WriteLine(string.Empty);
            WriteReturn(writer, item);

            static void WriteVoid(CodeWriter writer, SemanticStructInfo item)
            {
                var parameters = string.Join(", ", item.Members.Select(member => $"Action<{member.TypeName}> f{member.Index.ToString()}"));
                using var block = writer.WriteBlock($"public void Switch({parameters})");
                foreach (var member in item.Members)
                {
                    var paramName = $"f{member.Index.ToString()}";
                    block.WriteLine($"{paramName} = {paramName} ?? throw new ArgumentNullException(nameof({paramName}));");
                }
                using var sw = block.WriteSwitch($"this.{item.IndexFieldName}");
                foreach (var member in item.Members)
                {
                    sw.WriteCaseLine(
                        member.CanBeNull
                            ? $"{member.Index.ToString()} when !({member.FieldName} is null)"
                            : $"{member.Index.ToString()}",
                        $"f{member.Index.ToString()}(this.{member.FieldName});",
                        isReturn: true
                    );
                }
            }

            static void WriteReturn(CodeWriter writer, SemanticStructInfo item)
            {
                var parameters = string.Join(", ", item.Members.Select(member => $"Func<{member.TypeName}, TResult> f{member.Index.ToString()}"));
                using var block = writer.WriteBlock($"public TResult Switch<TResult>({parameters})");
                foreach (var member in item.Members)
                {
                    var paramName = $"f{member.Index.ToString()}";
                    block.WriteLine($"{paramName} = {paramName} ?? throw new ArgumentNullException(nameof({paramName}));");
                }
                using var sw = block.WriteBlockStatement($"return this.{item.IndexFieldName} switch");
                foreach (var member in item.Members)
                {
                    var condition = member.CanBeNull
                        ? $"{member.Index.ToString()} when !({member.FieldName} is null)"
                        : $"{member.Index.ToString()}";
                    sw.WriteLine($"{condition} => f{member.Index.ToString()}(this.{member.FieldName}),");
                }
                sw.WriteLine("_ => throw new InvalidOperationException(),");
            }
        }

        private void WriteAs(CodeWriter writer, SemanticStructInfo item)
        {
            foreach (var member in item.Members)
            {
                writer.WriteLine($"public {member.TypeName} As{member.PascalName}()");
                using var indent = writer.Indent($"=> this.{item.IndexFieldName} == {member.Index.ToString()}");
                var line = member.CanBeNull
                    ? $"? {member.FieldName} ?? throw new InvalidOperationException()"
                    : $"? {member.FieldName}";
                indent.WriteIndentedLine(line);
                indent.WriteIndentedLine($" : throw new InvalidOperationException($\"Cannot return as {member.PascalName}; value is not the correct type.\");");
            }
        }
        private void WriteIs(CodeWriter writer, SemanticStructInfo item)
        {
            foreach (var member in item.Members)
            {
                WriteMember(writer, item, member);
            }
            static void WriteMember(CodeWriter writer, SemanticStructInfo item, SemanticStructMemberInfo member)
            {
                writer.Write($"public bool Is{member.PascalName}(");
                if (member.CanBeNull)
                    writer.Write("[NotNullWhen(true)] ");
                writer.Write($"out {member.TypeName}");
                if (member.CanBeNull)
                    writer.Write("?");
                using var block = writer.WriteBlock(" value)");
                writer.WriteLine($"value = this.{item.IndexFieldName} == {member.Index.ToString()} ? this.{member.FieldName} : default;");
                writer.Write($"return this.{item.IndexFieldName} == {member.Index.ToString()}");
                if (member.CanBeNull)
                    writer.Write(" && !(value is null)");
                writer.WriteLine(";");
            }
        }

        private void WriteFrom(CodeWriter writer, SemanticStructInfo item)
        {
            foreach (var member in item.Members)
            {
                writer.WriteLine($"public static {item.Name} From{member.PascalName}({member.TypeName} {member.FieldName}) => new {item.Name}({member.Index.ToString()}, {member.FieldName}: {member.FieldName});");
            }
        }
        private void WriteConversion(CodeWriter writer, SemanticStructInfo item)
        {
            foreach (var member in item.Members)
            {
                writer.WriteLine($"public static implicit operator {item.Name}({member.TypeName} {member.FieldName}) => new {item.Name}({member.Index.ToString()}, {member.FieldName}: {member.FieldName});");
            }
        }

    }
}