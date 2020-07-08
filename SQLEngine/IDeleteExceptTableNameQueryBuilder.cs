namespace SQLEngine
{
    public interface IDeleteExceptTableNameQueryBuilder : IDeleteQueryBuilder
    {
        IDeleteExceptWhereQueryBuilder Where(string condition);
    }
}