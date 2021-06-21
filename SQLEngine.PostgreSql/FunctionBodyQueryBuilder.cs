using System;

namespace SQLEngine.PostgreSql
{
    [Obsolete("Do Not Use")]
    internal class FunctionBodyQueryBuilder : PostgreSqlQueryBuilder, IFunctionBodyQueryBuilder
    {
        public AbstractSqlVariable Param(string name)
        {
            return new PostgreSqlVariable(name);
        }
    }
}