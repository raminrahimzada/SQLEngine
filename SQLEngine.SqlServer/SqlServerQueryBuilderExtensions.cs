using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    public static class SqlServerQueryBuilderExtensions
    {
        private static string DetectSqlType<T>()
        {
            var type = typeof(T);
            {
                if (type == typeof(int))
                {
                    return (SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(uint))
                {
                    return (SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(long))
                {
                    return (SQLKeywords.BIGINT);
                }
            }
            {
                if (type == typeof(ulong))
                {
                    return (SQLKeywords.BIGINT);
                }
            }

            {
                if (type == typeof(byte))
                {
                    return (SQLKeywords.TINYINT);
                }
            }
            {
                if (type == typeof(Guid))
                {
                    return (SQLKeywords.UNIQUEIDENTIFIER);
                }
            }
            {
                if (type == typeof(DateTime))
                {
                    return (SQLKeywords.DATETIME);
                }
            }
            {
                if (type == typeof(string))
                {
                    return (SQLKeywords.NVARCHARMAX);
                }
            }
            throw new Exception("Complex type " + type.FullName + " cannot be converted to sql type");
        }


        public static ISelectWithSelectorQueryBuilder SelectLiteral(this ISelectWithSelectorQueryBuilder builder
            ,SqlServerLiteral literal)
        {
            return builder.Select(literal);
        }
        public static ISelectWithSelectorQueryBuilder Select(this ISelectWithSelectorQueryBuilder builder
            ,string columnName)
        {
            return builder.Select(new SqlServerColumn(columnName));
        }
        public static ISelectWithSelectorQueryBuilder Select(this ISelectWithSelectorQueryBuilder builder
            ,AbstractSqlVariable variable)
        {
            return builder.Select(variable);
        }
        public static void Return(this IProcedureBodyQueryBuilder builder, SqlServerLiteral literal)
        {
            builder.Return(literal);
        }
        public static void Return(this IFunctionBodyQueryBuilder builder, SqlServerLiteral literal)
        {
            builder.Return(literal);
        }
        public static ICreateProcedureWithArgumentQueryBuilder Parameter<T>(this ICreateProcedureQueryBuilder builder,string argName)
        {
            var typeSql = DetectSqlType<T>();
            return builder.Parameter(argName, typeSql);
        }
        public static ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(
            this ICreateProcedureQueryBuilder builder,string argName)
        {
            var typeSql = DetectSqlType<T>();
            return builder.ParameterOut(argName, typeSql);
        }
        public static ICreateProcedureWithArgumentQueryBuilder Parameter<T>(this ICreateProcedureWithArgumentQueryBuilder builder,string argName)
        {
            var typeSql = DetectSqlType<T>();
            return builder.Parameter(argName, typeSql);
        }
        public static ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(
            this ICreateProcedureWithArgumentQueryBuilder builder,string argName)
        {
            var typeSql = DetectSqlType<T>();
            return builder.ParameterOut(argName, typeSql);
        }
        public static ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns<T>(
            this ICreateFunctionNoNameQueryBuilder builder)
        {
            var typeSql = DetectSqlType<T>();
            return builder.Returns(typeSql);
        }
        public static ICreateFunctionNoNameQueryBuilder Parameter<T>(this ICreateFunctionNoNameQueryBuilder builder,
            string paramName)
        {
            var type = typeof(T);
            {
                if (type == typeof(int))
                {
                    return builder.Parameter(paramName, SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(uint))
                {
                    return builder.Parameter(paramName, SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(long))
                {
                    return builder.Parameter(paramName, SQLKeywords.BIGINT);
                }
            }
            {
                if (type == typeof(ulong))
                {
                    return builder.Parameter(paramName, SQLKeywords.BIGINT);
                }
            }

            {
                if (type == typeof(byte))
                {
                    return builder.Parameter(paramName, SQLKeywords.TINYINT);
                }
            }
            {
                if (type == typeof(Guid))
                {
                    return builder.Parameter(paramName, SQLKeywords.UNIQUEIDENTIFIER);
                }
            }
            {
                if (type == typeof(DateTime))
                {
                    return builder.Parameter(paramName, SQLKeywords.DATETIME);
                }
            }
            {
                if (type == typeof(string))
                {
                    return builder.Parameter(paramName, SQLKeywords.NVARCHARMAX);
                }
            }
            throw new Exception("Complex type " + type.FullName + " cannot be converted to sql type");
        }
        public static void If(this IQueryBuilder builder,AbstractSqlCondition condition)
        {
            builder.If(condition.ToSqlString());
        }
        public static void ElseIf(this IQueryBuilder builder,AbstractSqlCondition condition)
        {            
            builder.ElseIf(condition.ToSqlString());
        }
        public static IInsertNoValuesQueryBuilder Values(this IInsertWithValuesQueryBuilder builder,
            Dictionary<string, SqlServerLiteral> colsAndValues)
        {
            var colsAndValuesReformed = colsAndValues.ToDictionary(x => x.Key, x => x.Value as ISqlExpression);
            return builder.Values(colsAndValuesReformed);
        }
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
        public static AbstractSqlVariable Declare<T>(this IQueryBuilder builder,
            string variableName,  T defaultValue)
        {
            {
                if (defaultValue is int i)
                {
                    return builder.Declare(variableName, SQLKeywords.INT, i);
                }
            }
            {
                if (defaultValue is uint i)
                {
                    return builder.Declare(variableName, SQLKeywords.INT, i);
                }
            }
            {
                if (defaultValue is long i)
                {
                    return builder.Declare(variableName, SQLKeywords.BIGINT, i);
                }
            }
            {
                if (defaultValue is ulong i)
                {
                    return builder.Declare(variableName, SQLKeywords.BIGINT, i);
                }
            }

            {
                if (defaultValue is byte i)
                {
                    return builder.Declare(variableName, SQLKeywords.TINYINT, i);
                }                
            }
            {
                if (defaultValue is Guid i)
                {
                    return builder.Declare(variableName, SQLKeywords.UNIQUEIDENTIFIER, i);
                }
            }
            {
                if (defaultValue is DateTime i)
                {
                    return builder.Declare(variableName, SQLKeywords.DATETIME, i);
                }
            }
            {
                if (defaultValue is string i)
                {
                    return builder.Declare(variableName, SQLKeywords.NVARCHARMAX, i);
                }
            }
            throw new Exception("Complex type " + typeof(T).FullName + " cannot be converted to sql literal");
        }
        public static AbstractSqlVariable Declare<T>(this IQueryBuilder builder,
            string variableName)
        {
            var type = typeof(T);
            {
                if ( type== typeof(int))
                {
                    return builder.Declare(variableName, SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(uint))
                {
                    return builder.Declare(variableName, SQLKeywords.INT);
                }
            }
            {
                if (type == typeof(long))
                {
                    return builder.Declare(variableName, SQLKeywords.BIGINT);
                }
            }
            {
                if (type == typeof(ulong))
                {
                    return builder.Declare(variableName, SQLKeywords.BIGINT);
                }
            }

            {
                if (type == typeof(byte))
                {
                    return builder.Declare(variableName, SQLKeywords.TINYINT);
                }
            }
            {
                if (type == typeof(Guid))
                {
                    return builder.Declare(variableName, SQLKeywords.UNIQUEIDENTIFIER);
                }
            }
            {
                if (type == typeof(DateTime))
                {
                    return builder.Declare(variableName, SQLKeywords.DATETIME);
                }
            }
            {
                if (type == typeof(string))
                {
                    return builder.Declare(variableName, SQLKeywords.NVARCHARMAX);
                }
            }
            throw new Exception("Complex type " + type.FullName + " cannot be converted to sql literal");
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
        public static string ColumnLessThan(this IConditionFilterQueryHelper helper, string columnName, SqlServerLiteral value)
        {
            return helper.ColumnLessThan(columnName, value);
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