namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string i);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, byte? scale = null);
    }
}