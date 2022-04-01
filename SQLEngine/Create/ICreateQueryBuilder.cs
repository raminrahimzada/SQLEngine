namespace SQLEngine;

public interface ICreateQueryBuilder : IAbstractQueryBuilder
{
    ICreateDatabaseNoNameQueryBuilder Database(string databaseName);
    ICreateFunctionNoNameQueryBuilder Function(string funcName);
    ICreateIndexNoNameQueryBuilder Index(string indexName);
    ICreateProcedureNoNameQueryBuilder Procedure(string procName);
    ICreateTableQueryBuilder Table(string tableName);
    ICreateTableQueryBuilder Table<TTable>() where TTable : ITable, new();
    ICreateTriggerNoNameQueryBuilder Trigger(string triggerName);
    ICreateViewNoNameQueryBuilder View(string viewName);
    ICreateViewNoNameQueryBuilder View<TView>() where TView : IView, new();
}