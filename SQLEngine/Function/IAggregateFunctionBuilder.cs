﻿namespace SQLEngine;

public interface IAggregateFunctionBuilder : IAbstractQueryBuilder
{
    IAggregateFunctionBuilder All();


    IAggregateFunctionBuilder Avg(ISqlExpression expression);
    IAggregateFunctionBuilder Avg(AbstractSqlColumn column);
    IAggregateFunctionBuilder Avg(string columnName);


    IAggregateFunctionBuilder Count(AbstractSqlLiteral literal);
    IAggregateFunctionBuilder Count(ISqlExpression expression);
    IAggregateFunctionBuilder Count(AbstractSqlColumn column);
    IAggregateFunctionBuilder Count(string columnName);


    IAggregateFunctionBuilder Distinct();

    IAggregateFunctionBuilder Max(ISqlExpression expression);
    IAggregateFunctionBuilder Max(AbstractSqlColumn column);
    IAggregateFunctionBuilder Max(string columnName);
    IAggregateFunctionBuilder Min(ISqlExpression expression);
    IAggregateFunctionBuilder Min(AbstractSqlColumn column);
    IAggregateFunctionBuilder Min(string columnName);


    IAggregateFunctionBuilder Sum(AbstractSqlLiteral literal);
    IAggregateFunctionBuilder Sum(ISqlExpression expression);
    IAggregateFunctionBuilder Sum(AbstractSqlColumn column);
    IAggregateFunctionBuilder Sum(string columnName);
}