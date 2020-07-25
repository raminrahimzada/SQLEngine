using System;

namespace SQLEngine.SqlServer
{
    public class SqlServerLiteral : AbstractSqlLiteral
    {
        internal static void Setup()
        {
            CreateEmpty = () => new SqlServerLiteral();
        }

        private string _rawSqlString;
 

        public static SqlServerLiteral From(DateTime? date,bool includeTime=true)
        {
            var result = new SqlServerLiteral();

            if (date == null)
            {
                result._rawSqlString = C.NULL;
                return result;
            }

            var str = date.Value.ToString(!includeTime ? Query.Settings.DateFormat : Query.Settings.DateTimeFormat);
            result._rawSqlString = $"'{str}'";
            return result;
        }


        public override void SetFrom(byte[] data)
        {
            if (data == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public override void SetFrom(int i)
        {
            _rawSqlString = i.ToString();
        }

        public override void SetFrom(Guid i)
        {
            _rawSqlString = $"{i}";
        }

        public override void SetFrom(Guid? i)
        {
            _rawSqlString = i == null ? C.NULL : $"{i}";
        }

        public override void SetFrom(long l)
        {
            _rawSqlString = l.ToString();
        }

        public override void SetFrom(DateTime? dt)
        {
            throw new NotImplementedException();
        }

        public override void SetFrom(ulong l)
        {
            _rawSqlString = l.ToString();
        }

        public override void SetFrom(uint l)
        {
            _rawSqlString = l.ToString();
        }

        public override void SetFrom(bool b)
        {
            _rawSqlString = b ? "1" : "0";
        }

        public override void SetFrom(string str, bool isUnicode = true)
        {
            if (str == null) _rawSqlString = C.NULL;
            else
            {
                str = str.Replace("'", "''");
                str = $"'{str}'";
                _rawSqlString = isUnicode ? $"N{str}" : str;
            }
        }

        public override void SetFrom(double d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        public override void SetFrom(decimal d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        public override void SetFrom(float f)
        {
            _rawSqlString = (f + string.Empty).Replace(',', '.');
        }

        public override void SetFrom(short s)
        {
            _rawSqlString = s.ToString();
        }

        public override void SetFrom(DateTime date, bool includeTime = true)
        {
            var str = date.ToString((!includeTime) ? Query.Settings.DateFormat : Query.Settings.DateTimeFormat);
            _rawSqlString = $"'{str}'";
        }

        public override void SetFrom(int? i)
        {
            if (i == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
        }

        public override void SetFrom(long? l)
        {
            if (l == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = l.Value.ToString();
        }

        public override void SetFrom(bool? b)
        {
            if (b == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = b.Value ? "1" : "0";
        }

        public override void SetFrom(double? d)
        {
            if (d != null)
            {
                _rawSqlString = (d + string.Empty).Replace(',', '.');
            }
            else
            {
                _rawSqlString = C.NULL;
            }
        }

        public override void SetFrom(decimal? d)
        {
            if (d == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = (d.Value + string.Empty).Replace(',', '.');
        }

        public override void SetFrom(float? f)
        {
            if (f == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = (f.Value + string.Empty).Replace(',', '.');
        }

        public override void SetFrom(short? s)
        {
            if (s == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = s.Value.ToString();
        }


        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        private SqlServerLiteral()
        {
            
        }
        public static AbstractSqlLiteral Raw(string rawSqlString)
        {
            return new SqlServerLiteral {_rawSqlString = rawSqlString};
        }
        public static AbstractSqlLiteral Raw(char ch)
        {
            return new SqlServerLiteral {_rawSqlString = ch.ToString()};
        }
        public static AbstractSqlLiteral Raw(char? ch)
        {
            return new SqlServerLiteral {_rawSqlString = ch?.ToString()};
        }

        public static implicit operator string(SqlServerLiteral x)
        {
            return x.ToSqlString();
        }

        public override string ToString()
        {
            return ToSqlString();
        }

    }
}