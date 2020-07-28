namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlExpression expression);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    }
}