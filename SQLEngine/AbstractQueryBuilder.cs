using System;

namespace SQLEngine
{
    public abstract class AbstractQueryBuilder : IAbstractQueryBuilder
    {
        protected static ISqlWriter CreateNewWriter()
        {
            return new SqlWriter();
        }
        protected ISqlWriter Writer { get; private set; }


       
        public void Join(AbstractQueryBuilder other)
        {
            Writer = other.Writer;
        }

        public int Indent
        {
            get => Writer.Indent;
            set => Writer.Indent = value;
        }

        protected AbstractQueryBuilder()
        {
            Writer = new SqlWriter();
        }
        protected static T New<T>() where T : AbstractQueryBuilder, new()
        {
            return Activator.CreateInstance<T>();
        }

        protected virtual void ValidateAndThrow()
        {

        }

        public abstract void Build(ISqlWriter writer);

        public string Build()
        {
            using (var writer=SqlWriter.New)
            {
                Build(writer);
                return writer.Build();
            }
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
        /// <summary>
        /// validates the names of identifier 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string I(string name)
        {
            return name;
        }
    }
}
