namespace SQLEngine;

public interface IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(
        AbstractSqlLiteral literal, string defaultValueConstraintName = null);

    IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder NotNull(bool notNull = true);
    IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale = null);
}