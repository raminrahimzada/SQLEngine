namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type);
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType<T>();
        IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notNull=true);
    }
}