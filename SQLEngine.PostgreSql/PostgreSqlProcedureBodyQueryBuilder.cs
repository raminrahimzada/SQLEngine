namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlProcedureBodyQueryBuilder : PostgreSqlQueryBuilder, IProcedureBodyQueryBuilder
    {
        public AbstractSqlVariable Parameter(string name)
        {
            return new PostgreSqlVariable(name);
        }
    }
}