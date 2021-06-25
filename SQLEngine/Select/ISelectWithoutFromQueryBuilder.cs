namespace SQLEngine
{
    public interface ISelectWithoutFromQueryBuilder : ISelectOrderBuilder, ISelectWhereQueryBuilder, IJoinedQueryBuilder
    {
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        
        ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(string columnName);
        ISelectOrderBuilder OrderBy(string columnName);
        
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlColumn column);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(string columnName);
    }
}