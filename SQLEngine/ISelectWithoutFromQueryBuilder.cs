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
        
        ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression);
        ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(ISqlExpression expression);
    }
}