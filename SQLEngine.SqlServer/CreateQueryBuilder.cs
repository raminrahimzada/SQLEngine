namespace SQLEngine.SqlServer
{
    internal class CreateQueryBuilder : AbstractQueryBuilder, ICreateQueryBuilder
    {
        public ICreateTableQueryBuilder Table(string tableName)
        {
            return  GetDefault<CreateTableQueryBuilder>().Name(tableName);
        }
        public ICreateFunctionNoNameQueryBuilder Function(string funcName)
        {
            return GetDefault<CreateFunctionQueryBuilder>().Name(funcName);
        }

        public ICreateProcedureNoNameQueryBuilder Procedure(string procName)
        {
            return GetDefault<CreateProcedureQueryBuilder>().Name(procName);
        }

        public ICreateViewNoNameQueryBuilder View(string viewName)
        {
            return GetDefault<CreateViewQueryBuilder>().Name(viewName);
        }

        public ICreateIndexNoNameQueryBuilder Index(string indexName)
        {
            return GetDefault<CreateIndexQueryBuilder>().Name(indexName);
        }

        public ICreateDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            return GetDefault<CreateDatabaseQueryBuilder>().Name(databaseName);
        }
    }
}