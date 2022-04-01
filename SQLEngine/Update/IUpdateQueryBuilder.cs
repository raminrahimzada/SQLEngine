namespace SQLEngine;

public interface IUpdateQueryBuilder : IAbstractUpdateQueryBuilder
{
    IUpdateNoTopQueryBuilder Top(int? count);
    IUpdateNoTableQueryBuilder Table(string tableName, string schema = null);
    IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
}