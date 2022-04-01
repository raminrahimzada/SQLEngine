using System;

namespace SQLEngine;

public abstract class AbstractSqlCondition : ISqlExpression
{
    private static Func<AbstractSqlCondition> _createEmpty;

    protected static void SetCreateEmpty(Func<AbstractSqlCondition> func)
    {
        _createEmpty = func;
    }

    public abstract string ToSqlString();

        
    public static AbstractSqlCondition operator &(AbstractSqlCondition condition1, 
        AbstractSqlCondition condition2)
    {
        return condition1.And(condition2);
    }

    protected abstract void SetRaw(bool rawValue);
    protected abstract void SetRaw(bool? rawValue);

    public abstract AbstractSqlCondition And(AbstractSqlCondition condition);
    public abstract AbstractSqlCondition Or(AbstractSqlCondition condition);

    public static AbstractSqlCondition operator |(AbstractSqlCondition condition1, 
        AbstractSqlCondition condition2)
    {
        return condition1.Or(condition2);
    }
        
    public static implicit operator AbstractSqlCondition(bool? x)
    {
        var empty = _createEmpty();
        empty.SetRaw(x);
        return empty;
    }

    public static implicit operator AbstractSqlCondition(bool x)
    {
        var empty = _createEmpty();
        empty.SetRaw(x);
        return empty;
    }
}