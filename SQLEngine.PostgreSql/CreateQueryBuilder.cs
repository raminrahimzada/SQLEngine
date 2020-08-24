﻿namespace SQLEngine.PostgreSql
{
    internal class CreateQueryBuilder : AbstractQueryBuilder, ICreateQueryBuilder
    {
        private IAbstractQueryBuilder _innerBuilder;

        public CreateQueryBuilder()
        {
            
        }
        public override void Build(ISqlWriter writer)
        {
            _innerBuilder?.Build(writer);
        }

        public override string ToString()
        {
            using (var writer=CreateNewWriter())
            {
                Build(writer);
                return writer.Build();
            }
        }

        public ICreateTableQueryBuilder Table(string tableName)
        {
            var x = New<CreateTableQueryBuilder>().Name(tableName);
            _innerBuilder = x;
            return x;
        }
        public ICreateTableQueryBuilder Table<TTable>() where TTable:ITable,new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name);
            }
        }
        public ICreateFunctionNoNameQueryBuilder Function(string funcName)
        {
            var x = New<CreateFunctionQueryBuilder>().Name(funcName);
            _innerBuilder = x;
            return x;
        }

        public ICreateProcedureNoNameQueryBuilder Procedure(string procName)
        {
            var x = New<CreateProcedureQueryBuilder>().Name(procName);
            _innerBuilder = x;
            return x;
        }

        public ICreateViewNoNameQueryBuilder View(string viewName)
        {
            var x = New<CreateViewQueryBuilder>().Name(viewName);
            _innerBuilder = x;
            return x;
        }

        public ICreateIndexNoNameQueryBuilder Index(string indexName)
        {
            var x = New<CreateIndexQueryBuilder>().Name(indexName);
            _innerBuilder = x;
            return x;
        }

        public ICreateDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            var x=New<CreateDatabaseQueryBuilder>().Name(databaseName);
            _innerBuilder = x;
            return x;
        }
    }
}