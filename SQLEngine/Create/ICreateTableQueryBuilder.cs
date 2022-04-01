using System;

namespace SQLEngine;

public interface ICreateTableQueryBuilder : IAbstractQueryBuilder
{
    ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action);
    ICreateTableQueryBuilder Name(string tableName);
    ICreateTableQueryBuilder ResetColumns();
    ICreateTableQueryBuilder Schema(string schemaName);
}