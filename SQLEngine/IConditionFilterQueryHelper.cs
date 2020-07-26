using System;

namespace SQLEngine
{
    public interface IConditionFilterQueryHelper
    {
        //string Equal(string left, string right);
        //string ColumnGreaterThan(string columnName, ISqlExpression value);
        //string ColumnLessThan(string columnName, ISqlExpression value);
        AbstractSqlCondition Exists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
        AbstractSqlCondition NotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
    }
}