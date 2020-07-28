using System;
using System.Collections.Generic;

namespace SQLEngine
{
    public interface IInsertWithValuesQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues);
        IInsertNoValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> colsAndValuesAsLiterals);
        IInsertNoValuesQueryBuilder Values(Dictionary<string, AbstractSqlExpression> colsAndValuesAsLiterals);
        IInsertNoValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder);
    }
}