namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale=null);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
    }
}