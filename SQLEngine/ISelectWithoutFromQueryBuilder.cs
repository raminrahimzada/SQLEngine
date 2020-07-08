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
        ISelectOrderBuilder OrderBy(string orderFieldName);
        ISelectOrderBuilder OrderByDesc(string orderFieldName);
    }
}