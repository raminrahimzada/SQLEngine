using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class UpdateQueryBuilder : AbstractQueryBuilder, 
        IUpdateNoTopQueryBuilder, 
        IUpdateNoTableAndTopQueryBuilder, 
        IUpdateNoTableAndValuesAndWhereQueryBuilder,
        IUpdateQueryBuilder, 
        IUpdateNoTableSingleValueQueryBuilder, 
        IUpdateNoTableQueryBuilder, 
        IUpdateNoTableAndValuesQueryBuilder
    {
        private string _tableName;
        private string _tableSchema;
        private Dictionary<string, string> _columnsAndValuesDictionary=new();
        private string _whereCondition;
        private int? _topClause;


        private void Validate()
        {
            if (_columnsAndValuesDictionary.Count == 0)
            {
                Bomb();
            }
            if (_topClause <= 0)
            {
                Bomb();
            }
        }

        public IUpdateNoTableQueryBuilder Table(string tableName,string schema)
        {
            _tableName = tableName;
            _tableSchema = schema;
            return this;
        }

        public IUpdateNoTableQueryBuilder Table<TTable>() where TTable:ITable, new()
        {
            using (var table=new TTable())
            {
                return Table(table.Name,table.Schema);
            }
        }


        public IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, ISqlExpression> updateDict)
        {
            _columnsAndValuesDictionary = updateDict.ToDictionary(x => x.Key, x => x.Value.ToSqlString());
            return this;
        }


      
        IUpdateNoTableAndValuesQueryBuilder IUpdateNoTableQueryBuilder.Values(Dictionary<string, ISqlExpression> updateDict)
        {
            _columnsAndValuesDictionary = updateDict.ToDictionary(x => x.Key, x => x.Value.ToSqlString());
            return this;
        }

        public IUpdateNoTableAndValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> updateDict)
        {
            _columnsAndValuesDictionary = updateDict.ToDictionary(x => x.Key, x =>  x.Value.ToSqlString());
            return this;
        }

        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue)
        {
            _columnsAndValuesDictionary.Add(columnName, columnValue.ToSqlString());
            return this;
        }

        IUpdateNoTableSingleValueQueryBuilder IUpdateNoTableAndTopQueryBuilder.Value(string columnName, ISqlExpression expression)
        {
            _columnsAndValuesDictionary.Add(columnName, expression.ToSqlString());
            return this;
        }

        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlVariable variable)
        {
            _columnsAndValuesDictionary.Add(columnName, variable.ToSqlString());
            return this;
        }

        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, ISqlExpression expression)
        {
            _columnsAndValuesDictionary.Add(columnName, expression.ToSqlString());
            return this;
        }
        public IUpdateNoTableSingleValueQueryBuilder Value(AbstractSqlColumn column, ISqlExpression expression)
        {
            _columnsAndValuesDictionary.Add(column.ToSqlString(), expression.ToSqlString());
            return this;
        }
        public IUpdateNoTableSingleValueQueryBuilder Value(AbstractSqlColumn column, AbstractSqlLiteral expression)
        {
            _columnsAndValuesDictionary.Add(column.ToSqlString(), expression.ToSqlString());
            return this;
        }

        public IUpdateNoTableSingleValueQueryBuilder Value(AbstractSqlColumn column, AbstractSqlVariable expression)
        {
            _columnsAndValuesDictionary.Add(column.ToSqlString(), expression.ToSqlString());
            return this;
        }


        public IUpdateNoTableSingleValueQueryBuilder Value(string columnName, AbstractSqlExpression expression)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, string>();
            _columnsAndValuesDictionary.Add(columnName, expression.ToSqlString());
            return this;
        }
         
        public IUpdateNoTopQueryBuilder Top(int? count)
        {
            _topClause = count;
            return this;
        }
       
        public IUpdateNoTableAndValuesAndWhereQueryBuilder Where(AbstractSqlCondition condition)
        {
            _whereCondition = condition.ToSqlString();
            return this;
        }

        public IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression rightExpression)
        {
            _whereCondition = columnName + C.EQUALS + rightExpression.ToSqlString();
            return this;
        }
        public IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable)
        {
            _whereCondition = columnName + C.EQUALS + variable.ToSqlString();
            return this;
        }
        public IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal)
        {
            _whereCondition = columnName + C.EQUALS + literal.ToSqlString();
            return this;
        }

        public IUpdateNoTableAndValuesAndWhereQueryBuilder WhereColumnLike(string columnName, string right)
        {
            _whereCondition = columnName + C.SPACE + C.LIKE + C.SPACE + ((AbstractSqlLiteral) right).ToSqlString();
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            Validate();

            writer.Write2(C.UPDATE);
            if (_topClause != null)
            {
                writer.Write(C.TOP);
                writer.WriteScoped(_topClause.Value.ToString());
                writer.Write(C.SPACE);
            }

            if (!string.IsNullOrWhiteSpace(_tableSchema))
            {
                writer.Write(_tableSchema);
                writer.Write(C.DOT);
            }

            writer.Write(I(_tableName));
            writer.WriteLine();
            writer.Indent++;
            writer.Write2(C.SET);
            if (_columnsAndValuesDictionary != null)
            {
                var keys = _columnsAndValuesDictionary.Keys.ToArray();
                for (var i = 0; i < _columnsAndValuesDictionary.Count; i++)
                {
                    var key = keys[i];
                    var value = _columnsAndValuesDictionary[key];
                    writer.Write(I(key));
                    writer.Write2(C.EQUALS);
                    writer.Write(value);
                    if (i != _columnsAndValuesDictionary.Count - 1)
                        writer.Write2(C.COMMA);
                }
            }

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                writer.WriteLine();
                writer.Write2(C.WHERE);
                writer.WriteScoped(_whereCondition);
            }
            writer.Indent--;
            writer.WriteLine();
        }
    }
}