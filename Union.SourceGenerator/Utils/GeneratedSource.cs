#nullable enable

namespace Union.SourceGenerator
{
    internal class GeneratedSource
    {
        public GeneratedSource(string sourceCode, string fileName)
        {
            this.SourceCode = sourceCode;
            this.FileName = fileName + ".generated";
        }
        public string SourceCode { get; set; }
        public string FileName { get; set; }
    }
}