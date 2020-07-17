namespace SQLEngine.SqlServer
{
    internal class DropQueryBuilder : AbstractQueryBuilder, IDropQueryBuilder, IDropTableQueryBuilder
    {
        public IDropTableNoNameQueryBuilder Table(string tableName)
        {
            return GetDefault<DropTableQueryBuilder>().Table(tableName);
        }

        public IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable,new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name);
            }
        }

        public IDropFunctionQueryBuilder Function(string funcName)
        {
            return GetDefault<DropFunctionQueryBuilder>().FunctionName(funcName);
        }

        public IDropViewNoNameQueryBuilder View(string viewName)
        {
            return GetDefault<DropViewQueryBuilder>().View(viewName);
        }

        public IDropDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            return GetDefault<DropDatabaseQueryBuilder>().Database(databaseName);
        }
    }
}