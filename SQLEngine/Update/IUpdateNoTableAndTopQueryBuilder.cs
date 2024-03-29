﻿using System.Collections.Generic;

namespace SQLEngine;

public interface IUpdateNoTableAndTopQueryBuilder : IAbstractUpdateQueryBuilder
{
    IUpdateNoTableQueryBuilder Table(string tableName, string schema = null);
    IUpdateNoTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue);
    IUpdateNoTableSingleValueQueryBuilder Value(string columnName, ISqlExpression expression);
    IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, ISqlExpression> updateDict);
    IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> updateDict);
}