namespace SQLEngine
{
    public interface ICreateProcedureNoHeaderQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
    }
}