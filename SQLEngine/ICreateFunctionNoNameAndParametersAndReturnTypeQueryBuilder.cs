using System;

namespace SQLEngine
{
    public interface IFunctionBodyQueryBuilder : IQueryBuilder
    {
        AbstractSqlVariable Param(string name);
    }
    public interface ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder : IAbstractQueryBuilder
    {
        IAbstractCreateFunctionQueryBuilder Body(Action<IFunctionBodyQueryBuilder> body);
    }
}