﻿namespace SQLEngine.SqlServer;

internal sealed class DropDatabaseQueryBuilder : AbstractQueryBuilder, IDropDatabaseNoNameQueryBuilder, IDropDatabaseQueryBuilder
{
    private string _databaseName;

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.DROP);
        writer.Write(C.SPACE);
        writer.Write(C.DATABASE);
        writer.Write(C.SPACE);
        writer.Write(_databaseName);
    }

    public IDropDatabaseNoNameQueryBuilder Database(string databaseName)
    {
        _databaseName = databaseName;
        return this;
    }
}