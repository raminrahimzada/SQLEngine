namespace SQLEngine.SqlServer;

internal sealed class DeleteQueryBuilder : AbstractQueryBuilder,
    IDeleteExceptTableNameQueryBuilder,
    IDeleteExceptTopQueryBuilder,
    IDeleteExceptWhereQueryBuilder
{
    private string _tableName;
    private string _schema;
    private int? _topClause;
    private string _whereCondition;
    public IDeleteExceptTableNameQueryBuilder Table(string tableName, string schemaName = null)
    {
        _tableName = tableName;
        _schema = schemaName;
        return this;
    }

    public IDeleteExceptTableNameQueryBuilder Table<TTable>() where TTable : ITable, new()
    {
        using(var table = new TTable())
        {
            return Table(table.Name, table.Schema);
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
        var col = new SqlServerColumn(columnName);
        _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, expression.ToSqlString());
        return this;
    }

    public IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal)
    {
        var col = new SqlServerColumn(columnName);
        _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, literal.ToSqlString());
        return this;
    }

    public IDeleteExceptWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable)
    {
        var col = new SqlServerColumn(columnName);
        _whereCondition = string.Concat(col.ToSqlString(), C.EQUALS, variable.ToSqlString());
        return this;
    }

    public override void Build(ISqlWriter writer)
    {
        ValidateAndThrow();
        writer.Write(C.DELETE);
        writer.Write(C.SPACE);
        if(_topClause != null)
        {
            writer.Write(C.TOP);
            writer.WriteScoped(_topClause.Value.ToString());
            writer.Write2();
        }

        writer.Write2(C.FROM);
        if(!string.IsNullOrWhiteSpace(_schema))
        {
            writer.Write(_schema);
            writer.Write(C.DOT);
        }
        writer.Write(I(_tableName));

        if(!string.IsNullOrEmpty(_whereCondition))
        {
            writer.Write2(C.WHERE);
            writer.WriteScoped(_whereCondition);
        }

        writer.WriteLine();
    }
}