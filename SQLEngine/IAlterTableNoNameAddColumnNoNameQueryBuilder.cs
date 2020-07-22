namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type);
        IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notnull=true);
    }
}