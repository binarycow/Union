using System;
using System.CodeDom.Compiler;

namespace Union.Generator
{
    public static class Extensions
    {
        public static IDisposable IndentCase(this IndentedTextWriter writer, string? firstLine = null)
        {
            if (firstLine is not null)
                writer.WriteLine(firstLine);
            ++writer.Indent;
            return new Disposer(() =>
            {
                --writer.Indent;
            });
        }
        public static IDisposable IndentBlock(this IndentedTextWriter writer, string? firstLine = null)
        {
            if (firstLine is not null)
                writer.WriteLine(firstLine);
            writer.WriteLine("{");
            ++writer.Indent;
            return new Disposer(() =>
            {
                --writer.Indent;
                writer.WriteLine("}");
            });
        }
        public static IDisposable IndentSwitchExpression(this IndentedTextWriter writer, string? firstLine = null)
        {
            if (firstLine is not null)
                writer.WriteLine(firstLine);
            writer.WriteLine("{");
            ++writer.Indent;
            return new Disposer(() =>
            {
                --writer.Indent;
                writer.WriteLine("};");
            });
        }

        public static void WriteRegion(this IndentedTextWriter writer, string name, Action contents, bool newlineAfter = true)
        {
            writer.WriteLine($"#region {name}");
            contents();
            writer.WriteLine($"#endregion {name}");
            if (newlineAfter)
                writer.WriteLine();
        }
        public static IDisposable WriteRegion(this IndentedTextWriter writer, string name, bool newlineAfter = true)
        {
            writer.WriteLine($"#region {name}");
            return new Disposer(() =>
            {
                writer.WriteLine($"#endregion {name}");
                if (newlineAfter)
                    writer.WriteLine();
            });

        }
    }
}