namespace SQLEngine
{
    
    public interface IColumnQueryBuilder : IAbstractQueryBuilder
    {
        IColumnQueryBuilder NotNull();
        IColumnQueryBuilder Name(string name);
        IColumnQueryBuilder Description(string description);
        IColumnQueryBuilder Precision(byte precision);
        IColumnQueryBuilder Scale(byte scale);
        IColumnQueryBuilder Type(string type);
        IColumnQueryBuilder ForeignKey(string tableName, string columnName, string fkName = null);
        IColumnQueryBuilder MaxLength(int? maxLen);
        IColumnQueryBuilder Unique(string keyName = null, bool descending = false);
        IColumnQueryBuilder DefaultValue(ISqlExpression defaultValue, string defaultConstraintName = null);
        IColumnQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultConstraintName = null);
        IColumnQueryBuilder CalculatedColumn(AbstractSqlExpression expression, bool? isPersisted = true);
        IColumnQueryBuilder Identity(int start = 1, int step = 1);
        IColumnQueryBuilder Primary(string keyName = null);
        IColumnQueryBuilder Check(string checkExpression);
        IColumnQueryBuilder Check(AbstractSqlCondition condition);
        IColumnQueryBuilder Check(ISqlExpression expression);
        ColumnModel Model { get; }
    }
}