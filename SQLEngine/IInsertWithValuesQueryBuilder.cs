using System;
using System.Collections.Generic;

namespace SQLEngine
{
    public interface IInsertWithValuesQueryBuilder : IAbstractInsertQueryBuilder
    {
        IInsertNoValuesQueryBuilder Values(Dictionary<string, string> colsAndValues);
        IInsertNoValuesQueryBuilder Values(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder);
    }
}