using System;

namespace SQLEngine
{
    public interface IAbstractQueryBuilder : IDisposable
    {
        string Build();
    }
}