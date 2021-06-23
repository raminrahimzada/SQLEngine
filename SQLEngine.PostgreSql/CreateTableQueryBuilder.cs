using System;

namespace SQLEngine.PostgreSql
{
    // ReSharper disable NotAccessedField.Local
    internal class CreateTableQueryBuilder : AbstractQueryBuilder, ICreateTableQueryBuilder
    {
        //private string _tableName;
        //private string _schemaName;
        //private readonly List<IColumnQueryBuilder> _columns;

        //public CreateTableQueryBuilder()
        //{
        //    _columns = new List<IColumnQueryBuilder>();
        //}
        //public ICreateTableQueryBuilder Name(string tableName)
        //{
        //    _tableName = tableName;
        //    return this;
        //}
        //public ICreateTableQueryBuilder Schema(string schemaName)
        //{
        //    _schemaName = schemaName;
        //    return this;
        //}

        //public ICreateTableQueryBuilder ResetColumns()
        //{
        //    _columns.Clear();
        //    return this;
        //}

        //public ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action)
        //{
        //    var columnsBuilder = New<ColumnsCreateQueryBuilder>();
        //    var current = action(columnsBuilder);
        //    _columns.AddRange(current);
        //    return this;
        //}


        //protected override void ValidateAndThrow()
        //{
        //    base.ValidateAndThrow();
        //    //only one identity is allowed in table
        //    var count = _columns.Count(c => c.Model.IsIdentity ?? false);
        //    if (count > 1)
        //    {
        //        Bomb();
        //    }
        //}

        //public override void Build(ISqlWriter writer)
        //{
        //    throw new NotImplementedException();
        //}

        //public ICreateTableQueryBuilder SystemVersioned(bool systemVersioning, string logFileName = null)
        //{
        //    _systemVersioning = systemVersioning;
        //    _logFileName = logFileName;
        //    return this;
        //}
        public override void Build(ISqlWriter writer)
        {
            throw new NotImplementedException();
        }

        public ICreateTableQueryBuilder Name(string tableName)
        {
            throw new NotImplementedException();
        }

        public ICreateTableQueryBuilder Schema(string schemaName)
        {
            throw new NotImplementedException();
        }

        public ICreateTableQueryBuilder ResetColumns()
        {
            throw new NotImplementedException();
        }

        public ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action)
        {
            throw new NotImplementedException();
        }
    }
}
