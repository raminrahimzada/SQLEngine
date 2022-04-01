namespace SQLEngine;

public interface IDeleteQueryBuilder : IAbstractQueryBuilder
{
    IDeleteExceptTableNameQueryBuilder Table(string tableName,string schemaName=null);
    IDeleteExceptTableNameQueryBuilder Table<TTable>() where TTable : ITable, new();
    IDeleteExceptTopQueryBuilder Top(int? count);
}