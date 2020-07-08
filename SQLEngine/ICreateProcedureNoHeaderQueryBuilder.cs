namespace SQLEngine
{
    public interface ICreateProcedureNoHeaderQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Argument(string argName, string argType);

    }
}