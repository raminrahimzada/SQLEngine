using System;

namespace SQLEngine.SqlServer
{
    public static class SqlExtensions
    {
        public static SqlServerLiteral ToSQL(this int i)
        {
            return SqlServerLiteral.From(i);
        }

        public static SqlServerLiteral ToSQL(this short s)
        {
            return SqlServerLiteral.From(s);
        }

        public static SqlServerLiteral ToSQL(this decimal d)
        {
            return SqlServerLiteral.From(d);
        }

        public static SqlServerLiteral ToSQL(this double d)
        {
            return SqlServerLiteral.From(d);
        }

        public static SqlServerLiteral ToSQL(this byte b)
        {
            return SqlServerLiteral.From(b);
        }
        public static SqlServerLiteral ToSQL(this long b)
        {
            return SqlServerLiteral.From(b);
        }
        public static SqlServerLiteral ToSQL(this byte[] data)
        {
            return SqlServerLiteral.From(data);
        }
        public static SqlServerLiteral ToSQL(this string str, bool isUnicode = true)
        {
            return SqlServerLiteral.From(str, isUnicode);
        }

        public static SqlServerLiteral ToSQL(this DateTime? date)
        {
            return SqlServerLiteral.From(date);
        }

        public static SqlServerLiteral ToSQL(this bool b)
        {
            return SqlServerLiteral.From(b);
        }
        public static SqlServerLiteral ToSQL(this bool? b)
        {
            return SqlServerLiteral.From(b);
        }
        public static SqlServerLiteral ToSQL(this double? d)
        {
            return SqlServerLiteral.From(d);
        }
        public static SqlServerLiteral ToSQL(this decimal? d)
        {
            return SqlServerLiteral.From(d);
        }
        public static SqlServerLiteral ToSQL(this short? s)
        {
            return SqlServerLiteral.From(s);
        }

        public static SqlServerLiteral ToSQL(this int? i)
        {
            return SqlServerLiteral.From(i);
        }
        public static SqlServerLiteral ToSQL(this long? l)
        {
            return SqlServerLiteral.From(l);
        }
        public static SqlServerLiteral ToSQL(this DateTime date, bool onlyDate = false)
        {
            return SqlServerLiteral.From(date, onlyDate);
        }

        public static string AsSQLVariable(this string variableName)
        {
            return $"@{variableName}";
        }
         
    }
}
