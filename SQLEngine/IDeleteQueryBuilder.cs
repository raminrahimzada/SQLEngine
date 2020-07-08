namespace SQLEngine
{
    public interface IDeleteQueryBuilder : IAbstractQueryBuilder
    {
        IDeleteExceptTableNameQueryBuilder Table(string tableName);
        IDeleteExceptTopQueryBuilder Top(int? count);
    }
}