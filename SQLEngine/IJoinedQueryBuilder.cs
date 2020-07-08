namespace SQLEngine
{
    public interface IJoinedQueryBuilder : ISelectWhereQueryBuilder
    {
        IJoinedQueryBuilder InnerJoin(string alias, string tableName
            , string mainTableColumnName, string referenceTableColumnName);
        
        IJoinedQueryBuilder InnerJoinRaw(string alias, string tableName
            , string condition);

        IJoinedQueryBuilder RightJoin(string alias, string tableName, string mainTableColumnName,
            string referenceTableColumnName);

        IJoinedQueryBuilder LeftJoin(string alias, string tableName,
            string mainTableColumnName, string referenceTableColumnName);
    }
}