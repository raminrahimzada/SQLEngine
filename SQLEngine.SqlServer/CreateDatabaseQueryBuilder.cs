namespace SQLEngine.SqlServer
{
    internal class CreateDatabaseQueryBuilder : AbstractQueryBuilder, ICreateDatabaseNoNameQueryBuilder
    {
        private string _databaseName;

        public ICreateDatabaseNoNameQueryBuilder Name(string databaseName)
        {
            this._databaseName = databaseName;
            return this;
        }

        public override string Build()
        {
            Writer.Write(C.CREATE);
            Writer.Write2(C.DATABASE);
            Writer.Write(_databaseName);
            return base.Build();
        }
    }
}