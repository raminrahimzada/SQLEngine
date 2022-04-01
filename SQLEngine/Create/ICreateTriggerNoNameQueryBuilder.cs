using System;

namespace SQLEngine;

public interface ICreateTriggerNoNameQueryBuilder : IAbstractQueryBuilder
{
    ICreateTriggerNoNameQueryBuilder Body(Action<ITriggerBodyQueryBuilder> body);
    ICreateTriggerNoNameQueryBuilder On(string tableName);
    ICreateTriggerNoNameQueryBuilder On(string tableName, string tableSchema);
    ICreateTriggerNoNameQueryBuilder ForDelete();
    ICreateTriggerNoNameQueryBuilder ForInsert();
    ICreateTriggerNoNameQueryBuilder AfterUpdate();
    ICreateTriggerNoNameQueryBuilder Schema(string triggerSchemaName);
}