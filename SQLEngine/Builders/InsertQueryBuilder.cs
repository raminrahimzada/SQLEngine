using System;
using System.Collections.Generic;
using System.Linq;
using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public  class InsertQueryBuilder : AbstractQueryBuilder
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
                        Boom();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(_selection))
                    {
                        Boom();
                    }
                }
            }
            else
            {
                if (_valuesList != null)
                {
                    Boom();
                }

                if (_columnNames != null)
                {
                    Boom();
                }
            }
        }
        public InsertQueryBuilder Into(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public InsertQueryBuilder Value(string columnName,string columnValue)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }
        public InsertQueryBuilder Values(Dictionary<string,string> colsAndValues)
        {
            _columnsAndValuesDictionary = colsAndValues;
            return this;
        }
        public InsertQueryBuilder Values(params string[] values)
        {
            _valuesList = values;
            return this;
        }
        public InsertQueryBuilder Values(IEnumerable<string> values)
        {
            _valuesList = values.ToArray();
            return this;
        }
        public InsertQueryBuilder Columns(params string[] columnNames)
        {
            _columnNames = columnNames;
            return this;
        }
        public InsertQueryBuilder Values(Func<SelectQueryBuilder,SelectQueryBuilder> builder)
        {
            _selection = builder.Invoke(GetDefault<SelectQueryBuilder>()).Build();
            return this;
        }

        public override string Build()
        {
            ValidateAndThrow();

            Writer.Write(INSERT_INTO);
            Writer.Write2(SPACE);
            Writer.Write(I(_tableName));
            Writer.Write2(SPACE);

            var columnNamesSafe = _columnNames?.Select(I).ToArray();
            if (_columnsAndValuesDictionary!=null)
            {
                Writer.BeginScope();
                Writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(I).ToArray());
                Writer.EndScope();

                Writer.Write(VALUES);
                Writer.BeginScope();

                Writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(key => _columnsAndValuesDictionary[key]).ToArray());
                Writer.EndScope();
            }
            else if(!string.IsNullOrEmpty(_selection))//selection mode
            {
                if (_columnNames!=null)
                {
                    Writer.BeginScope();
                    Writer.WriteJoined(columnNamesSafe);
                    Writer.EndScope();
                }
                Writer.Write(SPACE);
                Writer.Write(_selection);
            }
            else //normal model 
            {
                if (_columnNames != null)
                {
                    Writer.BeginScope();
                    Writer.WriteJoined(columnNamesSafe);
                    Writer.EndScope();
                }

                Writer.Write(VALUES);
                Writer.BeginScope();
                Writer.WriteJoined(_valuesList);
                Writer.EndScope();
            }
            return base.Build();
        }
    }
}