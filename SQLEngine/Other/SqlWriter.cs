﻿using System;
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

        [Obsolete("Do not use",true)]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override string ToString()
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
        {
            return Build();
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

        public void Write(params string[] expressions)
        {
            foreach (var expression in expressions)
            {
                _indentedTextWriter.Write(expression);
            }
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

        public void WriteLine(string expression=null)
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