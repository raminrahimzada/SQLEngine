namespace SQLEngine;

public interface IUpdateQueryBuilder : IAbstractUpdateQueryBuilder
{
    IUpdateNoTableQueryBuilder Table(string tableName, string schema = null);
    IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    IUpdateNoTopQueryBuilder Top(int? count);
}