using System;

namespace SQLEngine.SqlServer
{
    public class SqlServerLiteral : AbstractSqlExpression,ISqlLiteral
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
            _rawSqlString = i.ToString();
        }
        public SqlServerLiteral(bool b)
        {
            _rawSqlString = b ? "1" : "0";
        }
        public SqlServerLiteral(bool? b)
        {
            if (b == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = b.Value ? "1" : "0";
        }
        public SqlServerLiteral(short s)
        {
            _rawSqlString = s.ToString();
        }
        public SqlServerLiteral(long l)
        {
            _rawSqlString = l.ToString();
        }
        public SqlServerLiteral(short? s)
        {
            if (s == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = s.Value.ToString();
        }
        public SqlServerLiteral(long? l)
        {
            if (l == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = l.Value.ToString();
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
            _rawSqlString=(d + string.Empty).Replace(',', '.');
        }
        public SqlServerLiteral(decimal? d)
        {
            if (d == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString=(d.Value + string.Empty).Replace(',', '.');
        }
        public SqlServerLiteral(double d)
        {
            _rawSqlString = (d + string.Empty).Replace(',', '.');
        }

        public SqlServerLiteral(byte[] data)
        {
            if (data == null)
            {
                _rawSqlString = SQLKeywords.NULL;
                return;
            }
            _rawSqlString = "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public SqlServerLiteral(double? d)
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


        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        private SqlServerLiteral()
        {
            
        }
        public static ISqlLiteral Raw(string rawSqlString)
        {
            var result = new SqlServerLiteral {_rawSqlString = rawSqlString};
            return result;
        }
    }
}