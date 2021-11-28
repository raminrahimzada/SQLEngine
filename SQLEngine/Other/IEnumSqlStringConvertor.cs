using System;

namespace SQLEngine
{
    public interface IEscapeStrategy
    {
        string Escape(string name);
    }
    public interface IEnumSqlStringConvertor
    {
        string ToSqlString(Enum @enum);
    }
}