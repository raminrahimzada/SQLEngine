namespace SQLEngine;

public interface ICreateProcedureNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateProcedureQueryBuilder Schema(string schemaName);
}