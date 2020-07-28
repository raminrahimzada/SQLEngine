using System;

namespace SQLEngine
{
    public interface IEnumSqlStringConvertor
    {
        string ToSqlString(Enum @enum);
    }

    public class IntegerEnumSqlStringConvertor : IEnumSqlStringConvertor
    {
        public string ToSqlString(Enum @enum)
        {
            if (@enum == null) return "NULL";
            return ((int) (object) @enum).ToString();
        }
    }
    public class StringEnumSqlStringConvertor : IEnumSqlStringConvertor
    {
        public string ToSqlString(Enum @enum)
        {
            if (@enum == null) return "NULL";
            return @enum.ToString();
        }
    }
}