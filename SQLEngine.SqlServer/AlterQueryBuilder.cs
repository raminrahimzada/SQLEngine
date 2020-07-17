namespace SQLEngine.SqlServer
{
    internal class AlterQueryBuilder : AbstractQueryBuilder,IAlterQueryBuilder
    {
        public IAlterTableNoNameQueryBuilder Table(string tableName)
        {
            return GetDefault<AlterTableQueryBuilder>().TableName(tableName);
        }

        public IAlterTableNoNameQueryBuilder Table<TTable>() where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name);
            }
        }
    }
}