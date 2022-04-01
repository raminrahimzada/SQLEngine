namespace SQLEngine;

public interface IDropTriggerNoNameQueryBuilder : IAbstractQueryBuilder
{
    IDropTriggerNoNameIfExistsQueryBuilder IfExists();
    IDropTriggerNoNameNoSchemaIfExistsQueryBuilder Schema(string schemaName);
}