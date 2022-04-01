namespace SQLEngine;

public interface IDropViewQueryBuilder : IAbstractQueryBuilder
{
    IDropViewNoNameQueryBuilder View(string viewName);
    IDropViewNoSchemaQueryBuilder FromSchema(string schema);
}