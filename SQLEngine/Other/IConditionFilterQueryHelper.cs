using System;

namespace SQLEngine;

public interface IConditionFilterQueryHelper
{
    AbstractSqlExpression Null { get; }
    AbstractSqlExpression Now { get; }

    AbstractSqlCondition Exists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
    AbstractSqlCondition NotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
}