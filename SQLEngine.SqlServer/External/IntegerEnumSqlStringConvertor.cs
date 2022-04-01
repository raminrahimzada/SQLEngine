using System;

namespace SQLEngine.SqlServer;

public sealed class IntegerEnumSqlStringConvertor : IEnumSqlStringConvertor
{
    public string ToSqlString(Enum @enum)
    {
        if(@enum == null)
        {
            return "NULL";
        }

        return ((int)(object)@enum).ToString();
    }
}