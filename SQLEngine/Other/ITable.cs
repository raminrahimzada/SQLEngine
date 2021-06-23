using System;

namespace SQLEngine
{
    public interface ITable:IDisposable
    {
        string Name { get;}
    }
}