namespace SQLEngine
{
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
}