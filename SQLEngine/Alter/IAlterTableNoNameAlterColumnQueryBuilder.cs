namespace SQLEngine
{
    public interface IAlterTableNoNameAlterColumnQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type(string newType);
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder Type<T>();
    }
}