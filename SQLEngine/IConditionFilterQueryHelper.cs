using System;

namespace SQLEngine
{
    public interface IConditionFilterQueryHelper
    {
        AbstractSqlCondition Exists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
        AbstractSqlCondition NotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
    }
}