namespace SQLEngine
{
    public interface IDropTriggerNoNameQueryBuilder:IAbstractQueryBuilder
    {
        IDropTriggerNoNameIfExistsQueryBuilder IfExists();
    }
}