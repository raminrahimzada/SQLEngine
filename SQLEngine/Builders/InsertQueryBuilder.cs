using System;
using System.Collections.Generic;
using System.Linq;

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

            Writer.Write("INSERT INTO ");
            Writer.Write(_tableName);

            if (_columnsAndValuesDictionary!=null)
            {
                Writer.Write("(");
                var columns = string.Join(" , ", _columnsAndValuesDictionary.Keys);
                Writer.Write(columns);
                Writer.Write(") VALUES (");
                var values = string.Join(" , ", _columnsAndValuesDictionary.Keys.Select(key => _columnsAndValuesDictionary[key]));
                Writer.Write(values);
                Writer.Write(")");
            }
            else if(!string.IsNullOrEmpty(_selection))//selection mode
            {
                if (_columnNames!=null)
                {
                    Writer.Write(" (");
                    var columns = string.Join(" , ", _columnNames);
                    Writer.Write(columns);
                    Writer.Write(")");
                }
                Writer.Write(" ");
                Writer.Write(_selection);
            }
            else //normal model 
            {
                if (_columnNames != null)
                {
                    Writer.Write("(");
                    var columns = string.Join(" , ", _columnNames);
                    Writer.Write(columns);
                    Writer.Write(")");
                }

                Writer.Write(" VALUES (");
                var values = string.Join(" , ", _valuesList);
                Writer.Write(values);
                Writer.Write(")");
            }
            return base.Build();
        }
    }
}