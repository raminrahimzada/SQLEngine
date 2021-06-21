using System;

namespace SQLEngine.PostgreSql
{
    [Obsolete("Do Not Use")]
    public class CatchFunctionQueryBuilder : PostgreSqlQueryBuilder, ICatchFunctionQueryBuilder
    {
        public ISqlExpression ErrorNumber()
        {
            return new PostgreSqlRawExpression("ERROR_NUMBER()");
        }

        public ISqlExpression ErrorMessage()
        {
            return new PostgreSqlRawExpression("ERROR_MESSAGE()");
        }
    }
}