namespace SQLEngine
{
    public interface ICreateProcedureQueryBuilder:IAbstractQueryBuilder
    {
        ICreateProcedureNoNameQueryBuilder Name(string procName);
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);

        ICreateProcedureNoHeaderQueryBuilder Header(string procedureHeaderMetaData);
    }
}