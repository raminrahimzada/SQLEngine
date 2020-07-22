using System;

namespace SQLEngine
{
    public interface IAbstractQueryBuilder : IDisposable
    {
        void Build(ISqlWriter writer);
    }
}