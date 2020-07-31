namespace SQLEngine
{
    public interface ITypeConvertor
    {
        string ToSqlType<T>();
    }
}