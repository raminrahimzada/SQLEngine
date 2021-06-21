using System;

namespace SQLEngine.PostgreSql
{
    public class PostgreSqlLiteral : AbstractSqlLiteral
    {
        static PostgreSqlLiteral()
        {
            Setup();
        }
        internal static void Setup()
        {
            CreateEmpty = () => new PostgreSqlLiteral();
        }

        private static IEnumSqlStringConvertor EnumSqlStringConvertor => Query.Settings.EnumSqlStringConvertor;
        private string _rawSqlString;
 

        public static PostgreSqlLiteral From(DateTime? date,bool includeTime=true)
        {
            var result = new PostgreSqlLiteral();

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

        public override void SetFrom(DateTimeOffset dto)
        {
            _rawSqlString = dto.ToString(Query.Settings.DatetimeOffsetFormat);
            _rawSqlString = $"'{_rawSqlString}'";
        }

        public override void SetFrom(DateTimeOffset? dto)
        {
            if (dto == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = dto.Value.ToString(Query.Settings.DatetimeOffsetFormat);
            _rawSqlString = $"'{_rawSqlString}'";
        }


        public override void SetFrom(Enum e)
        {
            _rawSqlString = EnumSqlStringConvertor.ToSqlString(e);
        }

        public override void SetFrom(byte b)
        {
            _rawSqlString = b.ToString();
        }

        public override void SetFrom(byte? b)
        {
            if (b == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = b.Value.ToString();
        }

        public override void SetFrom(sbyte? sb)
        {
            if (sb == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = sb.Value.ToString();
        }

        public override void SetFrom(sbyte sb)
        {
            _rawSqlString = sb.ToString();
        }

        public override void SetFrom(Guid g)
        {
            _rawSqlString = $"'{g}'";
        }

        public override void SetFrom(Guid? g)
        {
            _rawSqlString = g == null ? C.NULL : $"'{g}'";
        }

        public override void SetFrom(long l)
        {
            _rawSqlString = l.ToString();
        }

        public override void SetFrom(char ch)
        {
            _rawSqlString = "N'" + ch + "'";
        }

        public override void SetFrom(DateTime? dt)
        {
            if (dt == null)
            {
                _rawSqlString = C.NULL;
            }
            else
            {
                SetFrom(dt.Value);
            }
        }

        public override void SetFrom(ulong ul)
        {
            _rawSqlString = ul.ToString();
        }

        public override void SetFrom(uint ui)
        {
            _rawSqlString = ui.ToString();
        }

        public override void SetFrom(bool b)
        {
            _rawSqlString = b ? "1" : "0";
        }

        public override void SetFrom(string s, bool isUnicode = true)
        {
            if (s == null) _rawSqlString = C.NULL;
            else
            {
                s = s.Replace("'", "''");
                s = $"'{s}'";
                _rawSqlString = isUnicode ? $"{s}" : s;
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
        public override void SetFrom(ushort us)
        {
            _rawSqlString = us.ToString();
        }

        public override void SetFrom(DateTime dt, bool includeTime = true)
        {
            var str = dt.ToString((!includeTime) ? Query.Settings.DateFormat : Query.Settings.DateTimeFormat);
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

        public override void SetFrom(ushort? us)
        {
            if (us == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = us.Value.ToString();
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

        public override void SetFrom(ulong? ul)
        {
            if (ul == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = ul.Value.ToString();
        }

        public override void SetFrom(uint? ui)
        {
            if (ui == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = ui.Value.ToString();
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

        public override void SetFrom(short? sh)
        {
            if (sh == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = sh.Value.ToString();
        }

        public override void SetFrom(char? ch)
        {
            if (ch == null)
            {
                _rawSqlString = C.NULL;
                return;
            }

            _rawSqlString = "'" + ch + "'";
        }


        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        private PostgreSqlLiteral()
        {
            
        }
        public static AbstractSqlLiteral Raw(string rawSqlString)
        {
            return new PostgreSqlLiteral {_rawSqlString = rawSqlString};
        }
        public static AbstractSqlLiteral Raw(char ch)
        {
            return new PostgreSqlLiteral {_rawSqlString = ch.ToString()};
        }
        public static AbstractSqlLiteral Raw(char? ch)
        {
            return new PostgreSqlLiteral {_rawSqlString = ch?.ToString()};
        }

        public static implicit operator string(PostgreSqlLiteral x)
        {
            return x.ToSqlString();
        }

        public override string ToString()
        {
            return ToSqlString();
        }

    }
}