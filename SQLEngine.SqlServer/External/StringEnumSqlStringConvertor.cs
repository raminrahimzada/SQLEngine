using System;

namespace SQLEngine.SqlServer;

public sealed class StringEnumSqlStringConvertor : IEnumSqlStringConvertor
{
    public string ToSqlString(Enum @enum)
    {
        if(@enum == null)
        {
            return "NULL";
        }

        return @enum.ToString();
    }
}