using System;

namespace SQLEngine;

public interface ICreateTriggerNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateTriggerNoNameQueryBuilder AfterUpdate();
    ICreateTriggerNoNameQueryBuilder Body(Action<ITriggerBodyQueryBuilder> body);
    ICreateTriggerNoNameQueryBuilder ForDelete();
    ICreateTriggerNoNameQueryBuilder ForInsert();
    ICreateTriggerNoNameQueryBuilder On(string tableName);
    ICreateTriggerNoNameQueryBuilder On(string tableName, string tableSchema);
    ICreateTriggerNoNameQueryBuilder Schema(string triggerSchemaName);
}