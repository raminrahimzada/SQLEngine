namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    }
}