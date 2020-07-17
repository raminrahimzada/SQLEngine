namespace SQLEngine.SqlServer
{
    internal class DropDatabaseQueryBuilder : AbstractQueryBuilder, IDropDatabaseNoNameQueryBuilder, IDropDatabaseQueryBuilder
    {
        private string _databaseName;

        public override string Build()
        {
            Writer.Write(C.DROP);
            Writer.Write(C.DATABASE);
            Writer.Write(C.SPACE);
            Writer.Write(_databaseName);
            return base.Build();
        }

        public IDropDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }
    }
}