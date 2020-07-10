using System;

namespace SQLEngine.SqlServer
{
    public static class SqlExtensions
    {
        public static SqlServerLiteral ToSQL(this int i)
        {
            return new SqlServerLiteral(i);
        }

        public static SqlServerLiteral ToSQL(this short s)
        {
            return new SqlServerLiteral(s);
        }

        public static SqlServerLiteral ToSQL(this decimal d)
        {
            return new SqlServerLiteral(d);
        }

        public static SqlServerLiteral ToSQL(this double d)
        {
            return new SqlServerLiteral(d);
        }

        public static SqlServerLiteral ToSQL(this byte b)
        {
            return new SqlServerLiteral(b);
        }
        public static SqlServerLiteral ToSQL(this long b)
        {
            return new SqlServerLiteral(b);
        }
        public static SqlServerLiteral ToSQL(this byte[] data)
        {
            return new SqlServerLiteral(data);
        }
        public static SqlServerLiteral ToSQL(this string str, bool isUnicode = true)
        {
            return new SqlServerLiteral(str);
        }

        public static SqlServerLiteral ToSQL(this DateTime? date)
        {
            return new SqlServerLiteral(date);
        }

        public static SqlServerLiteral ToSQL(this bool b)
        {
            return new SqlServerLiteral(b);
        }
        public static SqlServerLiteral ToSQL(this bool? b)
        {
            return new SqlServerLiteral(b);
        }
        public static SqlServerLiteral ToSQL(this double? d)
        {
            return new SqlServerLiteral(d);
        }
        public static SqlServerLiteral ToSQL(this decimal? d)
        {
            return new SqlServerLiteral(d);
        }
        public static SqlServerLiteral ToSQL(this short? s)
        {
            return new SqlServerLiteral(s);
        }

        public static SqlServerLiteral ToSQL(this int? i)
        {
            return new SqlServerLiteral(i);
        }
        public static SqlServerLiteral ToSQL(this long? l)
        {
            return new SqlServerLiteral(l);
        }
        public static SqlServerLiteral ToSQL(this DateTime date, bool onlyDate = false)
        {
            return new SqlServerLiteral(date, onlyDate);
        }

        public static string AsSQLVariable(this string variableName)
        {
            return $"@{variableName}";
        }
         
    }
}
