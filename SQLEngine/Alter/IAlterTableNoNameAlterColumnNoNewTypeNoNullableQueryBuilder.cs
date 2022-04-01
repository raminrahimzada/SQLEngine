namespace SQLEngine;

public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(
        AbstractSqlLiteral literal, string defaultValueConstraintName = null);

    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale = null);
}