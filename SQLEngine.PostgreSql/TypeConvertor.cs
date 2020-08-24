using System;

namespace SQLEngine.PostgreSql
{
    public class TypeConvertor : ITypeConvertor
    {
        public string ToSqlType<T>()
        {
            var type = typeof(T);
            {
                if (type == typeof(int))
                {
                    return (C.INT);
                }
            }
            {
                if (type == typeof(uint))
                {
                    return (C.INT);
                }
            }
            {
                if (type == typeof(long))
                {
                    return (C.BIGINT);
                }
            }
            {
                if (type == typeof(ulong))
                {
                    return (C.BIGINT);
                }
            }

            {
                if (type == typeof(byte))
                {
                    return (C.TINYINT);
                }
            }
            {
                if (type == typeof(Guid))
                {
                    return (C.UUID);
                }
            }
            {
                if (type == typeof(DateTime))
                {
                    return (C.TIMESTAMP);
                }
            }
            {
                if (type == typeof(string))
                {
                    return (C.VARCHAR);
                }
            }
            {
                if (type == typeof(decimal))
                {
                    return (C.DECIMAL);
                }
            }
            {
                if (type == typeof(bool))
                {
                    return (C.BIT);
                }
            }
            throw new Exception("Complex type " + type.FullName + " cannot be converted to sql type");
        }
    }
}