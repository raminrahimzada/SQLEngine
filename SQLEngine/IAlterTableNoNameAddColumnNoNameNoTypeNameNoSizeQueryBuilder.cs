namespace SQLEngine
{
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlExpression expression);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    }
}