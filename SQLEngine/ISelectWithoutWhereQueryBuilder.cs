namespace SQLEngine
{
    public interface ISelectWithoutWhereQueryBuilder : IAbstractSelectQueryBuilder
    {
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
    }
}