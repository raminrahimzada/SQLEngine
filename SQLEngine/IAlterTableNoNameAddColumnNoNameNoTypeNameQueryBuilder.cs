namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, byte? scale = null);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
        IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notnull = true);
    }
}