using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    public static class SqlServerQueryBuilderExtensions
    {
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
            ISqlLiteral to = SqlServerLiteral.Raw($"{SQLKeywords.FORMATMESSAGE}({list.JoinWith(", ")})");
            builder.Set(errorMessageVar, to);
            builder.AddExpression($"{SQLKeywords.RAISERROR}({errorMessageVar}, 18, {SQL_ERROR_STATE}) {SQLKeywords.WITH} {SQLKeywords.NOWAIT}");
        }
    }
}