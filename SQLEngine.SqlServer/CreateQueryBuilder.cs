namespace SQLEngine.SqlServer
{
    internal class CreateQueryBuilder : AbstractQueryBuilder, ICreateQueryBuilder
    {
        private IAbstractQueryBuilder _innerBuilder;

        public CreateQueryBuilder()
        {
            
        }
        public override string Build()
        {
            return _innerBuilder.Build();
        }

        public override string ToString()
        {
            return Build();
        }

        public ICreateTableQueryBuilder Table(string tableName)
        {
            var x = GetDefault<CreateTableQueryBuilder>().Name(tableName);
            _innerBuilder = x;
            return x;
        }
        public ICreateFunctionNoNameQueryBuilder Function(string funcName)
        {
            var x = GetDefault<CreateFunctionQueryBuilder>().Name(funcName);
            _innerBuilder = x;
            return x;
        }

        public ICreateProcedureNoNameQueryBuilder Procedure(string procName)
        {
            var x = GetDefault<CreateProcedureQueryBuilder>().Name(procName);
            _innerBuilder = x;
            return x;
        }

        public ICreateViewNoNameQueryBuilder View(string viewName)
        {
            var x = GetDefault<CreateViewQueryBuilder>().Name(viewName);
            _innerBuilder = x;
            return x;
        }

        public ICreateIndexNoNameQueryBuilder Index(string indexName)
        {
            var x = GetDefault<CreateIndexQueryBuilder>().Name(indexName);
            _innerBuilder = x;
            return x;
        }

        public ICreateDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            var x=GetDefault<CreateDatabaseQueryBuilder>().Name(databaseName);
            _innerBuilder = x;
            return x;
        }
    }
}