using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.PostgreSql
{
    internal class CreateTableQueryBuilder : AbstractQueryBuilder, ICreateTableQueryBuilder
    {
        private string _tableName;
        private string _schemaName;
        private readonly List<IColumnQueryBuilder> _columns;
        private bool _systemVersioning;
        private string _logFileName;

        public CreateTableQueryBuilder()
        {
            _columns = new List<IColumnQueryBuilder>();
        }
        public ICreateTableQueryBuilder Name(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public ICreateTableQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public ICreateTableQueryBuilder ResetColumns()
        {
            _columns.Clear();
            return this;
        }

        public ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action)
        {
            var columnsBuilder = New<ColumnsCreateQueryBuilder>();
            var current = action(columnsBuilder);
            _columns.AddRange(current);
            return this;
        }


        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //only one identity is allowed in table
            var count = _columns.Count(c => c.Model.IsIdentity ?? false);
            if (count > 1)
            {
                Bomb();
            }
        }

        public override void Build(ISqlWriter writer)
        {
            throw new NotImplementedException();
        }

        public ICreateTableQueryBuilder SystemVersioned(bool systemVersioning, string logFileName = null)
        {
            _systemVersioning = systemVersioning;
            _logFileName = logFileName;
            return this;
        }
    }
}