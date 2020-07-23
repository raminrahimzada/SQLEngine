namespace SQLEngine
{
    public interface ISelectWithoutFromQueryBuilder : ISelectWithSelectorQueryBuilder, ISelectOrderBuilder
    {
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        
        
        ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
        
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(ISqlExpression expression);

        ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlColumn column);
        ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(AbstractSqlColumn column);


        
    }
}