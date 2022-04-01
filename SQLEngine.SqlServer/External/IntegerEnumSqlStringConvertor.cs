using System;

namespace SQLEngine.SqlServer;

public class IntegerEnumSqlStringConvertor : IEnumSqlStringConvertor
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