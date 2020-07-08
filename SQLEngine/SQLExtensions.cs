using System;

namespace SQLEngine
{
    public static class SqlExtensions
    {
        public static string ToSQL(this int i)
        {
            return i + string.Empty;
        }

        public static string ToSQL(this short s)
        {
            return s + string.Empty;
        }

        public static string ToSQL(this decimal i)
        {
            return (i + string.Empty).Replace(',', '.');
        }

        public static string ToSQL(this double i)
        {
            return i + string.Empty;
        }

        public static string ToSQL(this byte b)
        {
            return b + string.Empty;
        }
        public static string ToSQL(this long b)
        {
            return b + string.Empty;
        }
        public static string ToSQL(this byte[] data)
        {
            return "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }
        public static string ToSQL(this string str, bool isUnicode = true)
        {
            if (str == null) return SQLKeywords.NULL;
            str = str.Replace("'", "''");
            str = $"'{str}'";
            if (isUnicode)
            {
                return $"N{str}";
            }

            return str;
        }

        public static string ToSQL(this DateTime? date)
        {
            return date == null ? SQLKeywords.NULL : date.Value.ToSQL();
        }

        public static string ToSQL(this bool b)
        {
            return b ? "1" : "0";
        }
        public static string ToSQL(this bool? b)
        {
            return b == null ? SQLKeywords.NULL : b.Value ? "1" : "0";
        }
        public static string ToSQL(this double? date)
        {
            return date == null ? SQLKeywords.NULL : date.Value + string.Empty;
        }
        public static string ToSQL(this decimal? d)
        {
            return d == null ? SQLKeywords.NULL : (d.Value + string.Empty).Replace(',', '.');
        }
        public static string ToSQL(this short? date)
        {
            return date == null ? SQLKeywords.NULL : date.Value.ToString();
        }

        public static string ToSQL(this int? date)
        {
            return date == null ? SQLKeywords.NULL : date.Value.ToString();
        }
        public static string ToSQL(this long? date)
        {
            return date == null ? SQLKeywords.NULL : date.Value.ToString();
        }
        public static string ToSQL(this DateTime date, bool onlyDate = false)
        {
            //sql default full format is :
            //yyyy-MM-dd HH:mm:ss.fff
            //format = "2019-03-02 14:13:34.070";
            const string format = "yyyy-MM-dd HH:mm:ss.fff";

            const string formatOnlyDate = "yyyy-MM-dd";

            var value = date.ToString(onlyDate ? formatOnlyDate : format);

            return ToSQL(value, false);
        }

        public static string ToSQL(this object o)
        {
            if (o == null) return SQLKeywords.NULL;
            if (o is int i) return i.ToSQL();
            if (o is decimal d) return d.ToSQL();
            if (o is double d1) return d1.ToSQL();
            if (o is string s) return s.ToSQL();
            if (o is long l) return l.ToSQL();
            if (o is DateTime dt) return dt.ToSQL();
            if (o is short sh) return sh.ToSQL();
            if (o is byte bt) return bt.ToSQL();

            var type = o.GetType();
            if (type.IsEnum)
            {
                // ReSharper disable once PossibleInvalidCastException
                return ((byte)o).ToSQL();
            }
            throw new NotImplementedException();
        }

        public static string AsSQLVariable(this string variableName)
        {
            return $"@{variableName}";
        }
        //public static string I(this string variableName)
        //{
        //    return AbstractQueryBuilder.I(variableName);
        //}
    }
}
