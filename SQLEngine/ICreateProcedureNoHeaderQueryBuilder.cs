namespace SQLEngine
{
    public interface ICreateProcedureNoHeaderQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder Parameter<T>(string argName);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(string argName);
    }
}