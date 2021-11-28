using System;

namespace SQLEngine
{
    public interface ICreateViewNoNameQueryBuilder : IAbstractQueryBuilder
    {
        ICreateViewNoNameNoBodyQueryBuilder As(Action<ISelectQueryBuilder> selectionBuilder);
        ICreateViewNoNameQueryBuilder Schema(string schema);
    }
}