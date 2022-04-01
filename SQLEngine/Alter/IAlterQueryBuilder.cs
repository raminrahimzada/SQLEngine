namespace SQLEngine;

public interface IAlterQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameQueryBuilder Table(string tableName, string schemaName = null);
    IAlterTableNoNameQueryBuilder Table<TTable>() where TTable:ITable,new();
}