namespace SQLEngine
{
    public interface ICreateProcedureQueryBuilder:IAbstractQueryBuilder
    {
        ICreateProcedureNoNameQueryBuilder Name(string procName);
        ICreateProcedureWithArgumentQueryBuilder Argument(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ArgumentOut(string argName, string argType);

        ICreateProcedureNoHeaderQueryBuilder Header(string procedureHeaderMetaData);
    }
}