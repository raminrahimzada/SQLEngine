namespace SQLEngine
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public abstract class AbstractSqlVariable: ISqlExpression
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public string Name { get; set; }
        public abstract string ToSqlString();

        public abstract ISqlExpression Add(AbstractSqlVariable y);
        public abstract ISqlExpression Subtract(AbstractSqlVariable y);

        public abstract AbstractSqlCondition In(params ISqlExpression[] expressions);
        public abstract AbstractSqlCondition In(params AbstractSqlLiteral[] expressions);
        
        public abstract AbstractSqlCondition NotIn(params ISqlExpression[] expressions);
        public abstract AbstractSqlCondition NotIn(params AbstractSqlLiteral[] expressions);

        public abstract AbstractSqlCondition IsNull();
        public abstract AbstractSqlCondition IsNotNull();


        protected abstract AbstractSqlCondition Greater(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition Less(AbstractSqlVariable abstractSqlVariable);
        protected abstract AbstractSqlCondition LessEqual(AbstractSqlVariable abstractSqlVariable);

        protected abstract AbstractSqlCondition Greater(ISqlExpression abstractSqlVariable);
        protected abstract AbstractSqlCondition GreaterEqual(ISqlExpression abstractSqlVariable);
        protected abstract AbstractSqlCondition Less(ISqlExpression abstractSqlVariable);
        protected abstract AbstractSqlCondition LessEqual(ISqlExpression abstractSqlVariable);



        
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


        public static AbstractSqlCondition operator <(AbstractSqlVariable x, ISqlExpression y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlVariable x, ISqlExpression y)
        {
            return x.LessEqual(y);
        }

        public static AbstractSqlCondition operator >(AbstractSqlVariable x, ISqlExpression y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlVariable x, ISqlExpression y)
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