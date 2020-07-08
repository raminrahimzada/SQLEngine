namespace SQLEngine.SqlServer
{
    internal class DropQueryBuilder : SqlServerQueryBuilder, IDropQueryBuilder, IDropTableQueryBuilder
    {
        public IDropTableNoNameQueryBuilder Table(string tableName)
        {
            return GetDefault<DropTableQueryBuilder>().Table(tableName);
        }

        public IDropFunctionQueryBuilder Function(string funcName)
        {
            return GetDefault<DropFunctionQueryBuilder>().FunctionName(funcName);
        }

        public IDropViewQueryBuilder View(string viewName)
        {
            return GetDefault<DropViewQueryBuilder>().View(viewName);
        }

         
    }
}