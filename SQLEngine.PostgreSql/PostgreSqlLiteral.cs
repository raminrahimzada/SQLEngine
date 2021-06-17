﻿using System;

namespace SQLEngine.PostgreSql
{
    public class PostgreSqlLiteral : AbstractSqlLiteral
    {
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

        public override void SetFrom(DateTimeOffset d)
        {
            _rawSqlString = d.ToString(Query.Settings.DatetimeOffsetFormat);
            _rawSqlString = $"'{_rawSqlString}'";
        }

        public override void SetFrom(DateTimeOffset? d)
        {
            if (d == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = d.Value.ToString(Query.Settings.DatetimeOffsetFormat);
            _rawSqlString = $"'{_rawSqlString}'";
        }


        public override void SetFrom(Enum i)
        {
            _rawSqlString = EnumSqlStringConvertor.ToSqlString(i);
        }

        public override void SetFrom(byte i)
        {
            _rawSqlString = i.ToString();
        }

        public override void SetFrom(byte? i)
        {
            if (i == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
        }

        public override void SetFrom(sbyte? i)
        {
            if (i == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
        }

        public override void SetFrom(sbyte i)
        {
            _rawSqlString = i.ToString();
        }

        public override void SetFrom(Guid i)
        {
            _rawSqlString = $"'{i}'";
        }

        public override void SetFrom(Guid? i)
        {
            _rawSqlString = i == null ? C.NULL : $"'{i}'";
        }

        public override void SetFrom(long l)
        {
            _rawSqlString = l.ToString();
        }

        public override void SetFrom(char f)
        {
            _rawSqlString = "N'" + f + "'";
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
                _rawSqlString = isUnicode ? $"{str}" : str;
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
        public override void SetFrom(ushort s)
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

        public override void SetFrom(ushort? i)
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

        public override void SetFrom(ulong? i)
        {
            if (i == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
        }

        public override void SetFrom(uint? i)
        {
            if (i == null)
            {
                _rawSqlString = C.NULL;
                return;
            }
            _rawSqlString = i.Value.ToString();
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

        public override void SetFrom(char? f)
        {
            if (f == null)
            {
                _rawSqlString = C.NULL;
                return;
            }

            _rawSqlString = "'" + f + "'";
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