using System.Collections.Generic;

namespace SQLEngine
{
    public interface IUpdateNoTableAndTopQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableQueryBuilder Table(string tableName);
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, ISqlExpression> updateDict);
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> updateDict);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);

        //IUpdateNoTableSingleValueQueryBuilder Value(string columnName,
        //    Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder);
    }
}