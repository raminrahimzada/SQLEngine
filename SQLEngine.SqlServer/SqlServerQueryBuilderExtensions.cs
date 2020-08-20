﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    public static class SqlServerQueryBuilderExtensions
    {
        public static SqlServerQueryBuilder AsSqlServer(this IQueryBuilder builder)
        {
            var sqlServerBuilder = builder as SqlServerQueryBuilder;
            if (sqlServerBuilder == null)
            {
                throw new Exception("Builder is not Sql-Server Builder");
            }
            return sqlServerBuilder;
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
            params ISqlExpression[] args)
        {
            int sqlErrorState = Query.Settings.SQLErrorState;
            var list = new List<string>(args.Length + 1) {exceptionMessage.ToSQL().ToSqlString()};
            list.AddRange(args.Select(x => x.ToSqlString()));
            var errorMessageVar = builder.DeclareRandom("EXCEPTION", C.NVARCHARMAX);
            var to = SqlServerLiteral.Raw($"{C.FORMATMESSAGE}({list.JoinWith(", ")})");
            builder.Set(errorMessageVar, to);
            builder.Execute.Function(C.RAISERROR)
                .Arg(errorMessageVar)
                .Arg(18)
                .Arg(sqlErrorState);

            builder.AddExpression($" {C.WITH} {C.NOWAIT}");
        }
    }
}