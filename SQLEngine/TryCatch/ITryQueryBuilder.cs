using System;

namespace SQLEngine;

public interface ITryQueryBuilder : IAbstractQueryBuilder
{
    ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder);
}