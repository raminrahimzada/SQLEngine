namespace SQLEngine
{
    public interface IAlterTableNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName);
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder AddColumn<T>(string columnName);
        IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
        IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName);
        IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName);
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder AlterColumn<T>(string columnName);
    }
}