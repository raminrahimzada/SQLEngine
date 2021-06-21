namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(AbstractSqlLiteral literal,string defaultValueConstraintName=null);        
    }
}