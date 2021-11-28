namespace SQLEngine
{
    public interface IJoinedQueryBuilder<TTable> : IJoinedQueryBuilder, ISelectWhereQueryBuilder<TTable>
    {

    }
    public interface IJoinedQueryBuilder : ISelectWhereQueryBuilder
    {
        IJoinedNeedsOnQueryBuilder InnerJoin(string targetTableName,string schema, string targetTableAlias);
        IJoinedNeedsOnQueryBuilder InnerJoin<TTable>(string targetTableAlias) where TTable : ITable, new();

        IJoinedNeedsOnQueryBuilder LeftJoin(string targetTableName, string schema, string targetTableAlias);
        IJoinedNeedsOnQueryBuilder LeftJoin<TTable>(string targetTableAlias) where TTable : ITable, new();


        IJoinedNeedsOnQueryBuilder RightJoin(string targetTableName, string schema, string targetTableAlias);
        IJoinedNeedsOnQueryBuilder RightJoin<TTable>(string targetTableAlias) where TTable : ITable, new();
    }

    public interface IJoinedNeedsOnQueryBuilder<TTable> : IJoinedNeedsOnQueryBuilder
    {

    }
    public interface IJoinedNeedsOnQueryBuilder : IAbstractQueryBuilder
    {
        IJoinedNeedsOnEqualsToQueryBuilder OnColumn(string targetTableColumn, string targetTableAlias);
        /// <summary>
        /// Gets targetTableAlias from last joined tables alias
        /// </summary>
        /// <param name="targetTableColumn"></param>
        /// <returns></returns>
        IJoinedNeedsOnEqualsToQueryBuilder OnColumn(string targetTableColumn);


        IJoinedQueryBuilder On(AbstractSqlCondition condition);
    }

    public interface IJoinedNeedsOnEqualsToQueryBuilder : IAbstractQueryBuilder
    {
        /// <summary>
        /// If sourceTableAlias not specified then we will use main table alias
        /// </summary>
        /// <param name="sourceTableColumnName"></param>
        /// <returns></returns>
        IJoinedQueryBuilder IsEqualsTo(string sourceTableColumnName);
        IJoinedQueryBuilder IsEqualsTo(string sourceTableColumnName, string sourceTableAlias);
    }
}