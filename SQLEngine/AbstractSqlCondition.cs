namespace SQLEngine
{
    public abstract class AbstractSqlCondition : ISqlExpression
    {
        public abstract string ToSqlString();

        public static AbstractSqlCondition operator &(AbstractSqlCondition condition1, 
            AbstractSqlCondition condition2)
        {
            return condition1.And(condition2);
        }

        public abstract AbstractSqlCondition And(AbstractSqlCondition condition);
        public abstract AbstractSqlCondition Or(AbstractSqlCondition condition);

        public static AbstractSqlCondition operator |(AbstractSqlCondition condition1, 
            AbstractSqlCondition condition2)
        {
            return condition1.Or(condition2);
        }
    }
}