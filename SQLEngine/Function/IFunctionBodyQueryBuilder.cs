namespace SQLEngine;

public interface IFunctionBodyQueryBuilder : IQueryBuilder
{
    AbstractSqlVariable Param(string name);
}