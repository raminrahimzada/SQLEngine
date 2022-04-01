using System;

namespace SQLEngine;

public abstract class AbstractSqlExpression: ISqlExpression
{
    private static Func<AbstractSqlExpression> _createEmpty;

    protected static void SetCreateEmpty(Func<AbstractSqlExpression> func)
    {
        _createEmpty = func;
    }

    public abstract string ToSqlString();

    protected abstract void SetFrom(AbstractSqlLiteral literal);
    protected abstract void SetFrom(AbstractSqlVariable variable);

    public static implicit operator AbstractSqlExpression(AbstractSqlLiteral literal)
    {
        var expression = _createEmpty();
        expression.SetFrom(literal);
        return expression;
    }
    public static implicit operator AbstractSqlExpression(AbstractSqlVariable literal)
    {
        var expression = _createEmpty();
        expression.SetFrom(literal);
        return expression;
    }
       

    public static implicit operator AbstractSqlExpression(int x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(bool x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(bool? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(Enum x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(sbyte x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(sbyte? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(ushort? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(uint? x)
    {
        return (AbstractSqlLiteral)x;
    }
        
    public static implicit operator AbstractSqlExpression(decimal x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static implicit operator AbstractSqlExpression(decimal? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(int? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(byte x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static implicit operator AbstractSqlExpression(ushort x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static implicit operator AbstractSqlExpression(double x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static implicit operator AbstractSqlExpression(byte[] x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static implicit operator AbstractSqlExpression(float x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(byte? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(long? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(long x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(short x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(short? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(DateTime x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(DateTime? x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(Guid x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(Guid? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(ulong? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(ulong x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(char x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(char? x)
    {
        return (AbstractSqlLiteral)x;
    }
    public static implicit operator AbstractSqlExpression(uint x)
    {
        return (AbstractSqlLiteral)x;
    }


    public static implicit operator AbstractSqlExpression(string x)
    {
        return (AbstractSqlLiteral)x;
    }

    public static AbstractSqlExpression operator *(AbstractSqlExpression left, ISqlExpression right)
    {
        return left.Multiply(right);
    }

    protected abstract AbstractSqlExpression Multiply(ISqlExpression right);
}