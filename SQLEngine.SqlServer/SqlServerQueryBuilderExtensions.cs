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
                    return (C.INT);
                }
            }
            {
                if (type == typeof(uint))
                {
                    return (C.INT);
                }
            }
            {
                if (type == typeof(long))
                {
                    return (C.BIGINT);
                }
            }
            {
                if (type == typeof(ulong))
                {
                    return (C.BIGINT);
                }
            }

            {
                if (type == typeof(byte))
                {
                    return (C.TINYINT);
                }
            }
            {
                if (type == typeof(Guid))
                {
                    return (C.UNIQUEIDENTIFIER);
                }
            }
            {
                if (type == typeof(DateTime))
                {
                    return (C.DATETIME);
                }
            }
            {
                if (type == typeof(string))
                {
                    return (C.NVARCHAR);
                }
            }
            {
                if (type == typeof(decimal))
                {
                    return (C.DECIMAL);
                }
            }
            throw new Exception("Complex type " + type.FullName + " cannot be converted to sql type");
        }


        public static void Print(this IQueryBuilder builder, SqlServerLiteral literal)
        {
            builder.Print(literal);
        }
        public static IColumnQueryBuilder Column<T>(this IColumnsCreateQueryBuilder builder,string columnName)
        {
            var sqlType = DetectSqlType<T>();
            return builder.Column(columnName).Type(sqlType);
        }
        public static ISelectOrderBuilder OrderBy(this ISelectWithoutWhereQueryBuilder builder, string columnName)
        {
            return builder.OrderBy(new SqlServerColumn(columnName));
        }

        public static ISelectOrderBuilder OrderByDesc(this ISelectWithoutWhereQueryBuilder builder, string columnName)
        {
            return builder.OrderByDesc(new SqlServerColumn(columnName));
        }

        public static ISelectOrderBuilder OrderBy(this ISelectWithoutWhereQueryBuilder builder,AbstractSqlColumn column)
        {
            return builder.OrderBy(column);
        }

        public static ISelectOrderBuilder OrderByDesc(this ISelectWithoutWhereQueryBuilder builder, AbstractSqlColumn column)
        {
            return builder.OrderByDesc(column);
        }
        public static ISelectWithSelectorQueryBuilder SelectAssign(this ISelectWithSelectorQueryBuilder builder, AbstractSqlVariable left, SqlServerLiteral literal)
        {
            return builder.SelectAssign(left, literal);
        }
        public static ISelectWithSelectorQueryBuilder SelectAssign(this ISelectWithSelectorQueryBuilder builder, AbstractSqlVariable left, AbstractSqlColumn column)
        {
            return builder.SelectAssign(left, column);
        }
        public static IAggregateFunctionBuilder Min(this IAggregateFunctionBuilder builder, string columnName)
        {
            return builder.Min(new SqlServerColumn(columnName));
        }

        public static IAggregateFunctionBuilder Max(this IAggregateFunctionBuilder builder, string columnName)
        {
            return builder.Max(new SqlServerColumn(columnName));
        }

        public static IAggregateFunctionBuilder Count(this IAggregateFunctionBuilder builder, string columnName)
        {
            return builder.Count(new SqlServerColumn(columnName));
        }

        public static IAggregateFunctionBuilder Sum(this IAggregateFunctionBuilder builder, string columnName)
        {
            return builder.Sum(new SqlServerColumn(columnName));
        }

        public static IAggregateFunctionBuilder Avg(this IAggregateFunctionBuilder builder, string columnName)
        {
            return builder.Avg(new SqlServerColumn(columnName));
        }



        public static ISelectWithoutFromAndGroupQueryBuilder GroupBy(this ISelectWithoutFromQueryBuilder builder,
            params string[] columnNames)
        {
            var cols = columnNames
                .Select(columnName => new SqlServerColumn(columnName))
                .Cast<AbstractSqlColumn>()
                .ToArray();
            var result = builder.GroupBy(cols[0]);
            for (int i = 1; i < cols.Length; i++)
            {
                result = result.GroupBy(cols[0]);
            }

            return result;
        }
        public static ISelectWithoutFromAndGroupQueryBuilder GroupBy(this ISelectWithoutFromQueryBuilder builder,
            string columnName)
        {
            return builder
                .GroupBy(new SqlServerColumn(columnName));
        }
        public static ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(this ISelectWithoutFromQueryBuilder builder,
            string columnName)
        {
            return builder
                .GroupByDesc(new SqlServerColumn(columnName));
        }
        public static IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(
            this IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder builder,

            SqlServerLiteral literal)
        {
            return builder.DefaultValue(literal);
        }
        public static IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType
        <T>(
            this IAlterTableNoNameAddColumnNoNameQueryBuilder builder)
        {
            var sqlType = DetectSqlType<T>();
            return builder.OfType(sqlType);
        }
        public static IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType
        <T>(
            this IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder builder)
        {
            var sqlType = DetectSqlType<T>();
            return builder.OfType(sqlType);
        }
        public static IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(
            this IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder builder,
            SqlServerLiteral literal)
        {
            return builder.DefaultValue(literal);
        }
        public static IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(
            this IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder builder,
            SqlServerLiteral literal)
        {
            return builder.DefaultValue(literal);
        }
        public static IDeleteExceptWhereQueryBuilder Where(this IDeleteExceptTableNameQueryBuilder builder,
            SqlServerCondition condition)
        {
            return builder.Where(condition);
        }
        public static AbstractSqlCondition In(this AbstractSqlColumn column
            , params ISqlExpression[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -IN query needed");
            }
            var expression =   column.ToSqlString() + " IN (" +
                            string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";
            
            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition In(this AbstractSqlColumn column
            , params SqlServerLiteral[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -IN query needed");
            }
            var expression =   column.ToSqlString() + " IN (" +
                            string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";
            
            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition In(this AbstractSqlColumn column
            , Action<ISelectQueryBuilder> builderFunc)
        {
            var writer = SqlWriter.New;
            writer.Write(column.ToSqlString());
            writer.Write(C.SPACE);
            writer.Write(C.IN);
            writer.Write(C.SPACE);
            writer.Write(C.BEGIN_SCOPE);
            using (var builder=new SelectQueryBuilder())
            {
                builderFunc(builder);
                builder.Build(writer);
            }
            writer.Write(C.END_SCOPE);

            return SqlServerCondition.Raw(writer.Build());
        }

        public static AbstractSqlCondition NotIn(this AbstractSqlColumn column
            , params ISqlExpression[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -IN query needed");
            }
            var expression = column.ToSqlString() + " NOT IN (" +
                             string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition NotIn(this AbstractSqlColumn column
            , IAbstractSelectQueryBuilder selectQuery)
        {
            var expression = column.ToSqlString() + " NOT IN (" + selectQuery + ")";
            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition In(this AbstractSqlColumn column
            , IAbstractSelectQueryBuilder selectQuery)
        {
            var expression = column.ToSqlString() + "  IN (" + selectQuery + ")";
            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition NotIn(this AbstractSqlColumn column
            , params SqlServerLiteral[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -IN query needed");
            }
            var expression = column.ToSqlString() + " NOT IN (" +
                             string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

            return SqlServerCondition.Raw(expression);
        }


        public static AbstractSqlCondition Between(this AbstractSqlColumn column
            , ISqlExpression from,ISqlExpression to)
        {
            var expression = "(" + column.ToSqlString() + " BETWEEN (" + from.ToSqlString() + ") AND (" +
                             to.ToSqlString() + "))";

            return SqlServerCondition.Raw(expression);
        }
        public static AbstractSqlCondition Between(this AbstractSqlColumn column
            , SqlServerLiteral from, SqlServerLiteral to)
        {
            var expression = "(" + column.ToSqlString() + " BETWEEN " + from.ToSqlString() + " AND " +
                             to.ToSqlString() + ")";

            return SqlServerCondition.Raw(expression);
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
            var sqlType = DetectSqlType<T>();
            return builder.Parameter(paramName, sqlType);
        }
        public static void If(this IQueryBuilder builder,AbstractSqlCondition condition)
        {
            builder.If(condition);
        }
        public static void ElseIf(this IQueryBuilder builder,AbstractSqlCondition condition)
        {            
            builder.ElseIf(condition);
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
            return builder.Value(columnName, columnValue);
        }
        public static IInsertNeedValueQueryBuilder Value(this IInsertNeedValueQueryBuilder builder, 
            string columnName, SqlServerLiteral columnValue)
        {
            return builder.Value(columnName, columnValue);
        }
        public static AbstractSqlVariable Declare<T>(this IQueryBuilder builder,
            string variableName,  T defaultValue)
        {
            {
                if (defaultValue is int i)
                {
                    return builder.Declare(variableName, C.INT, i);
                }
            }
            {
                if (defaultValue is uint i)
                {
                    return builder.Declare(variableName, C.INT, i);
                }
            }
            {
                if (defaultValue is long i)
                {
                    return builder.Declare(variableName, C.BIGINT, i);
                }
            }
            {
                if (defaultValue is ulong i)
                {
                    return builder.Declare(variableName, C.BIGINT, i);
                }
            }

            {
                if (defaultValue is byte i)
                {
                    return builder.Declare(variableName, C.TINYINT, i);
                }                
            }
            {
                if (defaultValue is Guid i)
                {
                    return builder.Declare(variableName, C.UNIQUEIDENTIFIER, i);
                }
            }
            {
                if (defaultValue is DateTime i)
                {
                    return builder.Declare(variableName, C.DATETIME, i);
                }
            }
            {
                if (defaultValue is string i)
                {
                    return builder.Declare(variableName, C.NVARCHARMAX, i);
                }
            }
            throw new Exception("Complex type " + typeof(T).FullName + " cannot be converted to sql literal");
        }
        public static AbstractSqlVariable Declare<T>(this IQueryBuilder builder,
            string variableName)
        {
            var sqlType = DetectSqlType<T>();
            return builder.Declare(variableName, sqlType);
        }

        public static AbstractSqlVariable Declare(this IQueryBuilder builder, 
            string variableName, string type, SqlServerLiteral defaultValue = null)
        {
            return builder.Declare(variableName, type, defaultValue);
        }
        public static void Set(this IQueryBuilder builder, AbstractSqlVariable variable, SqlServerLiteral value)
        {
            builder.Set(variable, value);
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
            int sqlErrorState = Query.Settings.SQLErrorState;
            var list = new List<string>(args.Length + 1) {exceptionMessage.ToSQL().ToSqlString()};
            list.AddRange(args);
            var errorMessageVar = builder.DeclareRandom("EXCEPTION", C.NVARCHARMAX);
            AbstractSqlLiteral to = SqlServerLiteral.Raw($"{C.FORMATMESSAGE}({list.JoinWith(", ")})");
            builder.Set(errorMessageVar, to);
            builder.AddExpression($"{C.RAISERROR}({errorMessageVar}, 18, {sqlErrorState}) {C.WITH} {C.NOWAIT}");
        }
    }
}