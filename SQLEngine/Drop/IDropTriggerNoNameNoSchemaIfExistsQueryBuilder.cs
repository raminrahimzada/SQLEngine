namespace SQLEngine;

public interface IDropTriggerNoNameNoSchemaIfExistsQueryBuilder : IAbstractQueryBuilder
{
    IDropTriggerNoNameIfExistsQueryBuilder IfExists();
}