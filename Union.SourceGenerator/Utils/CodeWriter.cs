using System;
using System.CodeDom.Compiler;

#nullable enable

namespace Union.SourceGenerator
{

    internal abstract class CodeWriter : IDisposable
    {
        protected IndentedTextWriter Writer { get; }
        protected CodeWriter(IndentedTextWriter writer) => this.Writer = writer;
        protected void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }
        protected abstract void Close();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void WriteLine(string text) => this.Writer.WriteLine(text);

        public void Write(string text) => this.Writer.Write(text);

        public CodeWriter WriteBlock(string text) => new BlockWriter(Writer, text);

        public CodeWriter WriteBlockStatement(string text) => new BlockStatementWriter(Writer, text);
        public void WriteIndentedLine(string text)
        {
            ++Writer.Indent;
            Writer.WriteLine(text);
            --Writer.Indent;
        }

        public CodeWriter Indent(string text) => new IndentWriter(Writer, text);

        public CodeWriter WriteRegion(string header) => new RegionWriter(Writer, header);
        public SwitchWriter WriteSwitch(string expression) => new (Writer, expression);
    }


    internal class FileWriter : CodeWriter
    {
        public FileWriter(IndentedTextWriter writer) : base(writer)
        {
        }
        protected override void Close()
        {
        }

    }

    internal class RegionWriter : CodeWriter
    {
        private readonly string header;

        public RegionWriter(IndentedTextWriter writer, string header) : base(writer)
        {
            this.header = header;
            writer.WriteLine($"#region {header}");
        }

        protected override void Close()
        {
            Writer.WriteLine($"#endregion {header}");
        }
    }

    internal sealed class SwitchWriter : IDisposable
    {
        private readonly BlockWriter writer;

        public SwitchWriter(IndentedTextWriter writer, string expression)
        {
            this.writer = new BlockWriter(writer, $"switch({expression})");
        }

        public void WriteCaseLine(string expression, string code, bool isReturn = false, bool isThrow = false)
        {
            this.writer.WriteLine($"case {expression}:");
            this.writer.WriteIndentedLine(code);
            if (isThrow)
                return;
            this.writer.WriteIndentedLine(isReturn ? "return;" : "break;");
        }
        public void WriteDefaultLine(string code, bool isReturn = false, bool isThrow = false)
        {
            this.writer.WriteLine("default:");
            this.writer.WriteIndentedLine(code);
            if (isThrow)
                return;
            this.writer.WriteIndentedLine(isReturn ? "return;" : "break;");
        }

        public void Dispose() => writer.Dispose();
    }
    internal class IndentWriter : CodeWriter
    {
        public IndentWriter(IndentedTextWriter writer, string text) : base(writer)
        {
            writer.WriteLine(text);
            this.Open();
        }
    
        public IndentWriter(IndentedTextWriter writer) : base(writer)
        {
            this.Open();
        }

        private void Open()
        {
            ++Writer.Indent;
        }

        protected override void Close()
        {
            --Writer.Indent;
        }
    }
    internal class BlockWriter : CodeWriter
    {
        public BlockWriter(IndentedTextWriter writer, string text) : base(writer)
        {
            writer.WriteLine(text);
            this.Open();
        }
    
        public BlockWriter(IndentedTextWriter writer) : base(writer)
        {
            this.Open();
        }

        private void Open()
        {
            Writer.WriteLine("{");
            ++Writer.Indent;
        }

        protected override void Close()
        {
            --Writer.Indent;
            Writer.WriteLine("}");
        }
    }

    internal class BlockStatementWriter : BlockWriter
    {
        public BlockStatementWriter(IndentedTextWriter writer, string text) : base(writer, text)
        {
        }

        public BlockStatementWriter(IndentedTextWriter writer) : base(writer)
        {
        }
    
        protected override void Close()
        {
            --Writer.Indent;
            Writer.WriteLine("};");
        }
    }
}