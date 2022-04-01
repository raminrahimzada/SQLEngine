namespace SQLEngine;

public interface IInsertQueryBuilder : IAbstractInsertQueryBuilder
{
    IInsertNoIntoQueryBuilder Into(string tableName, string schemaName = null);
    IInsertNoIntoQueryBuilder Into<TTable>() where TTable : ITable, new();
}