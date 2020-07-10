using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class UpdateQueryBuilder : SqlServerQueryBuilder, IUpdateNoTopQueryBuilder, 
        IUpdateNoTableAndTopQueryBuilder, 
        IUpdateNoTableAndValuesAndWhereQueryBuilder,
        IUpdateQueryBuilder, 
        IUpdateNoTableSingleValueQueryBuilder, 
        IUpdateNoTableQueryBuilder, 
        IUpdateNoTableAndValuesQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, string> _columnsAndValuesDictionary;
        private string _whereCondition;
        private int? _topClause;


        private void Validate()
        {
            if (_columnsAndValuesDictionary.Count == 0)
            {
                Bomb();
            }
            if (_topClause != null)
            {
                if (_topClause.Value <= 0) Bomb();
            }
        }

        public IUpdateNoTableQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, string> updateDict)
        {
            _columnsAndValuesDictionary = updateDict;
            return this;
        }
        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, columnValue.ToSqlString());
            return this;
        }
        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> builder)
        {
            var columnValue = builder(GetDefault<BinaryExpressionBuilder>()).Build();
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }

        //public UpdateQueryBuilder Columns(params string[] columnNames)
        //{
        //    _columnNames = columnNames;
        //    return this;
        //}
        //public UpdateQueryBuilder Values(params string[] values)
        //{
        //    _values = values;
        //    return this;
        //}
        public IUpdateNoTopQueryBuilder Top(int? count)
        {
            _topClause = count;
            return this;
        }
        public IUpdateNoTableAndValuesAndWhereQueryBuilder Where(string condition)
        {
            _whereCondition = condition;
            return this;
        }
        public IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            return this;
        }

        public IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right)
        {
            _whereCondition = columnName + SQLKeywords.EQUALS + right.ToSqlString();
            return this;
        }

        //public IUpdateNoTableAndValuesAndWhereQueryBuilder Where(Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder)
        //{
        //    _whereCondition = builder.Invoke(GetDefault<BinaryConditionExpressionBuilder>()).Build();
        //    return this;
        //}

        public override string Build()
        {
            Validate();

            Writer.Write2(SQLKeywords.UPDATE);
            if (_topClause != null)
            {
                Writer.Write(SQLKeywords.TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write(SQLKeywords.SPACE);
            }
            Writer.Write(I(_tableName));
            Writer.WriteLine();
            Writer.Indent++;
            Writer.Write2(SQLKeywords.SET);
            if (_columnsAndValuesDictionary != null)
            {
                var keys = _columnsAndValuesDictionary.Keys.ToArray();
                for (var i = 0; i < _columnsAndValuesDictionary.Count; i++)
                {
                    var key = keys[i];
                    var value = _columnsAndValuesDictionary[key];
                    Writer.Write(I(key));
                    Writer.Write2(SQLKeywords.EQUALS);
                    Writer.Write(value);
                    if (i != _columnsAndValuesDictionary.Count - 1)
                        Writer.Write2(SQLKeywords.COMMA);
                }
            }

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                Writer.WriteLine();
                Writer.Write2(SQLKeywords.WHERE);
                Writer.WriteScoped(_whereCondition);
            }
            Writer.Indent--;

            return base.Build();
        }
    }
}