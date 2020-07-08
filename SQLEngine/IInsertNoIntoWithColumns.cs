using System;

namespace SQLEngine
{
    public interface IInsertNoIntoWithColumns : IAbstractInsertQueryBuilder
    {
        IInsertNoValuesQueryBuilder Values(params string[] values);
        IInsertNoValuesQueryBuilder Values(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder);
    }
}