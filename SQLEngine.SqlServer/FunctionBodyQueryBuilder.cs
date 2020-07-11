namespace SQLEngine.SqlServer
{
    public class FunctionBodyQueryBuilder : SqlServerQueryBuilder, IFunctionBodyQueryBuilder
    {
        public AbstractSqlVariable Param(string name)
        {
            return new SqlServerVariable(name);
        }
    }
}