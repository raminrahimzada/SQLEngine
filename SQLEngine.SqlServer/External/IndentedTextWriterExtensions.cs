using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal static class SqlWriterExtensions
    {
        public static void WriteWithScoped(this ISqlWriter writer, string expression)
        {
            writer.BeginScope();
            writer.Write(expression);
            writer.EndScope();
        }
        public static void BeginScope(this ISqlWriter writer)
        {
            writer.Write(C.BEGIN_SCOPE);
        }
        public static void EndScope(this ISqlWriter writer)
        {
            writer.Write(C.END_SCOPE);
        }
        public static void WriteLineComment(this ISqlWriter writer, string comment)
        {
            writer.WriteComment(comment);
            writer.WriteLine();
        }
        public static void WriteComment(this ISqlWriter writer, string comment)
        {
            writer.Write("/*");
            writer.Write(comment);
            writer.Write("*/");
        }
        public static void WriteScoped(this ISqlWriter writer, string expression,
            string beginScope = C.BEGIN_SCOPE, string endScope = C.END_SCOPE)
        {
            writer.Write(beginScope);
            writer.Write(expression);
            writer.Write(endScope);
        }
        public static void WriteJoined(this ISqlWriter writer, IEnumerable<string> expressionArray, string joiner = " , ", bool useNewLine = false)
        {
            var first = true;
            foreach (var expression in expressionArray)
            {
                if (!first)
                {
                    writer.Write(joiner);
                    if (useNewLine)
                    {
                        writer.WriteLine();
                    }
                }
                writer.Write(expression);
                first = false;
            }
        }

        public static void WriteNewLine(this ISqlWriter writer)
        {
            writer.Write("\r\n\t");
        }

        public static void WriteLineJoined(this ISqlWriter writer, IEnumerable<string> expressions)
        {
            var expression = string.Join(" , ", expressions);
            writer.WriteLine(expression);
        }

        public static void WriteLineEx(this ISqlWriter writer, string expression)
        {
            writer.WriteEx(expression);
            writer.WriteLine();
        }
        public static void WriteEx(this ISqlWriter writer, string expression)
        {
            if (string.IsNullOrEmpty(expression)) return;
            for (var i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == '\r' && expression[i + 1] == '\n')
                {
                    writer.WriteLine();
                    i++;
                }
                else
                {
                    writer.Write(expression[i]);
                }
            }

            if (expression[expression.Length - 1] != '\n')
                writer.Write(expression[expression.Length - 1]);
        }
        public static void Write2(this ISqlWriter writer, string expression = "")
        {
            writer.WriteScoped(expression, C.SPACE, C.SPACE);
        }
    }
}