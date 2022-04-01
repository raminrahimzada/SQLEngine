using System;
using System.Collections.Generic;

namespace SQLEngine;

public interface IInsertWithValuesQueryBuilder : IAbstractInsertQueryBuilder
{
    IInsertHasValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues);
    IInsertHasValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> colsAndValuesAsLiterals);
    IInsertHasValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder);
    IInsertHasValuesQueryBuilder Values(params AbstractSqlLiteral[] literals);
    IInsertHasValuesQueryBuilder Values(params ISqlExpression[] literals);
}