namespace SQLEngine.SqlServer
{
    internal class DropQueryBuilder :AbstractQueryBuilder, IDropQueryBuilder, IDropTableQueryBuilder
    {
        private IAbstractQueryBuilder _internalBuilder;
        public IDropTableNoNameQueryBuilder Table(string tableName)
        {
            var b = new DropTableQueryBuilder();
            _internalBuilder = b.Table(tableName);
            return b;
        }

        public IDropTableNoNameQueryBuilder Table<TTable>() where TTable : ITable,new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name);
            }
        }

        public IDropFunctionQueryBuilder Function(string funcName)
        {
            var b = new DropFunctionQueryBuilder();
            _internalBuilder = b.FunctionName(funcName);
            return b;
        }

        public IDropViewNoNameQueryBuilder View(string viewName)
        {
            var b = new DropViewQueryBuilder();
            _internalBuilder = b.View(viewName);
            return b;
        }

        public IDropDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            var b = new DropDatabaseQueryBuilder();
            _internalBuilder = b.Database(databaseName);
            return b;
        }

        public IDropTriggerNoNameQueryBuilder Trigger(string triggerName)
        {
            var b = new DropTriggerQueryBuilder();
            _internalBuilder = b.Trigger(triggerName);
            return b;
        }

        public override void Build(ISqlWriter writer)
        {
            _internalBuilder?.Build(writer);
        }
    }
}