namespace SQLEngine.SqlServer
{
    internal class FunctionBodyQueryBuilder : SqlServerQueryBuilder, IFunctionBodyQueryBuilder
    {
        public AbstractSqlVariable Param(string name)
        {
            return new SqlServerVariable(name);
        }
    }
}