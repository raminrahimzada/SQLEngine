using System;

namespace SQLEngine
{
    public interface ITryNoTryQueryBuilder : IAbstractQueryBuilder
    {
        ITryNoCatchQueryBuilder Catch(Action<ICatchFunctionQueryBuilder> builder);
    }
}