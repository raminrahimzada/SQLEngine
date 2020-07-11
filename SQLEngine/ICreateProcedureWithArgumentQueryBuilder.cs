using System;

namespace SQLEngine
{
    public interface IProcedureBodyQueryBuilder : IQueryBuilder
    {
        AbstractSqlVariable Parameter(string name);
    }
    public interface ICreateProcedureWithArgumentQueryBuilder : IAbstractQueryBuilder
    {
        ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType);
        ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType);
        ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> body);
    }
}