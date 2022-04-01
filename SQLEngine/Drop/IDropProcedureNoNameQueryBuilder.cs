namespace SQLEngine;

public interface IDropProcedureNoNameQueryBuilder : IAbstractQueryBuilder
{
    IDropProcedureNoNameNoSchemaNameQueryBuilder Schema(string schemaName);
}