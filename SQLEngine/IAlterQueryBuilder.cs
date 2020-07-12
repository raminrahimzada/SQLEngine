namespace SQLEngine
{
    public interface IAlterQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameQueryBuilder Table(string tableName);
    }

    public interface IAlterTableQueryBuilder : IAbstractQueryBuilder
    {

    }

    public interface IAlterTableNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnQueryBuilder AddColumn(string columnName, string columnType, bool? canBeNull = null,
            int? size = null, int? scale = null, ISqlExpression columnDefaultValue = null);

        IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
        IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName, string columnNewName);

        IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName, string newType, bool? canBeNull = true,
            int? size = null,
            int? scale = null);
    }

    public interface IAlterTableNoNameAlterColumnQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameRenameColumnQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameDropColumnQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface IAlterTableNoNameAddColumnQueryBuilder : IAbstractQueryBuilder
    {

    }
}