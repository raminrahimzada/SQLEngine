namespace SQLEngine;

public interface IAlterTableNoNameAddColumnNoNameQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notNull = true);
    IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type);
    IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType<T>();
}