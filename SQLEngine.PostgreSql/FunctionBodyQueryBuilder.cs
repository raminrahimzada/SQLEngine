using System;

namespace SQLEngine.PostgreSql
{
    [Obsolete]
    internal class FunctionBodyQueryBuilder : PostgreSqlQueryBuilder, IFunctionBodyQueryBuilder
    {
        public AbstractSqlVariable Param(string name)
        {
            return new PostgreSqlVariable(name);
        }
    }
}