namespace SQLEngine;

public interface IProcedureBodyQueryBuilder : IQueryBuilder
{
    AbstractSqlVariable Parameter(string name);
}