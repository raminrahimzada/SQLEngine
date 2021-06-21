namespace SQLEngine
{
    public interface IDeleteQueryBuilder : IAbstractQueryBuilder
    {
        IDeleteExceptTableNameQueryBuilder Table(string tableName);
        IDeleteExceptTableNameQueryBuilder Table<TTable>() where TTable : ITable, new();
        IDeleteExceptTopQueryBuilder Top(int? count);
    }
}