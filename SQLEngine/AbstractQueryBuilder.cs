using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace SQLEngine
{
    public abstract class AbstractQueryBuilder:IDisposable
    {
        protected IndentedTextWriter Writer { get; private set; }
        private readonly StringBuilder _stringBuilder;
        public int Indent
        {
            get => Writer.Indent;
            set => Writer.Indent = value;
        }

        public static string True => "(1=1)";
        public static string False => "(1=0)";

        protected AbstractQueryBuilder()
        {
            _stringBuilder=new StringBuilder();
            var output = new StringWriter(_stringBuilder);
            Writer = new IndentedTextWriter(output);
        }
        protected virtual AbstractQueryBuilder GetDefault()
        {
            return new IfElseQueryBuilder();
        }
        protected static T GetDefault<T>() where T: AbstractQueryBuilder,new()
        {
            var result = Activator.CreateInstance<T>();
            //TODO add extra here
            return result;
        }
        
        public AbstractQueryBuilder FromRawQuery(string query)
        {
            Writer.Write(query);
            return this;
        }

        protected virtual void ValidateAndThrow()
        {

        }
        public virtual string Build()
        {
            ValidateAndThrow();
            return _stringBuilder.ToString();
        }

        public virtual void Dispose()
        {
            Writer?.Dispose();
        }
        protected void Boom()
        {
            throw new Exception("Invalid Usage of class");
        }
    }
}