using System;

namespace SQLEngine
{
    public interface ICreateTableQueryBuilder : IAbstractQueryBuilder
    {
        ICreateTableQueryBuilder Name(string tableName);
        ICreateTableQueryBuilder Schema(string schemaName);
        ICreateTableQueryBuilder ResetColumns();
        ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action);
        ICreateTableQueryBuilder SystemVersioned(bool systemVersioning, string logFileName = null);
        void Join(AbstractQueryBuilder other);
    }
}