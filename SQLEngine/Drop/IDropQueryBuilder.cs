namespace SQLEngine;

public interface IDropQueryBuilder
{
    IDropDatabaseNoNameQueryBuilder Database(string databaseName);

    IDropFunctionQueryBuilder Function(string funcName);
    IDropProcedureNoNameQueryBuilder Procedure(string procedureName);
    IDropTableNoNameQueryBuilder Table(string tableName);
    IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable, new();
    IDropTriggerNoNameQueryBuilder Trigger(string triggerName);
    IDropViewNoNameQueryBuilder View(string viewName);
}