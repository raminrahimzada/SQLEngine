namespace SQLEngine;

public interface ICreateProcedureQueryBuilder:IAbstractQueryBuilder
{
    ICreateProcedureNoNameQueryBuilder Name(string procedureName);
    ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
    ICreateProcedureWithArgumentQueryBuilder Parameter<T>(string argName);
    ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
    ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(string argName);

    ICreateProcedureNoHeaderQueryBuilder Header(string procedureHeaderMetaData);
}