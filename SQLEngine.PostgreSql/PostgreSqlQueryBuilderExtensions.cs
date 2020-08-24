using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.PostgreSql
{
    public static class PostgreSqlQueryBuilderExtensions
    {
        public static PostgreSqlQueryBuilder AsPostgreSql(this IQueryBuilder builder)
        {
            var sqlServerBuilder = builder as PostgreSqlQueryBuilder;
            if (sqlServerBuilder == null)
            {
                throw new Exception("Builder is not Sql-Server Builder");
            }
            return sqlServerBuilder;
        }

        public static void SetNoCountOn(this PostgreSqlQueryBuilder builder)
        {
            builder.AddExpression("SET NOCOUNT ON;");
        }
        /// <summary>
        /// Throws the exception.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="args">The arguments.</param>
        public static void ThrowException(this IQueryBuilder builder, string exceptionMessage,
            params ISqlExpression[] args)
        {
            throw new NotImplementedException();
        }
    }
}