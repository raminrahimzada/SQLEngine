namespace SQLEngine;

public interface IColumnQueryBuilder : IAbstractQueryBuilder
{
    ColumnModel Model { get; }
    IColumnQueryBuilder CalculatedColumn(AbstractSqlExpression expression, bool? isPersisted = true);
    IColumnQueryBuilder Check(string checkExpression);
    IColumnQueryBuilder Check(AbstractSqlCondition condition);
    IColumnQueryBuilder Check(ISqlExpression expression);
    IColumnQueryBuilder DefaultValue(ISqlExpression defaultValue, string defaultConstraintName = null);
    IColumnQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultConstraintName = null);
    IColumnQueryBuilder Description(string description);
    IColumnQueryBuilder ForeignKey(string tableName, string schemaName, string columnName, string fkName = null);
    IColumnQueryBuilder Identity(int start = 1, int step = 1);
    IColumnQueryBuilder MaxLength(int? maxLen);
    IColumnQueryBuilder Name(string name);
    IColumnQueryBuilder NotNull(bool notNull = true);
    IColumnQueryBuilder Precision(byte precision);
    IColumnQueryBuilder Primary(string keyName = null);
    IColumnQueryBuilder Scale(byte scale);
    IColumnQueryBuilder Type(string type);
    IColumnQueryBuilder Unique(string keyName = null, bool descending = false);
}