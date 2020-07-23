using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace SQLEngine
{
    public class SqlWriter:ISqlWriter
    {
        private readonly StringBuilder _stringBuilder;
        private readonly IndentedTextWriter _indentedTextWriter;

        public static ISqlWriter New => new SqlWriter();

        internal SqlWriter()
        {
            _stringBuilder=new StringBuilder();
            _indentedTextWriter = new IndentedTextWriter(new StringWriter(_stringBuilder));
        }

        public override string ToString()
        {
            throw new Exception("Please Call Build() method Instead of ToString()");
        }

        public void Dispose()
        {
            _indentedTextWriter?.Dispose();
        }

        public int Indent
        {
            get => _indentedTextWriter.Indent;
            set => _indentedTextWriter.Indent = value;
        }

        public void Write(string expression)
        {
            _indentedTextWriter.Write(expression);
        }

        public void Write(byte? b)
        {
            _indentedTextWriter.Write(b);
        }

        public void Write(int? b)
        {
            _indentedTextWriter.Write(b);
        }

        public void Write(char? b)
        {
            _indentedTextWriter.Write(b);
        }

        public void Write(byte b)
        {
            _indentedTextWriter.Write(b);
        }

        public void Write(int b)
        {
            _indentedTextWriter.Write(b);
        }

        public void Write(char b)
        {
            _indentedTextWriter.Write(b);
        }

        public void WriteLine(string expression)
        {
            if (expression != null)
                _indentedTextWriter.WriteLine(expression);
            else
                _indentedTextWriter.WriteLine();
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }
    }
}