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
}