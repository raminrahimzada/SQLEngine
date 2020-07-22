namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnQueryBuilder // IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type(string newType);
    }
}