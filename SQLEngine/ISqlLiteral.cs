namespace SQLEngine
{
    public abstract class AbstractSqlVariable: ISqlExpression
    {
        public string Name { get; set; }
        public abstract string ToSqlString();

        public abstract ISqlExpression Add(AbstractSqlVariable y);
        public abstract ISqlExpression Subtract(AbstractSqlVariable y);
        
        public static ISqlExpression operator +(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Add(y);
        }

        public static ISqlExpression operator -(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Subtract(y);
        }
    }
    
    public interface ISqlExpression
    {
        string ToSqlString();
    }
    public abstract class AbstractSqlExpression:ISqlExpression
    {
        public abstract string ToSqlString();
        public override string ToString()
        {
            return ToSqlString();
        }
        public static implicit operator string(AbstractSqlExpression expression)
        {
            return expression.ToSqlString();
        }
    }
    public interface ISqlLiteral: ISqlExpression
    {
        //ISqlString From(int i);
        //ISqlString From(string s);
        //ISqlString From(double d);
        //ISqlString From(decimal d);
        //ISqlString From(float f);
        //ISqlString From(DateTime dt);
    }
}