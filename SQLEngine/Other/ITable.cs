using System;

namespace SQLEngine;

public interface ITable : IDisposable
{
    string Name { get; }
    string Schema { get; }
}

public interface IView : IDisposable
{
    string Name { get; }
    string Schema { get; }
}