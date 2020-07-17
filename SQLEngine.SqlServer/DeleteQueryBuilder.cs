namespace SQLEngine.SqlServer
{
    internal sealed class DeleteQueryBuilder : AbstractQueryBuilder,
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
        public IDeleteExceptWhereQueryBuilder Where(AbstractSqlCondition condition)
        {
            _whereCondition = condition.ToSqlString();
            return this;
        }
        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write2(C.DELETE);
            if (_topClause != null)
            {
                Writer.Write(C.TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write2();
            }
            Writer.Write2(C.FROM);

            Writer.Write(I(_tableName));

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                Writer.Write2(C.WHERE);
                Writer.WriteScoped(_whereCondition);
            }
            return base.Build();
        }
    }
}