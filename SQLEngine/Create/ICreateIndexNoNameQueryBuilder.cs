namespace SQLEngine;

public interface ICreateIndexNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateIndexNoTableNameQueryBuilder OnTable(string tableName);
    ICreateIndexNoTableNameQueryBuilder OnTable<TTable>() where TTable:ITable,new();
}