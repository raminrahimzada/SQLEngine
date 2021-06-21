using System;

namespace SQLEngine.PostgreSql
{
    public static class PostgreSqlQueryBuilderExtensions
    {
        [Obsolete("Do not use")]
        public static PostgreSqlQueryBuilder AsPostgreSql(this IQueryBuilder builder)
        {
            if (!(builder is PostgreSqlQueryBuilder sqlServerBuilder))
            {
                throw new InvalidCastException("Builder is not Sql-Server Builder");
            }
            return sqlServerBuilder;
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