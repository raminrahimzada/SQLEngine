using System;

namespace SQLEngine.PostgreSql
{
    [Obsolete("Do Not Use")]
    internal class PostgreSqlProcedureBodyQueryBuilder : PostgreSqlQueryBuilder, IProcedureBodyQueryBuilder
    {
        public AbstractSqlVariable Parameter(string name)
        {
            return new PostgreSqlVariable(name);
        }
    }
}