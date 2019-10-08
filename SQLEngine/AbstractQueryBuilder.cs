using SQLEngine.Builders;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text;

namespace SQLEngine
{
    public abstract class AbstractQueryBuilder : IDisposable
    {
        protected IndentedTextWriter Writer { get; private set; }
        private readonly StringBuilder _stringBuilder;
        public int Indent
        {
            get => Writer.Indent;
            set => Writer.Indent = value;
        }
        /// <summary>
        /// Identifize the variable name
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static string I(string variableName)
        {
            bool shouldBeSwapped;
            if (variableName.Contains(SQLKeywords.SPACE))
            {
                shouldBeSwapped = true;
            }
            else
            if (SQLKeywords.AllKeywords.Any(k => k == variableName))
            {
                shouldBeSwapped = true;
            }
            else
            {
                shouldBeSwapped = false;
            }
            if (shouldBeSwapped)
            {
                return SQLKeywords.BEGIN_SQUARE + variableName + SQLKeywords.END_SQUARE;
            }

            return variableName;
        }
        public static string True => "(1=1)";
        public static string False => "(1=0)";

        protected AbstractQueryBuilder()
        {
            _stringBuilder = new StringBuilder();
            StringWriter output = new StringWriter(_stringBuilder);
            Writer = new IndentedTextWriter(output);
        }
        protected virtual AbstractQueryBuilder GetDefault()
        {
            return new IfElseQueryBuilder();
        }
        protected static T GetDefault<T>() where T : AbstractQueryBuilder, new()
        {
            T result = Activator.CreateInstance<T>();
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