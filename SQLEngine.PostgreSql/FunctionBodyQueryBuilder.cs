namespace SQLEngine.PostgreSql
{
    internal class FunctionBodyQueryBuilder : PostgreSqlQueryBuilder, IFunctionBodyQueryBuilder
    {
        public AbstractSqlVariable Param(string name)
        {
            return new PostgreSqlVariable(name);
        }
    }
}