using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    public static class SqlServerQueryBuilderExtensions
    {
        public static IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(this IUpdateNoTableSingleValueQueryBuilder builder,
            string columnName, SqlServerLiteral right)
        {
            return builder.WhereColumnEquals(columnName, right);
        }
        public static IUpdateNoTableSingleValueQueryBuilder Value(
            this IUpdateNoTableSingleValueQueryBuilder builder,
            string columnName, SqlServerLiteral columnValue)
        {
            return builder.Value(columnName, columnValue);
        }
        public static IUpdateNoTableSingleValueQueryBuilder Value(this IUpdateNoTableQueryBuilder builder,
            string columnName, 
            SqlServerLiteral columnValue)
        {
            return builder.Value(columnName, columnValue);
        }
        public static IInsertNeedValueQueryBuilder Value(this IInsertNoIntoQueryBuilder builder, 
            string columnName, SqlServerLiteral columnValue)
        {
            return builder.Value(columnName, (AbstractSqlLiteral)columnValue);
        }
        public static IInsertNeedValueQueryBuilder Value(this IInsertNeedValueQueryBuilder builder, 
            string columnName, SqlServerLiteral columnValue)
        {
            return builder.Value(columnName, (AbstractSqlLiteral)columnValue);
        }

        public static AbstractSqlVariable Declare(this IQueryBuilder builder, 
            string variableName, string type, SqlServerLiteral defaultValue = null)
        {
            return builder.Declare(variableName, type, defaultValue);
        }
        public static void Set(this IQueryBuilder builder, AbstractSqlVariable variable, SqlServerLiteral value)
        {
            builder.Set(variable, (AbstractSqlLiteral) value);
        }

        public static string ColumnGreaterThan(this IConditionFilterQueryHelper helper, string columnName, SqlServerLiteral value)
        {
            return helper.ColumnGreaterThan(columnName, value);
        }

        public static ISelectWithoutWhereQueryBuilder WhereColumnEquals(this ISelectWhereQueryBuilder builder,
            string columnName, SqlServerLiteral right)
        {
            return builder.WhereColumnEquals(columnName, right);
        }
        public static void SetNoCountOn(this SqlServerQueryBuilder builder)
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
            params string[] args)
        {
            const int SQL_ERROR_STATE = 47;
            var list = new List<string>(args.Length + 1) {exceptionMessage.ToSQL().ToSqlString()};
            list.AddRange(args);
            var errorMessageVar = builder.DeclareRandom("EXCEPTION", SQLKeywords.NVARCHARMAX);
            AbstractSqlLiteral to = SqlServerLiteral.Raw($"{SQLKeywords.FORMATMESSAGE}({list.JoinWith(", ")})");
            builder.Set(errorMessageVar, to);
            builder.AddExpression($"{SQLKeywords.RAISERROR}({errorMessageVar}, 18, {SQL_ERROR_STATE}) {SQLKeywords.WITH} {SQLKeywords.NOWAIT}");
        }
    }
}