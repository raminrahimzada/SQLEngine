namespace SQLEngine
{
    public interface IAlterQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameQueryBuilder Table(string tableName);
        IAlterTableNoNameQueryBuilder Table<TTable>() where TTable:ITable,new();
    }

    public interface IAlterTableQueryBuilder //: IAbstractQueryBuilder
    {
    }

    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder //: IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
    }
    public interface IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeQueryBuilder Size(int size, int? scale = null);
        IAlterTableNoNameAddColumnNoNameNoTypeNameNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
    }

    public interface IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string i);
    }
    public interface IAlterTableNoNameAddColumnNoNameQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder OfType(string type);
        IAlterTableNoNameAddColumnNoNameNoNullableQueryBuilder NotNull(bool notnull=true);
    }
    public interface IAlterTableNoNameQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName);
        IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
        IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName);
        IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName);
    }

    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder// // IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder //: IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
    }
    public interface IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale=null);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);
    }
    public interface IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableQueryBuilder NotNull(bool notNull = true);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeQueryBuilder Size(byte size, byte? scale = null);
        IAlterTableNoNameAlterColumnNoNewTypeNoNullableNoSizeNoDefaultValueQueryBuilder DefaultValue(ISqlExpression expression);

    }
    public interface IAlterTableNoNameAlterColumnQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type(string newType);
    }

    public interface IAlterTableNoNameRenameColumnNoNewNameQueryBuilder // IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameRenameColumnQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameRenameColumnNoNewNameQueryBuilder To(string newName);
    }
    public interface IAlterTableNoNameDropColumnQueryBuilder // IAbstractQueryBuilder
    {

    }
}