using System;

namespace SQLEngine.SqlServer;

public class DefaultTypeConvertor : ITypeConvertor
{
    public string ToSqlType<T>()
    {
        var type = typeof(T);
        if (type == typeof(int))
        {
            return (C.INT);
        }

        if (type == typeof(uint))
        {
            return (C.INT);
        }

        if (type == typeof(long))
        {
            return (C.BIGINT);
        }

        if (type == typeof(ulong))
        {
            return (C.BIGINT);
        }

        if (type == typeof(byte))
        {
            return (C.TINYINT);
        }

        if (type == typeof(Guid))
        {
            return (C.UNIQUEIDENTIFIER);
        }

        if (type == typeof(DateTime))
        {
            return (C.DATETIME);
        }

        if (type == typeof(string))
        {
            return (C.NVARCHARMAX);
        }

        if (type == typeof(char))
        {
            return (C.NCHAR1);
        }

        if (type == typeof(decimal))
        {
            return (C.DECIMAL);
        }

        if (type == typeof(float))
        {
            return ("FLOAT");
        }

        if (type == typeof(bool))
        {
            return (C.BIT);
        }

        throw new SqlEngineException("Complex type " + type.FullName + " cannot be converted to sql type");
    }
}