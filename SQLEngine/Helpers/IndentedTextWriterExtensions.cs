using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
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
        public static void WriteLineComment(this IndentedTextWriter writer, string comment)
        {
            writer.WriteComment(comment);
            writer.WriteLine();
        }
        public static void WriteComment(this IndentedTextWriter writer, string comment)
        {
            writer.Write("/*");
            writer.Write(comment);
            writer.Write("*/");
        }
        public static void WriteScoped(this IndentedTextWriter writer, string expression,
            string beginScope=BEGIN_SCOPE,string endScope=END_SCOPE)
        {
            writer.Write(beginScope);
            writer.Write(expression);
            writer.Write(endScope);
        }
        public static void WriteScoped(this IndentedTextWriter writer, string[] expressionArray)
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
            writer.WriteScoped(expression, SPACE, SPACE);
            //writer.Write(SPACE);
            //writer.Write(expression);
            //writer.Write(SPACE);
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