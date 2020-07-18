namespace SQLEngine
{
    public interface ISelectWithoutWhereQueryBuilder : IAbstractSelectQueryBuilder
    {
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
    }
}