using System.Collections.Generic;

namespace SQLEngine.PostgreSql
{
    public static class CustomFunctionCallExpressionBuilderExtensions
    {
        public static ICustomFunctionCallNopBuilder IsNull(
            this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression expression,ISqlExpression fallback)
        {
            return builder.Call("ISNULL", expression, fallback);
        }
        public static ICustomFunctionCallNopBuilder IsNull(
            this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression expression,AbstractSqlLiteral fallback)
        {
            return builder.Call("ISNULL", expression, fallback);
        }

        public static ICustomFunctionCallNopBuilder Cast(
            this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression expression,string asType)
        {
            return CustomFunctionCallExpressionBuilder.Raw(w =>
            {
                w.Write(expression.ToSqlString());
                w.Write(C.COLON);
                w.Write(C.COLON);
                w.Write(asType);
            });
        }
        public static ICustomFunctionCallNopBuilder ObjectId(this ICustomFunctionCallExpressionBuilder builder, ISqlExpression expression)
        {
            return CustomFunctionCallExpressionBuilder.Raw(writer =>
            {
                writer.Write(expression.ToSqlString());
                writer.Write("::regclass::oid");
            });
            
        }
        public static ICustomFunctionCallNopBuilder ScopeIdentity(this ICustomFunctionCallExpressionBuilder builder)
        {
            return builder.Call("SCOPE_IDENTITY");
        }
        /// <summary>
        /// The ASCII() function returns the ASCII value for the specific character.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ICustomFunctionCallNopBuilder Ascii(this ICustomFunctionCallExpressionBuilder builder, ISqlExpression expression)
        {
            return builder.Call("ASCII",expression);
        }
        /// <summary>
        /// The CHAR() function returns the character based on the ASCII code.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ICustomFunctionCallNopBuilder Char(this ICustomFunctionCallExpressionBuilder builder, ISqlExpression expression)
        {
            return builder.Call("CHAR", expression);
        }

        /// <summary>
        /// The CHARINDEX() function searches for a substring in a string, and returns the position.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="substring"></param>
        /// <param name="string"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static ICustomFunctionCallNopBuilder CharIndex(this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression substring,
            ISqlExpression @string,
            ISqlExpression start
            )
        {
            return builder.Call("CHARINDEX", substring,@string,start);
        }

        /// <summary>
        /// The CHARINDEX() function searches for a substring in a string, and returns the position.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="substring"></param>
        /// <param name="string"></param>
        /// <returns></returns>
        public static ICustomFunctionCallNopBuilder CharIndex(this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression substring,
            ISqlExpression @string
            )
        {
            return builder.Call("CHARINDEX", substring,@string);
        }

        /// <summary>
        /// The CHARINDEX() function searches for a substring in a string, and returns the position.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static ICustomFunctionCallNopBuilder Concat(this ICustomFunctionCallExpressionBuilder builder,
            params
                ISqlExpression[] strings
            )
        {
            return builder.Call("CONCAT", strings);
        }
        public static ICustomFunctionCallNopBuilder ConcatWs(this ICustomFunctionCallExpressionBuilder builder,
            char separator,
            params
                ISqlExpression[] strings
            )
        {
            var list = new List<ISqlExpression>(1 + strings.Length) {PostgreSqlLiteral.Raw(separator)};
            list.AddRange(strings);
            return builder.Call("CONCAT_WS", list.ToArray());
        }
        public static ICustomFunctionCallNopBuilder DataLength(this ICustomFunctionCallExpressionBuilder builder,
            ISqlExpression expression
        )
        {
            return builder.Call("DATALENGTH", expression);
        }
        public static ICustomFunctionCallNopBuilder Len(this ICustomFunctionCallExpressionBuilder builder,
                ISqlExpression expression
        )
        {
            return builder.Call("LEN", expression);
        }
        public static ICustomFunctionCallNopBuilder Trim(this ICustomFunctionCallExpressionBuilder builder,
                ISqlExpression expression
        )
        {
            return builder.Call("TRIM", expression);
        }
        //TODO continue from https://www.w3schools.com/sql/func_sqlserver_datalength.asp
    }
}