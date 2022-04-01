namespace SQLEngine;

public interface ICreateQueryBuilder : IAbstractQueryBuilder
{
    ICreateTableQueryBuilder Table(string tableName);
    ICreateTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    ICreateFunctionNoNameQueryBuilder Function(string funcName);
    ICreateProcedureNoNameQueryBuilder Procedure(string procName);
    ICreateViewNoNameQueryBuilder View(string viewName);
    ICreateViewNoNameQueryBuilder View<TView>() where TView : IView, new();
    ICreateIndexNoNameQueryBuilder Index(string indexName);
    ICreateDatabaseNoNameQueryBuilder Database(string databaseName);
    ICreateTriggerNoNameQueryBuilder Trigger(string triggerName);
}