using System.CodeDom.Compiler;

namespace SQLEngine
{
    public static class IndentedTextWriterExtensions
    {
        public static void WriteWithScoped(this IndentedTextWriter writer, string expression)
        {
            writer.Write("(");
            writer.Write(expression);
            writer.Write(")");
        }
        public static void WriteWithBeginEnd(this IndentedTextWriter writer, string expression)
        {
            writer.WriteLine("BEGIN");
            writer.Indent++;
            writer.WriteLine(expression);
            writer.Indent--;
            writer.WriteLine("END");
        }
    }
}