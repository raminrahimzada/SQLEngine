using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine
{
    public  class UpdateQueryBuilder : AbstractQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, string> _columnsAndValuesDictionary;
        private string[] _values;
        private string[] _columnNames;
        private string _whereCondition;
        private int? _topClause;

        private void Validate()
        {
            if (_columnsAndValuesDictionary == null)
            {
                if (_values != null)
                {
                    Boom();
                }
            }
            else
            {
                if (_columnsAndValuesDictionary.Count==0)
                {
                     Boom();
                }
                if (_values != null)
                {                    
                    Boom();
                }

                if (_columnNames != null)
                {
                    Boom();
                }
            }

            if (_columnNames != null && _values != null)
            {
                if (_columnNames.Length != _values.Length)
                {
                    Boom();
                }
                if (_values.Length == 0)
                {
                    Boom();
                }
                if (_columnNames.Length == 0)
                {
                    Boom();
                }
            }
            if (_topClause != null)
            {
                if(_topClause.Value<=0) Boom();
            }
        }
        public UpdateQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public UpdateQueryBuilder Values(Dictionary<string, string> updateDict)
        {
            _columnsAndValuesDictionary = updateDict;
            return this;
        }
        public UpdateQueryBuilder Value(string columnName,string columnValue)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }
        public UpdateQueryBuilder Where(string condition)
        {
            _whereCondition = condition;
            return this;
        }
        public UpdateQueryBuilder Columns(params string[] columnNames)
        {
            _columnNames = columnNames;
            return this;
        }
        public UpdateQueryBuilder Values(params string[] values)
        {
            _values = values;
            return this;
        }
        public UpdateQueryBuilder Top(int count)
        {
            _topClause = count;
            return this;
        }
        public UpdateQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            return this;
        }
        public UpdateQueryBuilder Where(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();
            return this;
        }
        public UpdateQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();
            return this;
        }
        public override string Build()
        {
            Validate();

            Writer.Write("UPDATE ");
            if (_topClause != null)
            {
                Writer.Write("TOP");
                Writer.WriteWithScoped(_topClause.Value.ToString());
                Writer.Write(" ");
            }
            Writer.Write(_tableName);
            Writer.Write(" SET ");

            if (_columnsAndValuesDictionary != null)
            {
                var keys = _columnsAndValuesDictionary.Keys.ToArray();
                for (int i = 0; i < _columnsAndValuesDictionary.Count; i++)
                {
                    var key = keys[i];
                    var value = _columnsAndValuesDictionary[key];
                    Writer.Write(key);
                    Writer.Write(" = ");
                    Writer.Write(value);
                    if (i != _columnsAndValuesDictionary.Count-1)
                        Writer.Write(" , ");
                }
            }
            else //normal model 
            {
                var len = _values.Length;
                for (var i = 0; i < len; i++)
                {
                    var column = _columnNames[i];
                    var value = _values[i];
                    Writer.Write(column);
                    Writer.Write(" = ");
                    Writer.Write(value);
                    if (i != len-1)
                        Writer.Write(" , ");
                }
            }

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                Writer.Write(" WHERE ");
                Writer.WriteWithScoped(_whereCondition);
            }
            return base.Build();
        }
    }
}