namespace SQLEngine.PostgreSql
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

        public IDeleteExceptTableNameQueryBuilder Table<TTable>() where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name);
            }
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

        public IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression expression)
        {
            var col = new PostgreSqlColumn(columnName);
            _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, expression.ToSqlString());
            return this;
        }

        public IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal)
        {
            var col = new PostgreSqlColumn(columnName);
            _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, literal.ToSqlString());
            return this;
        }

        public IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable)
        {
            var col = new PostgreSqlColumn(columnName);
            _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, variable.ToSqlString());
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();
            writer.Write(C.DELETE);
            writer.Write(C.SPACE);
            if (_topClause != null)
            {
                writer.Write(C.TOP);
                writer.WriteScoped(_topClause.Value.ToString());
                writer.Write2();
            }
            writer.Write2(C.FROM);

            writer.Write(I(_tableName));

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                writer.Write2(C.WHERE);
                writer.WriteScoped(_whereCondition);
            }
        }
    }
}