namespace SQLEngine
{
    public interface IDropViewNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IDropViewNoSchemaQueryBuilder FromSchema(string schema);
    }
}