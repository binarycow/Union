using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Union.Generator
{
    internal static class Program
    {
        public static void Main()
        {
            var targetDirectory = @"C:\Users\mikec\RiderProjects\Union\Union";
            Directory.CreateDirectory(targetDirectory);
            Directory.CreateDirectory(Path.Combine(targetDirectory, "Interfaces"));
            Directory.CreateDirectory(Path.Combine(targetDirectory, "Classes"));
            Directory.CreateDirectory(Path.Combine(targetDirectory, "Structs"));
            Directory.CreateDirectory(Path.Combine(targetDirectory, "Enums"));
            
            var numTypes = 8;
            for (var count = 0; count < numTypes; ++count)
            {
                CreateTypes(targetDirectory, count);
            }

            Console.WriteLine("Done.");
        }

        private static void CreateTypes(string targetDirectory, int count)
        {
            CreateEnum(targetDirectory, count);
            CreateInterface(targetDirectory, count);
            CreateStruct(targetDirectory, count);
        }

        private static void CreateEnum(string targetDirectory, int count)
        {
            var file = new FileInfo(Path.Combine(targetDirectory, "Enums", $"UnionIndexT{count.ToString()}.cs"));
            File.WriteAllText(file.FullName, string.Empty);
            using var stream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var streamWriter = new StreamWriter(stream);
            using var writer = new IndentedTextWriter(streamWriter);
            using var ns = writer.IndentBlock($"namespace Union");
            using var iface = writer.IndentBlock($"public enum UnionIndexT{count.ToString()}");
            for (var n = 0; n < count + 1; ++n)
            {
                writer.WriteLine($"T{n.ToString()} = {n.ToString()},");
            }
        }

        private static IEnumerable<(string Signature, int N)> AsMethodSignatures(int count)
        {
            foreach (var n in Enumerable.Range(0, count + 1))
                yield return ($"public T{n.ToString()}? AsT{n.ToString()}()", n);
        }
        private static IEnumerable<(string Signature, int N)> IsMethodSignatures(int count)
        {
            foreach (var n in Enumerable.Range(0, count + 1))
                yield return ($"public bool IsT{n.ToString()}([NotNullWhen(true)] out T{n.ToString()}? value)", n);
        }
        private static IEnumerable<(string Signature, int N, string Type)> SelectMethodSignatures(int count)
        {
            for (var n = 0; n < count + 1; ++n)
            {
                var ret = Comma(count, i => i == n ? "TResult" : $"T{i.ToString()}");
                yield return ($"public Union<{ret}> Select{n}<TResult>(Func<T{n}, TResult> selector)", n, $"Union<{ret}>");
            }
        }

        
        
        private static void CreateStruct(string targetDirectory, int count)
        {
            var typeName = $"Union<{Comma(count, T)}>";
            WriteType(
                Path.Combine(targetDirectory, "Structs"),
                count,
                "readonly struct",
                typeName,
                $"IEquatable<Union<{Comma(count, T)}>>, IUnion<{Comma(count, T)}>",
                $"UnionT{count}",
                new []
                {
                    "System",
                    "System.Collections.Generic",
                    "System.Diagnostics.CodeAnalysis"
                },
                Fields,
                Constructor,
                IsMethod,
                AsMethod,
                VoidSwitchMethod,
                ReturnSwitchMethod,
                StaticFactory,
                SelectMethod,
                ValueProperty
            );

            void StaticFactory(IndentedTextWriter writer)
            {
                using var r = writer.WriteRegion("Static factory methods");
                for (var n = 0; n < count + 1; ++n)
                {
                    writer.WriteLine($"public static {typeName} FromT{n.ToString()}(T{n.ToString()} t) => new (UnionIndexT{count.ToString()}.T{n.ToString()}, value{n.ToString()}: t);");
                }
            }
            void Fields(IndentedTextWriter writer)
            {
                writer.WriteLine($"public UnionIndexT{count.ToString()} Index {{ get; }}");
                for(var n = 0; n < count + 1; ++n)
                    writer.WriteLine($"private readonly T{n.ToString()}? value{n.ToString()};");
                writer.WriteLine();
                writer.WriteLine("int IUnion.Index => (int) Index;");
            }
            void Constructor(IndentedTextWriter writer)
            {
                var parameters = Enumerable.Range(0, count + 1).Select(n => $"T{n.ToString()}? value{n.ToString()} = default");
                using var ctor = writer.IndentBlock($"private Union(UnionIndexT{count.ToString()} index, {Comma(parameters)})");
                writer.WriteLine("this.Index = index;");
                for(var n = 0; n < count + 1; ++n)
                    writer.WriteLine($"this.value{n.ToString()} = default;");
                using var sw = writer.IndentBlock("switch (this.Index)");
                for (var n = 0; n < count + 1; ++n)
                {
                    using var c = writer.IndentCase($"case UnionIndexT{count.ToString()}.T{n.ToString()}:");
                    writer.WriteLine($"this.value{n.ToString()} = value{n.ToString()} ?? throw new ArgumentNullException(nameof(value{n.ToString()}));");
                    writer.WriteLine("break;");
                }
                using var c2 = writer.IndentCase($"default:");
                writer.WriteLine("throw new ArgumentOutOfRangeException(nameof(index));");
            }

            void IsMethod(IndentedTextWriter writer, int n)
            {
                writer.WriteLine($"value = this.Index == UnionIndexT{count.ToString()}.T{n.ToString()} ? value{n.ToString()} : default;");
                writer.WriteLine($"return this.Index == UnionIndexT{count.ToString()}.T{n.ToString()} && !(value is null);");
            }

            void AsMethod(IndentedTextWriter writer, int n)
            {
                writer.WriteLine($"return this.Index == UnionIndexT{count.ToString()}.T{n.ToString()} ? value{n.ToString()} : throw new InvalidOperationException($\"Cannot return as T{n.ToString()} as result is T{{this.Index}}\");");
            }

            void VoidSwitchMethod(IndentedTextWriter writer)
            {
                for (var n = 0; n < count + 1; ++n)
                {
                    writer.WriteLine($"f{n.ToString()} = f{n.ToString()} ?? throw new ArgumentNullException(nameof(f{n.ToString()}));");
                }
                using var sw = writer.IndentBlock("switch (this.Index)");
                for (var n = 0; n < count + 1; ++n)
                {
                    using var c = writer.IndentCase($"case UnionIndexT{count.ToString()}.T{n.ToString()} when value{n.ToString()} is not null:");
                    writer.WriteLine($"f{n.ToString()}(this.value{n.ToString()});");
                    writer.WriteLine("return;");
                }
            }

            void ReturnSwitchMethod(IndentedTextWriter writer)
            {
                for (var n = 0; n < count + 1; ++n)
                {
                    writer.WriteLine($"f{n.ToString()} = f{n.ToString()} ?? throw new ArgumentNullException(nameof(f{n.ToString()}));");
                }
                using var sw = writer.IndentSwitchExpression("return this.Index switch");
                for (var n = 0; n < count + 1; ++n)
                {
                    writer.WriteLine($"UnionIndexT{count.ToString()}.T{n.ToString()} when value{n.ToString()} is not null => f{n.ToString()}(this.value{n.ToString()}),");
                }
                writer.WriteLine("_ => throw new InvalidOperationException(),");
            }

            void SelectMethod(IndentedTextWriter writer, int n, string type)
            {
                writer.WriteLine($"selector = selector ?? throw new ArgumentNullException(nameof(selector));");
                using var sw = writer.IndentSwitchExpression("return this.Index switch");
                for (var m = 0; m < count + 1; ++m)
                {
                    writer.WriteLine(m == n
                        ? $"UnionIndexT{count.ToString()}.T{m.ToString()} when this.value{m.ToString()} is not null => selector(this.value{m.ToString()}),"
                        : $"UnionIndexT{count.ToString()}.T{m.ToString()} when this.value{m.ToString()} is not null => {type}.FromT{m.ToString()}(this.value{m.ToString()}),");
                }
                writer.WriteLine($"_ => throw new InvalidOperationException(),");
            }

            void ValueProperty(IndentedTextWriter writer)
            {
                using var sw = writer.IndentSwitchExpression(" => Index switch");
                for (var n = 0; n < count + 1; ++n)
                    writer.WriteLine($"UnionIndexT{count.ToString()}.T{n.ToString()} when value{n.ToString()} is not null => value{n.ToString()},");
                writer.WriteLine("_ => throw new InvalidOperationException(),");
            }
        }

        private static void CreateInterface(string targetDirectory, int count)
        {
            WriteType(
                Path.Combine(targetDirectory, "Interfaces"),
                count,
                "interface",
                $"IUnion<{Comma(count, T)}>",
                $"IUnion",
                $"IUnionT{count}",
                new []
                {
                    "System",
                    "System.Diagnostics.CodeAnalysis"
                },
                fields: null,
                constructor: null,
                isMethod: null,
                asMethod: null,
                voidSwitchMethod: null,
                returnSwitchMethod: null,
                staticFactory: null,
                selectMethod: null,
                valueProperty: null
            );
        }
        
        private static void WriteType(
            string targetDirectory,
            int count,
            string typeType,
            string typeName,
            string baseClasses,
            string fileNameWithoutExt,
            IEnumerable<string> namespaces,
            Action<IndentedTextWriter>? fields,
            Action<IndentedTextWriter>? constructor,
            Action<IndentedTextWriter, int>? isMethod,
            Action<IndentedTextWriter, int>? asMethod,
            Action<IndentedTextWriter>? voidSwitchMethod,
            Action<IndentedTextWriter>? returnSwitchMethod,
            Action<IndentedTextWriter>? staticFactory,
            Action<IndentedTextWriter, int, string>? selectMethod,
            Action<IndentedTextWriter>? valueProperty
        )
        {
            var file = new FileInfo(Path.Combine(targetDirectory, $"{fileNameWithoutExt}.cs"));
            File.WriteAllText(file.FullName, string.Empty);
            using var stream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var streamWriter = new StreamWriter(stream);
            using var writer = new IndentedTextWriter(streamWriter);
            foreach (var namespaceName in namespaces)
            {
                writer.WriteLine($"using {namespaceName};");
            }
            writer.WriteLine();
            using var ns = writer.IndentBlock($"namespace Union");
            using var iface = writer.IndentBlock($"public {typeType} {typeName} : {baseClasses}");
            
            fields?.Invoke(writer);
            constructor?.Invoke(writer);
            
            WriteVoidSwitch();
            WriteReturnSwitch();

            if (valueProperty is not null)
            {
                writer.Write("public object Value");
                valueProperty(writer);
                writer.WriteLine();
            }
            
            writer.WriteRegion("As Methods", WriteAsMethods);
            writer.WriteRegion("Is Methods", WriteIsMethods);
            writer.WriteRegion("Select Methods", WriteSelectMethods);
            
            
            if (typeType.Contains("interface"))
                return;
            var isClass = typeType.Contains("struct") == false;
            WriteToString();
            WriteConversions(writer, typeName, count);
            staticFactory?.Invoke(writer);
            if (isClass)
                return;

            WriteEquality(writer, typeName, count);

            void WriteToString()
            {
                if (isClass)
                {
                    writer.WriteLine($"public override string ToString() => union.ToString();");
                    return;
                }
                using var sw = writer.IndentSwitchExpression($"public override string ToString() => this.Index switch");
                for (var n = 0; n < count + 1; ++n)
                {
                    writer.WriteLine($"UnionIndexT{count.ToString()}.T{n.ToString()} => $\"{{typeof(T{n.ToString()})}}: {{value{n.ToString()}}}\",");
                }

                writer.WriteLine($"_ => throw new InvalidOperationException(),");
            }
            void WriteVoidSwitch()
            {
                var parameters = Enumerable.Range(0, count + 1).Select(n => $"Action<T{n.ToString()}> f{n.ToString()}");
                WriteMethod(writer, voidSwitchMethod, $"public void Switch({string.Join(", ", parameters)})");
                writer.WriteLine();
            }
            void WriteReturnSwitch()
            {
                var parameters = Enumerable.Range(0, count + 1).Select(n => $"Func<T{n.ToString()}, TResult> f{n.ToString()}");
                WriteMethod(writer, returnSwitchMethod, $"public TResult Switch<TResult>({string.Join(", ", parameters)})");
                writer.WriteLine();
            }
            void WriteAsMethods()
            {
                foreach (var (methodSig, n) in AsMethodSignatures(count))
                    WriteMethod(writer, asMethod is null ? null : w => asMethod(w, n), methodSig);
            }            
            void WriteIsMethods()
            {
                foreach(var (methodSig, n) in IsMethodSignatures(count))
                    WriteMethod(writer, isMethod is null ? null : w => isMethod(w, n), methodSig);
            }
            void WriteSelectMethods()
            {
                foreach(var (methodSig, n, type) in SelectMethodSignatures(count))
                    WriteMethod(writer, selectMethod is null ? null : w => selectMethod(w, n, type), methodSig);
            }
        }

        private static void WriteEquality(IndentedTextWriter writer, string typeName, int count)
        {
            using var r = writer.WriteRegion("Equality");
            WriteEquals();
            WriteGetHashcode();
            writer.WriteLine($"public override bool Equals(object? obj) => obj is {typeName} other && Equals(other);");
            writer.WriteLine($"public static bool operator ==({typeName} left, {typeName} right) => left.Equals(right);");
            writer.WriteLine($"public static bool operator !=({typeName} left, {typeName} right) => !left.Equals(right);");

            void WriteGetHashcode()
            {
                using var method = writer.IndentBlock($"public override int GetHashCode()");
                using var sw = writer.IndentSwitchExpression("return this.Index switch");
                for (var n = 0; n < count + 1; ++n)
                    writer.WriteLine($"UnionIndexT{count.ToString()}.T{n.ToString()} => HashCode.Combine(this.Index, this.value{n.ToString()}),");
                writer.WriteLine("_ => throw new InvalidOperationException($\"Invalid index {this.Index}\"),");
            }
            void WriteEquals()
            {
                using var method = writer.IndentBlock($"public bool Equals({typeName} other)");
                using var sw = writer.IndentSwitchExpression("return this.Index switch");
                writer.WriteLine("{ } when this.Index != other.Index => false,");
                for (var n = 0; n < count + 1; ++n)
                    writer.WriteLine($"UnionIndexT{count.ToString()}.T{n.ToString()} => EqualityComparer<T{n.ToString()}?>.Default.Equals(this.value{n.ToString()}, other.value{n.ToString()}),");
                writer.WriteLine("_ => throw new InvalidOperationException($\"Invalid index {this.Index}\"),");
            }
        }

        private static void WriteConversions(IndentedTextWriter writer, string typeName, int count)
        {
            using var r = writer.WriteRegion("Conversion");
            for (var n = 0; n < count + 1; ++n)
                writer.WriteLine($"public static implicit operator {typeName}(T{n.ToString()} t) => FromT{n.ToString()}(t);");   
        }

        private static void WriteMethod(IndentedTextWriter writer, Action<IndentedTextWriter>? method, string signature)
        {
            if (method is null)
                writer.WriteLine($"{signature};");
            else
            {
                using var block = writer.IndentBlock(signature);
                method(writer);
            }
        }


        private static string T(int n) => $"T{n}";

        private static string Comma(IEnumerable<string> strings) => string.Join(", ", strings);
        private static string Comma(int count, Func<int, string> func, bool inclusive = true)
            => Comma(Enumerable.Range(0, count + (inclusive ? 1 : 0)).Select(func));

    }
}