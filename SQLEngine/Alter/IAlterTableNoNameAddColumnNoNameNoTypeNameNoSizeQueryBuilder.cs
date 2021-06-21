namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    }
}