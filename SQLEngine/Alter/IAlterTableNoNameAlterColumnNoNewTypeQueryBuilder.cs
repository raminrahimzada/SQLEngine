﻿namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder NotNull(bool notNull = true);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale = null);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultValueConstraintName = null);
    }
}