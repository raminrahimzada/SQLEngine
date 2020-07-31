using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class InsertQueryBuilder : AbstractQueryBuilder, 
        IInsertNoIntoWithColumns, 
        IInsertNoValuesQueryBuilder, 
        IInsertNeedValueQueryBuilder,
        IInsertQueryBuilder, 
        IInsertNoIntoQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, ISqlExpression> _columnsAndValuesDictionary=new Dictionary<string, ISqlExpression>();
        private ISqlExpression[] _valuesList;
        private string[] _columnNames;
        private string _selection;

        public IInsertNoIntoQueryBuilder Into(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IInsertNoIntoQueryBuilder Into<TTable>() where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return Into(table.Name);
            }
        }

        public IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue)
        {
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }
        public IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlVariable variable)
        {
            _columnsAndValuesDictionary.Add(columnName, variable);
            return this;
        }

        public IInsertNoValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues)
        {
            _columnsAndValuesDictionary = colsAndValues;
            return this;
        }
 

        public IInsertNoValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> colsAndValuesAsLiterals)
        {
            _columnsAndValuesDictionary =
                colsAndValuesAsLiterals.ToDictionary(x => x.Key, x => (ISqlExpression) x.Value);
            return this;
        }
        

        public IInsertNoValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder)
        {
            using (var writer=CreateNewWriter())
            using (var select=new SelectQueryBuilder())
            {
                builder(select);
                select.Build(writer);
                _selection = writer.Build();
            }

            return this;
        }


         
        public IInsertNoIntoWithColumns Columns(params string[] columnNames)
        {
            _columnNames = columnNames;
            return this;
        }
        
        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();

            writer.Write(C.INSERT);
            writer.Write2(C.INTO);
            writer.Write(I(_tableName));
            writer.Write2(C.SPACE);

            var columnNamesSafe = _columnNames?.Select(I).ToArray();
            if (_columnNames != null&& _columnNames.Length>0)
            {
                writer.WriteLine();
                writer.Indent++;
                writer.BeginScope();
                writer.WriteJoined(_columnNames.ToArray());
                writer.EndScope();
                writer.WriteLine();
                writer.Indent--;

                writer.Write(C.VALUES);
                writer.WriteLine();
                writer.Indent++;

                writer.BeginScope();

                writer.WriteJoined(_valuesList.Select(x => x.ToSqlString()).ToArray());
                writer.EndScope();
                writer.Indent--;
            }
            else if (!string.IsNullOrEmpty(_selection))//selection mode
            {
                if (_columnNames != null)
                {
                    writer.BeginScope();
                    writer.WriteJoined(columnNamesSafe);
                    writer.EndScope();
                }
                writer.Write(C.SPACE);
                writer.Write(_selection);
            }
            else //normal model 
            {
                if (_columnNames != null)
                {
                    writer.WriteLine();
                    writer.Indent++;
                    writer.BeginScope();
                    writer.WriteJoined(columnNamesSafe);
                    writer.EndScope();
                    writer.Indent--;

                }
                writer.WriteLine();

                writer.Write(C.VALUES);
                writer.WriteLine();
                writer.Indent++;

                writer.BeginScope();
                for (int i = 0; i < _valuesList.Length; i++)
                {
                    if (i != 0)
                    {
                        writer.Write(C.COMMA);
                    }

                    writer.Write(_valuesList[i].ToSqlString());
                }
                writer.EndScope();
                writer.Indent--;
            }
        }


        public IInsertNoValuesQueryBuilder Values(params ISqlExpression[] values)
        {
            _valuesList = values;
            return this;
        }

        public IInsertNoValuesQueryBuilder Values(params AbstractSqlLiteral[] values)
        {
            _valuesList = values.Select(x => (ISqlExpression) x).ToArray();
            return this;
        }
    }
}