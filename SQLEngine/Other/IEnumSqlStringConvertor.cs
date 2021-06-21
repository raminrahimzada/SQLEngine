using System;

namespace SQLEngine
{
    public interface IEnumSqlStringConvertor
    {
        string ToSqlString(Enum @enum);
    }
}