using System;

namespace SQLEngine.PostgreSql
{
    public class StringEnumSqlStringConvertor : IEnumSqlStringConvertor
    {
        public string ToSqlString(Enum @enum)
        {
            if (@enum == null) return C.NULL;
            return @enum.ToString();
        }
    }
}