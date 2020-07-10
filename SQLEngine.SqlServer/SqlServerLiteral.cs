using System;

namespace SQLEngine.SqlServer
{
    public class SqlServerLiteral : AbstractSqlLiteral
    {
        private const string FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        private const string FORMAT_ONLY_DATE = "yyyy-MM-dd";

        private string _rawSqlString;
        public SqlServerLiteral(string str,bool isUnicode=true)
        {
            if (str == null) _rawSqlString= SQLKeywords.NULL;
            else
            {
                str = str.Replace("'", "''");
                str = $"'{str}'";
                _rawSqlString = isUnicode ? $"N{str}" : str;
            }
        }
        public SqlServerLiteral(int i)
        {
            SetFrom(i);
        }
        public SqlServerLiteral(bool b)
        {
            SetFrom(b);
        }
        public SqlServerLiteral(bool? b)
        {
          SetFrom(b);
        }
        public SqlServerLiteral(short s)
        {
            SetFrom(s);
        }
        public SqlServerLiteral(long l)
        {
            SetFrom(l);
        }
        public SqlServerLiteral(short? s)
        {
            SetFrom(s);
        }
        public SqlServerLiteral(long? l)
        {
            SetFrom(l);
        }
        

        public SqlServerLiteral(DateTime date,bool onlyDate=false)
            :this(date.ToString(onlyDate ? FORMAT_ONLY_DATE : FORMAT))
        {
        }
        public SqlServerLiteral(DateTime? date,bool onlyDate=false)
        {
            if (date == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }

            var str = date.Value.ToString(onlyDate ? FORMAT_ONLY_DATE : FORMAT);
            _rawSqlString = $"'{str}'";
        }


        public SqlServerLiteral(decimal d)
        {
            SetFrom(d);
        }
        public SqlServerLiteral(decimal? d)
        {
            SetFrom(d);
        }
        public SqlServerLiteral(double d)
        {
            SetFrom(d);
        }

        public SqlServerLiteral(byte[] data)
        {
            SetFrom(data);
        }

        public SqlServerLiteral(double? d)
        {
            SetFrom(d);
        }


        protected override void SetFrom(byte[] data)
        {
            if (data == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }

        protected override void SetFrom(int i)
        {
            _rawSqlString = i.ToString();
        }

        protected override void SetFrom(long l)
        {
            _rawSqlString = l.ToString();
        }

        protected override void SetFrom(bool b)
        {
            _rawSqlString = b ? "1" : "0";
        }

        protected override void SetFrom(string s)
        {
            throw new NotImplementedException();
        }

        protected override void SetFrom(double d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        protected override void SetFrom(decimal d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        protected override void SetFrom(float f)
        {
            _rawSqlString = (f + string.Empty).Replace(',', '.');
        }

        protected override void SetFrom(short s)
        {
            _rawSqlString = s.ToString();
        }

        protected override void SetFrom(DateTime dt)
        {
            throw new NotImplementedException();
        }

        protected override void SetFrom(int? i)
        {
            throw new NotImplementedException();
        }

        protected override void SetFrom(long? l)
        {
            if (l == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = l.Value.ToString();
        }

        protected override void SetFrom(bool? b)
        {
            if (b == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = b.Value ? "1" : "0";
        }

        protected override void SetFrom(double? d)
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

        protected override void SetFrom(decimal? d)
        {
            if (d == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = (d.Value + string.Empty).Replace(',', '.');
        }

        protected override void SetFrom(float? f)
        {
            if (f == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = (f.Value + string.Empty).Replace(',', '.');
        }

        protected override void SetFrom(short? s)
        {
            if (s == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = s.Value.ToString();
        }

        protected override void SetFrom(DateTime? dt)
        {
            throw new NotImplementedException();
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
            var result = new SqlServerLiteral {_rawSqlString = rawSqlString};
            return result;
        }

        public static implicit operator string(SqlServerLiteral x)
        {
            return x.ToSqlString();
        }

        public static implicit operator SqlServerLiteral(int x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(double x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(DateTime x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(long x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(string x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(short x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(byte x)
        {
            return new SqlServerLiteral(x);
        }
        public static implicit operator SqlServerLiteral(byte[] x)
        {
            return new SqlServerLiteral(x);
        }

        public override string ToString()
        {
            return ToSqlString();
        }
    }
}