namespace SQLEngine
{
    public interface IAlterTableNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName);
        IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
        IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName);
        IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName);
    }
}