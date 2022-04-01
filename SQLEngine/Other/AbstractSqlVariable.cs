#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()

namespace SQLEngine;

public abstract class AbstractSqlVariable : ISqlExpression
{
    public string Name { get; set; }
    public abstract string ToSqlString();

    public abstract AbstractSqlExpression Add(AbstractSqlVariable y);
    public abstract AbstractSqlExpression Subtract(AbstractSqlVariable y);

    public abstract AbstractSqlCondition In(params AbstractSqlExpression[] expressions);
    public abstract AbstractSqlCondition In(params AbstractSqlLiteral[] expressions);

    public abstract AbstractSqlCondition NotIn(params AbstractSqlExpression[] expressions);
    public abstract AbstractSqlCondition NotIn(params AbstractSqlLiteral[] expressions);

    public abstract AbstractSqlCondition IsNull();
    public abstract AbstractSqlCondition IsNotNull();


    protected abstract AbstractSqlCondition Greater(AbstractSqlVariable abstractSqlVariable);
    protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlVariable abstractSqlVariable);
    protected abstract AbstractSqlCondition Less(AbstractSqlVariable abstractSqlVariable);
    protected abstract AbstractSqlCondition LessEqual(AbstractSqlVariable abstractSqlVariable);

    protected abstract AbstractSqlCondition Greater(AbstractSqlExpression abstractSqlVariable);
    protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlExpression abstractSqlVariable);
    protected abstract AbstractSqlCondition Less(AbstractSqlExpression abstractSqlVariable);
    protected abstract AbstractSqlCondition LessEqual(AbstractSqlExpression abstractSqlVariable);


    protected abstract AbstractSqlCondition Greater(AbstractSqlLiteral literal);
    protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlLiteral literal);
    protected abstract AbstractSqlCondition Less(AbstractSqlLiteral literal);
    protected abstract AbstractSqlCondition LessEqual(AbstractSqlLiteral literal);


    protected abstract AbstractSqlCondition Greater(AbstractSqlColumn column);
    protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlColumn column);
    protected abstract AbstractSqlCondition Less(AbstractSqlColumn column);
    protected abstract AbstractSqlCondition LessEqual(AbstractSqlColumn column);


    public static AbstractSqlCondition operator ==(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.EqualsTo(y);
    }

    protected abstract AbstractSqlCondition EqualsTo(AbstractSqlColumn column);
    protected abstract AbstractSqlCondition EqualsTo(AbstractSqlLiteral literal);
    protected abstract AbstractSqlCondition NotEqualsTo(AbstractSqlColumn column);
    protected abstract AbstractSqlCondition NotEqualsTo(AbstractSqlLiteral literal);


    protected abstract AbstractSqlCondition EqualsTo(AbstractSqlVariable variable);
    protected abstract AbstractSqlCondition NotEqualsTo(AbstractSqlVariable variable);

    public static AbstractSqlCondition operator !=(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.NotEqualsTo(y);
    }

    public static AbstractSqlCondition operator ==(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.EqualsTo(y);
    }

    public static AbstractSqlCondition operator !=(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.NotEqualsTo(y);
    }

    public static AbstractSqlCondition operator !=(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.NotEqualsTo(y);
    }

    public static AbstractSqlCondition operator ==(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.EqualsTo(y);
    }

    public static AbstractSqlCondition operator >(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.Greater(y);
    }

    public static AbstractSqlCondition operator <(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.Less(y);
    }

    public static AbstractSqlCondition operator >=(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.GreaterEqual(y);
    }

    public static AbstractSqlCondition operator <=(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.LessEqual(y);
    }


    public static AbstractSqlCondition operator <(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.Less(y);
    }

    public static AbstractSqlCondition operator <=(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.LessEqual(y);
    }

    public static AbstractSqlCondition operator >(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.Greater(y);
    }

    public static AbstractSqlCondition operator >=(AbstractSqlVariable x, AbstractSqlColumn y)
    {
        return x.GreaterEqual(y);
    }

    public static AbstractSqlCondition operator <(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Less(y);
    }

    public static AbstractSqlCondition operator <=(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.LessEqual(y);
    }

    public static AbstractSqlCondition operator >(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Greater(y);
    }

    public static AbstractSqlCondition operator >=(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.GreaterEqual(y);
    }


    public static AbstractSqlCondition operator <(AbstractSqlVariable x, AbstractSqlExpression y)
    {
        return x.Less(y);
    }

    public static AbstractSqlCondition operator <=(AbstractSqlVariable x, AbstractSqlExpression y)
    {
        return x.LessEqual(y);
    }

    public static AbstractSqlCondition operator >(AbstractSqlVariable x, AbstractSqlExpression y)
    {
        return x.Greater(y);
    }

    public static AbstractSqlCondition operator >=(AbstractSqlVariable x, AbstractSqlExpression y)
    {
        return x.GreaterEqual(y);
    }


    public static AbstractSqlExpression operator +(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Add(y);
    }

    public static AbstractSqlExpression operator -(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Subtract(y);
    }

    public static AbstractSqlExpression operator -(AbstractSqlVariable x)
    {
        return 0 - x;
    }

    public static AbstractSqlExpression operator *(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Multiply(y);
    }
    public static AbstractSqlExpression operator *(AbstractSqlVariable x, ISqlExpression y)
    {
        return x.Multiply(y);
    }
    public static AbstractSqlExpression operator *(ISqlExpression y, AbstractSqlVariable x)
    {
        return x.MultiplyReverse(y);
    }

    public abstract AbstractSqlExpression Multiply(ISqlExpression expression);
    public abstract AbstractSqlExpression MultiplyReverse(ISqlExpression expression);
    public abstract AbstractSqlExpression Multiply(AbstractSqlVariable variable);
    public abstract AbstractSqlExpression Multiply(AbstractSqlLiteral variable);

    public abstract AbstractSqlExpression Add(AbstractSqlLiteral literal);
    public abstract AbstractSqlExpression Divide(AbstractSqlVariable variable);
    public abstract AbstractSqlExpression Subtract(AbstractSqlLiteral literal);


    public static AbstractSqlExpression operator -(AbstractSqlLiteral x, AbstractSqlVariable y)
    {
        return y.SubtractReverse(x);
    }

    protected abstract AbstractSqlExpression SubtractReverse(AbstractSqlLiteral literal);
    protected abstract AbstractSqlExpression DivideReverse(AbstractSqlLiteral literal);

    public static AbstractSqlExpression operator /(AbstractSqlLiteral x, AbstractSqlVariable y)
    {
        return y.DivideReverse(x);
    }


    public static AbstractSqlExpression operator +(AbstractSqlLiteral x, AbstractSqlVariable y)
    {
        return y.Add(x);
    }

    public static AbstractSqlExpression operator +(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.Add(y);
    }


    public static AbstractSqlExpression operator -(AbstractSqlVariable x, AbstractSqlLiteral y)
    {
        return x.Subtract(y);
    }

    public static AbstractSqlExpression operator *(AbstractSqlLiteral x, AbstractSqlVariable y)
    {
        return y.Multiply(x);
    }

    public static AbstractSqlExpression operator /(AbstractSqlVariable x, AbstractSqlVariable y)
    {
        return x.Divide(y);
    }
}