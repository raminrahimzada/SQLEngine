using System;
using System.Collections.Generic;

namespace SQLEngine
{
    public interface IInsertWithValuesQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues);
        IInsertNoValuesQueryBuilder Values(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder);
    }
}