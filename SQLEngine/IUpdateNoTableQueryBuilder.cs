using System.Collections.Generic;

namespace SQLEngine
{
    public interface IUpdateNoTableQueryBuilder : IAbstractUpdateQueryBuilder
    {
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlExpression> updateDict);
        IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> updateDict);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable);
        IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlExpression expression);
        //IUpdateNoTableSingleValueQueryBuilder Value(string columnName, ISqlExpression expression);
        
    }
}