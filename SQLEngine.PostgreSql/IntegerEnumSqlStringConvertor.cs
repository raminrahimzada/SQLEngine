using System;

namespace SQLEngine.PostgreSql
{
    public class IntegerEnumSqlStringConvertor : IEnumSqlStringConvertor
    {
        public string ToSqlString(Enum @enum)
        {
            if (@enum == null) return "NULL";
            return ((int)(object)@enum).ToString();
        }
    }
}