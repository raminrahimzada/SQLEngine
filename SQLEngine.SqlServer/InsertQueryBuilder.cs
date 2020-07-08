using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class InsertQueryBuilder : SqlServerAbstractQueryBuilder, IInsertNoIntoWithColumns, IInsertNoValuesQueryBuilder, IInsertNeedValueQueryBuilder,IInsertQueryBuilder, IInsertNoIntoQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, string> _columnsAndValuesDictionary;
        private string[] _valuesList;
        private string[] _columnNames;
        private string _selection;

        protected override void ValidateAndThrow()
        {
            if (_columnsAndValuesDictionary == null)
            {
                if (_valuesList == null)
                {
                    if (string.IsNullOrEmpty(_selection))
                    {
                        throw Bomb();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_selection))
                    {
                        throw Bomb();
                    }
                }
            }
            else
            {
                if (_valuesList != null)
                {
                    throw Bomb();
                }

                if (_columnNames != null)
                {
                    throw Bomb();
                }
            }
        }
        public IInsertNoIntoQueryBuilder Into(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public IInsertNeedValueQueryBuilder Value(string columnName, string columnValue)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }

        public IInsertNoValuesQueryBuilder Values(Dictionary<string, string> colsAndValues)
        {
            _columnsAndValuesDictionary = colsAndValues;
            return this;
        }
        public IInsertNoValuesQueryBuilder Values(params string[] values)
        {
            _valuesList = values;
            return this;
        }
        public IInsertNoIntoWithColumns Columns(params string[] columnNames)
        {
            _columnNames = columnNames;
            return this;
        }
        public IInsertNoValuesQueryBuilder Values(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder)
        {
            _selection = builder.Invoke(GetDefault<SelectQueryBuilder>()).Build();
            return this;
        }
        public override string Build()
        {
            ValidateAndThrow();

            Writer.Write(SQLKeywords.INSERT_INTO);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(I(_tableName));
            Writer.Write2(SQLKeywords.SPACE);

            var columnNamesSafe = _columnNames?.Select(I).ToArray();
            if (_columnsAndValuesDictionary != null)
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.BeginScope();
                Writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(I).ToArray());
                Writer.EndScope();
                Writer.WriteLine();
                Writer.Indent--;

                Writer.Write(SQLKeywords.VALUES);
                Writer.WriteLine();
                Writer.Indent++;

                Writer.BeginScope();

                Writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(key => _columnsAndValuesDictionary[key]).ToArray());
                Writer.EndScope();
                Writer.Indent--;
            }
            else if (!string.IsNullOrEmpty(_selection))//selection mode
            {
                if (_columnNames != null)
                {
                    Writer.BeginScope();
                    Writer.WriteJoined(columnNamesSafe);
                    Writer.EndScope();
                }
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(_selection);
            }
            else //normal model 
            {
                if (_columnNames != null)
                {
                    Writer.WriteLine();
                    Writer.Indent++;
                    Writer.BeginScope();
                    Writer.WriteJoined(columnNamesSafe);
                    Writer.EndScope();
                    Writer.Indent--;

                }
                Writer.WriteLine();

                Writer.Write(SQLKeywords.VALUES);
                Writer.WriteLine();
                Writer.Indent++;

                Writer.BeginScope();
                Writer.WriteJoined(_valuesList);
                Writer.EndScope();
                Writer.Indent--;
            }
            return base.Build();
        }
    }
}