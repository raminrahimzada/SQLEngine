namespace SQLEngine
{
    public interface IDeleteExceptTableNameQueryBuilder : IDeleteQueryBuilder
    {
        IDeleteExceptWhereQueryBuilder Where(AbstractSqlCondition condition);
    }
}