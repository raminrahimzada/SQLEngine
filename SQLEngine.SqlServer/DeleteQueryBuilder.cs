namespace SQLEngine.SqlServer
{
    internal sealed class DeleteQueryBuilder : SqlServerQueryBuilder,
        IDeleteExceptTableNameQueryBuilder,
        IDeleteExceptTopQueryBuilder,
        IDeleteExceptWhereQueryBuilder
    {
        private string _tableName;
        private int? _topClause;
        private string _whereCondition;
        public IDeleteExceptTableNameQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public IDeleteExceptTopQueryBuilder Top(int? count)
        {
            _topClause = count;
            return this;
        }
        public IDeleteExceptWhereQueryBuilder Where(string condition)
        {
            _whereCondition = condition;
            return this;
        }
        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write2(SQLKeywords.DELETE);
            if (_topClause != null)
            {
                Writer.Write(SQLKeywords.TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write2();
            }
            Writer.Write2(SQLKeywords.FROM);

            Writer.Write(I(_tableName));

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                Writer.Write2(SQLKeywords.WHERE);
                Writer.WriteScoped(_whereCondition);
            }
            return base.Build();
        }
    }
}