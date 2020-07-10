namespace SQLEngine
{
    public interface ISqlVariable: ISqlExpression
    {
        string Name { get; set; }
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