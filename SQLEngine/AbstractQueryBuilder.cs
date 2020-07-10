using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace SQLEngine
{
    public abstract class AbstractQueryBuilder : IAbstractQueryBuilder
    {
        protected IndentedTextWriter Writer { get; private set; }

        private StringBuilder _stringBuilder;

        public void Join(AbstractQueryBuilder other)
        {
            Writer = other.Writer;
            _stringBuilder = other._stringBuilder;
        }

        public int Indent
        {
            get => Writer.Indent;
            set => Writer.Indent = value;
        }
        protected AbstractQueryBuilder()
        {
            _stringBuilder = new StringBuilder();
            var output = new StringWriter(_stringBuilder);
            Writer = new IndentedTextWriter(output);
        }
        protected static T GetDefault<T>() where T : AbstractQueryBuilder, new()
        {
            var result = Activator.CreateInstance<T>();
            //TODO add extra here
            return result;
        }

        protected virtual void ValidateAndThrow()
        {

        }
        public virtual string Build()
        {
            ValidateAndThrow();
            return _stringBuilder.ToString();
        }

        public override string ToString()
        {
            return Build();
        }

        

        public virtual void Dispose()
        {
            Writer?.Dispose();
        }

        protected Exception Bomb(string message = "")
        {
            if (string.IsNullOrEmpty(message)) message = "Invalid Usage of QueryBuilder: " + GetType().Name;
            throw new Exception(message);
        }

    }
}
