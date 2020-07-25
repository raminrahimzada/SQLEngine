namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    }
}