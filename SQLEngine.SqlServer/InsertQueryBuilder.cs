﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class InsertQueryBuilder : AbstractQueryBuilder, 
        IInsertNoIntoWithColumns, IInsertNoValuesQueryBuilder, 
        IInsertNeedValueQueryBuilder,
        IInsertQueryBuilder, 
        IInsertNoIntoQueryBuilder
    {
        private string _tableName;
        private Dictionary<string, ISqlExpression> _columnsAndValuesDictionary;
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
        public IInsertNeedValueQueryBuilder Value(string columnName, AbstractSqlLiteral columnValue)
        {
            if (_columnsAndValuesDictionary == null) _columnsAndValuesDictionary = new Dictionary<string, ISqlExpression>();
            _columnsAndValuesDictionary.Add(columnName, columnValue);
            return this;
        }

        public IInsertNoValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues)
        {
            _columnsAndValuesDictionary = colsAndValues;
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
        
        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();

            writer.Write(C.INSERT);
            writer.Write2(C.INTO);
            writer.Write(I(_tableName));
            writer.Write2(C.SPACE);

            var columnNamesSafe = _columnNames?.Select(I).ToArray();
            if (_columnsAndValuesDictionary != null)
            {
                writer.WriteLine();
                writer.Indent++;
                writer.BeginScope();
                writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(I).ToArray());
                writer.EndScope();
                writer.WriteLine();
                writer.Indent--;

                writer.Write(C.VALUES);
                writer.WriteLine();
                writer.Indent++;

                writer.BeginScope();

                writer.WriteJoined(_columnsAndValuesDictionary.Keys.Select(key => _columnsAndValuesDictionary[key].ToSqlString()).ToArray());
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
                writer.WriteJoined(_valuesList);
                writer.EndScope();
                writer.Indent--;
            }
        }
        
    }
}