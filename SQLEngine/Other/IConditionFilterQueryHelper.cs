using System;

namespace SQLEngine;

public interface IConditionFilterQueryHelper
{
    AbstractSqlExpression Now { get; }
    AbstractSqlExpression Null { get; }

    AbstractSqlCondition Exists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
    AbstractSqlCondition NotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func);
}