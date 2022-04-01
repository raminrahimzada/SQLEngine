using System;

namespace SQLEngine.SqlServer;

internal sealed class RawStringQueryBuilder : AbstractQueryBuilder, IElseIfQueryBuilder
{
    private readonly Action<ISqlWriter> _func;

    public RawStringQueryBuilder(Action<ISqlWriter> func)
    {
        _func = func;
    }

    public override void Build(ISqlWriter writer)
    {
        _func(writer);
    }
}