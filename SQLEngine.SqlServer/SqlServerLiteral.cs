using System;

namespace SQLEngine.SqlServer
{
    public class SqlServerLiteral : AbstractSqlLiteral
    {
        private const string FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        private const string FORMAT_ONLY_DATE = "yyyy-MM-dd";

        private string _rawSqlString;

        public static SqlServerLiteral From(Guid x)
        {
            return new SqlServerLiteral()
            {
                _rawSqlString = $"'{x}'"
            };
        }
        public static SqlServerLiteral From(Guid? x)
        {
            if (x==null)
            {
                return new SqlServerLiteral()
                {
                    _rawSqlString = SQLKeywords.NULL
                };
            }
            return new SqlServerLiteral()
            {
                _rawSqlString = $"'{x}'"
            };
        }
        public static SqlServerLiteral From(string str,bool isUnicode=true)
        {
            var result = new SqlServerLiteral();
            if (str == null) result._rawSqlString = SQLKeywords.NULL;
            else
            {
                str = str.Replace("'", "''");
                str = $"'{str}'";
                result._rawSqlString = isUnicode ? $"N{str}" : str;
            }

            return result;
        }
        public static SqlServerLiteral From(DateTime date,bool onlyDate=false)
        {
            var result = new SqlServerLiteral();
            var str = date.ToString(onlyDate ? FORMAT_ONLY_DATE : FORMAT);
            result._rawSqlString = $"'{str}'";
            return result;
        }

        public static SqlServerLiteral From(DateTime? date,bool onlyDate=false)
        {
            var result = new SqlServerLiteral();

            if (date == null)
            {
                result._rawSqlString = SQLKeywords.NULL;
                return result;
            }

            var str = date.Value.ToString(onlyDate ? FORMAT_ONLY_DATE : FORMAT);
            result._rawSqlString = $"'{str}'";
            return result;
        }

 

        protected void SetFrom(byte[] data)
        {
            if (data == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }

        protected void SetFrom(int i)
        {
            _rawSqlString = i.ToString();
        }

        protected void SetFrom(long l)
        {
            _rawSqlString = l.ToString();
        }

        protected void SetFrom(bool b)
        {
            _rawSqlString = b ? "1" : "0";
        }

        protected void SetFrom(double d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        protected void SetFrom(decimal d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        protected void SetFrom(float f)
        {
            _rawSqlString = (f + string.Empty).Replace(',', '.');
        }

        protected void SetFrom(short s)
        {
            _rawSqlString = s.ToString();
        }

        protected void SetFrom(int? i)
        {
            if (i == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
        }

        protected void SetFrom(long? l)
        {
            if (l == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = l.Value.ToString();
        }

        protected void SetFrom(bool? b)
        {
            if (b == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = b.Value ? "1" : "0";
        }

        protected void SetFrom(double? d)
        {
            if (d != null)
            {
                _rawSqlString = (d + string.Empty).Replace(',', '.');
            }
            else
            {
                _rawSqlString = SQLKeywords.NULL;
            }
        }

        protected void SetFrom(decimal? d)
        {
            if (d == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = (d.Value + string.Empty).Replace(',', '.');
        }

        protected void SetFrom(float? f)
        {
            if (f == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = (f.Value + string.Empty).Replace(',', '.');
        }

        protected void SetFrom(short? s)
        {
            if (s == null)
            {
                _rawSqlString = SQLKeywords.NULL;
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

        public static implicit operator string(SqlServerLiteral x)
        {
            return x.ToSqlString();
        }

        public static implicit operator SqlServerLiteral(int x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(double x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(DateTime x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(long x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(Guid x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(Guid? x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(string x)
        {
            return From(x, true);
        }
        public static implicit operator SqlServerLiteral(short x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(byte x)
        {
            return From(x);
        }
        public static implicit operator SqlServerLiteral(byte[] x)
        {
            return From(x);
        }

        public override string ToString()
        {
            return ToSqlString();
        }

        public static SqlServerLiteral From(int i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(short i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(long i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(byte i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(int? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(short? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(double? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(double i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(decimal i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(bool i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(bool? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(decimal? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(long? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(byte? i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
        public static SqlServerLiteral From(byte[] i)
        {
            var literal = new SqlServerLiteral();
            literal.SetFrom(i);
            return literal;
        }
    }
}