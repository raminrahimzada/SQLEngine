namespace SQLEngine;

public interface IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type);
    IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType<T>();
    IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, byte? scale = null);
}