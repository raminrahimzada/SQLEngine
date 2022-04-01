namespace SQLEngine;

public interface IDropViewQueryBuilder : IAbstractQueryBuilder
{
    IDropViewNoSchemaQueryBuilder FromSchema(string schema);
    IDropViewNoNameQueryBuilder View(string viewName);
}