using System.CodeDom.Compiler;
using System.Collections.Generic;
using static SQLEngine.SQLKeywords;
namespace SQLEngine.Helpers
{
    public static class IndentedTextWriterExtensions
    {
        public static void BeginScope(this IndentedTextWriter writer)
        {
            writer.Write(BEGIN_SCOPE);
        }
        public static void EndScope(this IndentedTextWriter writer)
        {
            writer.Write(END_SCOPE);
        }
        public static void WriteWithScoped(this IndentedTextWriter writer, string expression)
        {
            writer.Write(BEGIN_SCOPE);
            writer.Write(expression);
            writer.Write(END_SCOPE);
        }
        public static void WriteWithScoped(this IndentedTextWriter writer, string[] expressionArray)
        {
            var expression = string.Join(" , ", expressionArray);
            writer.Write(BEGIN_SCOPE);
            writer.Write(expression);
            writer.Write(END_SCOPE);
        }
        public static void WriteJoined(this IndentedTextWriter writer, string[] expressionArray)
        {
            var expression = string.Join(" , ", expressionArray);
            writer.Write(expression);
        }

        public static void WriteNewLine(this IndentedTextWriter writer)
        {
            writer.Write("\r");
        }

        public static void WriteLineJoined(this IndentedTextWriter writer, IEnumerable<string> expressions)
        {
            var expression = string.Join(" , ", expressions);
            writer.WriteLine(expression);
        }
        public static void Write2(this IndentedTextWriter writer, string expression="")
        {
            writer.Write($" {expression} ");
        }
        public static void WriteWithBeginEnd(this IndentedTextWriter writer, string expression)
        {
            writer.WriteLine(BEGIN);
            writer.Indent++;
            writer.WriteLine(expression);
            writer.Indent--;
            writer.WriteLine(END);
        }

    }
}