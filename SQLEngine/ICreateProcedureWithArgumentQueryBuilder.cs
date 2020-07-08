using System;

namespace SQLEngine
{
    public interface ICreateProcedureWithArgumentQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Argument(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ArgumentOut(string argName, string argType);
        ICreateProcedureNeedBodyQueryBuilder Body(Action<IQueryBuilder> body);
    }
}