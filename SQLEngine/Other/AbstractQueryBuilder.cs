using System;

namespace SQLEngine;

public abstract class AbstractQueryBuilder : IAbstractQueryBuilder
{
    protected static ISqlWriter CreateNewWriter()
    {
        return new SqlWriter();
    }
    protected ISqlWriter Writer { get; }

 
    public int Indent
    {
        get => Writer.Indent;
        set => Writer.Indent = value;
    }

    protected AbstractQueryBuilder()
    {
        Writer = new SqlWriter();
    }
    protected static T New<T>() where T : AbstractQueryBuilder, new()
    {
        return Activator.CreateInstance<T>();
    }

    protected virtual void ValidateAndThrow()
    {

    }

    public abstract void Build(ISqlWriter writer);

    public string Build()
    {
        using (var writer=SqlWriter.New)
        {
            Build(writer);
            return writer.Build();
        }
    }

    protected SqlEngineException Bomb(string message = "")
    {
        if (string.IsNullOrEmpty(message)) message = "Invalid Usage of QueryBuilder: " + GetType().Name;
        throw new SqlEngineException(message);
    }

    protected static string I(string name)
    {
        return Query.Settings.EscapeStrategy.Escape(name);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Writer?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}