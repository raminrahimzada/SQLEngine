using System.Collections.Generic;

namespace SQLEngine
{
    public interface IUpdateNoTableQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, ISqlExpression> updateDict);
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> updateDict);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
        
    }
}