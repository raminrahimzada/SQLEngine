namespace SQLEngine;

public interface IDropTableNoNameQueryBuilder : IAbstractQueryBuilder
{
    IDropTableNoNameNoSchemaQueryBuilder FromSchema(string schemaName);
}