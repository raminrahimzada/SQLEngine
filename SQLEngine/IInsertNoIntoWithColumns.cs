using System;

namespace SQLEngine
{
    public interface IInsertNoIntoWithColumns : IAbstractInsertQueryBuilder
    {
        IInsertHasValuesQueryBuilder Values(params ISqlExpression[] values);
        IInsertHasValuesQueryBuilder Values(params AbstractSqlLiteral[] values);

        IInsertHasValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder);
    }
}