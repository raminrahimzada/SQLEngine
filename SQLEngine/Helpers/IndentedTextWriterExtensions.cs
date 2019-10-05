using System.CodeDom.Compiler;

namespace SQLEngine.Helpers
{
    public static class IndentedTextWriterExtensions
    {
        public static void WriteWithScoped(this IndentedTextWriter writer, string expression)
        {
            writer.Write("(");
            writer.Write(expression);
            writer.Write(")");
        }
        public static void WriteWithScoped(this IndentedTextWriter writer, string[] expressionArray)
        {
            var expression = string.Join(" , ", expressionArray);
            writer.Write("(");
            writer.Write(expression);
            writer.Write(")");
        }
        public static void Write2(this IndentedTextWriter writer, string expression="")
        {
            writer.Write($" {expression} ");
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