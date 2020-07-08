namespace SQLEngine.SqlServer
{
    internal class CreateQueryBuilder :SqlServerQueryBuilder, ICreateQueryBuilder
    {
        public ICreateTableQueryBuilder Table(string tableName)
        {
            ICreateTableQueryBuilder defaultValue = GetDefault<CreateTableQueryBuilder>();
            defaultValue.Name(tableName);
            return defaultValue;
        }

        public ICreateFunctionNoNameQueryBuilder Function(string funcName)
        {
            ICreateFunctionQueryBuilder defaultValue = GetDefault<CreateFunctionQueryBuilder>();
            return defaultValue.Name(funcName);
        }

        public ICreateProcedureNoNameQueryBuilder Procedure(string procName)
        {
            return GetDefault<CreateProcedureQueryBuilder>().Name(procName);
        }

        public ICreateViewNoNameQueryBuilder View(string viewName)
        {
            return GetDefault<CreateViewQueryBuilder>().Name(viewName);
        }
    }
}