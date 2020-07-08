using System;

namespace SQLEngine
{
    public interface ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder : IAbstractQueryBuilder
    {
        IAbstractCreateFunctionQueryBuilder Body(Action<IQueryBuilder> body);
    }
}