namespace SQLEngine
{
    public interface ISelectWithoutWhereQueryBuilder : 
        IAbstractSelectQueryBuilder
    {
        ISelectOrderBuilder OrderBy(ISqlExpression expression);
        ISelectOrderBuilder OrderBy(AbstractSqlColumn column);
        ISelectOrderBuilder OrderBy(string columnName);
        ISelectOrderBuilder OrderByDesc(ISqlExpression expression);
        ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column);
        ISelectOrderBuilder OrderByDesc(string columnName);

        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params ISqlExpression[] expressions);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params AbstractSqlColumn[] columns);
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(params string[] columnNames);
    }
    
}