using System;

namespace SQLEngine;

public abstract class AbstractQueryBuilder : IAbstractQueryBuilder
{
    protected AbstractQueryBuilder()
    {
        Writer = new SqlWriter();
    }


    public int Indent
    {
        get => Writer.Indent;
        set => Writer.Indent = value;
    }

    protected ISqlWriter Writer { get; }

    public abstract void Build(ISqlWriter writer);

    public string Build()
    {
        using(var writer = SqlWriter.New)
        {
            Build(writer);
            return writer.Build();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected SqlEngineException Bomb(string message = "")
    {
        if(string.IsNullOrEmpty(message))
        {
            message = "Invalid Usage of QueryBuilder: " + GetType().Name;
        }

        throw new SqlEngineException(message);
    }

    protected static ISqlWriter CreateNewWriter()
    {
        return new SqlWriter();
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            Writer?.Dispose();
        }
    }

    protected static string I(string name)
    {
        return Query.Settings.EscapeStrategy.Escape(name);
    }

    protected static T New<T>() where T : AbstractQueryBuilder, new()
    {
        return Activator.CreateInstance<T>();
    }

    protected virtual void ValidateAndThrow()
    {
    }
}