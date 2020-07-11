namespace SQLEngine
{
    public abstract class AbstractSqlVariable: ISqlExpression
    {
        public string Name { get; set; }
        public abstract string ToSqlString();

        public abstract ISqlExpression Add(AbstractSqlVariable y);
        public abstract ISqlExpression Subtract(AbstractSqlVariable y);
        
        
        protected abstract AbstractSqlCondition Greater(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition Less(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition LessEqual(AbstractSqlVariable abstractSqlVariable);


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

        public static ISqlExpression operator +(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Add(y);
        }

        public static ISqlExpression operator -(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Subtract(y);
        }
        public static ISqlExpression operator *(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Multiply(y);
        }

        public abstract  ISqlExpression Multiply(AbstractSqlVariable variable);
        public abstract  ISqlExpression Multiply(AbstractSqlLiteral variable);
        public abstract  ISqlExpression Add(AbstractSqlLiteral literal);
        public abstract  ISqlExpression Divide(AbstractSqlVariable variable);
        public abstract  ISqlExpression Subtract(AbstractSqlLiteral literal);


        public static ISqlExpression operator -(AbstractSqlLiteral x, AbstractSqlVariable y)
        {
            return y.SubtractReverse(x);
        }

        protected abstract ISqlExpression SubtractReverse(AbstractSqlLiteral literal);
        protected abstract ISqlExpression DivideReverse(AbstractSqlLiteral literal);

        public static ISqlExpression operator /(AbstractSqlLiteral x, AbstractSqlVariable y)
        {
            return y.DivideReverse(x);
        }


        public static ISqlExpression operator +(AbstractSqlLiteral x, AbstractSqlVariable y)
        {
            return y.Add(x);
        }
        public static ISqlExpression operator +(AbstractSqlVariable x, AbstractSqlLiteral y)
        {
            return x.Add(y);
        }

        
        public static ISqlExpression operator -(AbstractSqlVariable x, AbstractSqlLiteral y)
        {
            return x.Subtract(y);
        }
        public static ISqlExpression operator *(AbstractSqlLiteral x, AbstractSqlVariable y)
        {
            return y.Multiply(x);
        }
        
        public static ISqlExpression operator /(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Divide(y);
        }
    }
}