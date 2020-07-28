namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, int? scale = null);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlExpression expression);
    }
}