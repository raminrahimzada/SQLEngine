using System;
using System.Collections.Generic;

namespace SQLEngine
{
    public interface IUpdateNoTableQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, string> updateDict);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName,
            Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder);
    }
}