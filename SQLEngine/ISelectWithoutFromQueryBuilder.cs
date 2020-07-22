namespace SQLEngine
{
    public interface ISelectWithoutFromQueryBuilder : ISelectWithSelectorQueryBuilder, ISelectOrderBuilder
    {
        //IJoinedQueryBuilder InnerJoin(string alias, string tableName
        //    , string mainTableColumnName, string referenceTableColumnName);

        //IJoinedQueryBuilder RightJoin(string alias, string tableName, string mainTableColumnName,
        //    string referenceTableColumnName);

        //IJoinedQueryBuilder LeftJoin(string alias, string tableName,
        //    string mainTableColumnName, string referenceTableColumnName);
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