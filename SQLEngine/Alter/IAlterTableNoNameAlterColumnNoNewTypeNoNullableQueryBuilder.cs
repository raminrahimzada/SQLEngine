namespace SQLEngine;

public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale=null);
    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultValueConstraintName = null);
}