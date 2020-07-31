using System;

namespace SQLEngine.SqlServer
{
    public class StringEnumSqlStringConvertor : IEnumSqlStringConvertor
    {
        public string ToSqlString(Enum @enum)
        {
            if (@enum == null) return "NULL";
            return @enum.ToString();
        }
    }
}