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

        public static ISqlExpression operator -(AbstractSqlVariable x, AbstractSqlVariable y)
        {
            return x.Subtract(y);
        }
    }
}