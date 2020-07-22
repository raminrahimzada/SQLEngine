namespace SQLEngine
{
    public interface IAlterTableNoNameRenameColumnQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameRenameColumnNoNewNameQueryBuilder To(string newName);
    }
}