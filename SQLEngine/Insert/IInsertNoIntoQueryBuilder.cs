namespace SQLEngine;

public interface IInsertNoIntoQueryBuilder : IInsertWithValuesQueryBuilder
{
    IInsertNoIntoWithColumns Columns(params string[] columnNames);
    IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
    IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
}