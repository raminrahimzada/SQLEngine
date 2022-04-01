namespace SQLEngine;

public interface IDropFunctionQueryBuilder : IAbstractQueryBuilder
{
    IDropFunctionNoSchemaQueryBuilder FromSchema(string schemaName);
}