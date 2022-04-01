namespace SQLEngine;

public interface IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal);
    IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notnull = true);
    IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, byte? scale = null);
}