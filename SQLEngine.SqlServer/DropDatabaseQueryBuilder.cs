﻿namespace SQLEngine.SqlServer
{
    internal class DropDatabaseQueryBuilder : AbstractQueryBuilder, IDropDatabaseNoNameQueryBuilder, IDropDatabaseQueryBuilder
    {
        private string _databaseName;
        

        public override string Build()
        {
            Writer.Write(SQLKeywords.DROP);
            Writer.Write(SQLKeywords.DATABASE);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(_databaseName);
            return base.Build();
        }

        public IDropDatabaseNoNameQueryBuilder Database(string databaseName)
        {
            this._databaseName = databaseName;
            return this;
        }
    }
}