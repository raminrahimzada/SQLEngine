﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer;

internal sealed class InsertQueryBuilder : AbstractQueryBuilder,
    IInsertNoIntoWithColumns,
    IInsertHasValuesQueryBuilder,
    IInsertNeedValueQueryBuilder,
    IInsertQueryBuilder,
    IInsertNoIntoQueryBuilder
{
    private string _tableName;
    private string _schemaName;
    private Dictionary<string, ISqlExpression> _columnsAndValuesDictionary = new();

    private readonly List<ISqlExpression[]> _valuesList = new();
    private string[] _columnNames;
    private string _selection;

    public IInsertNoIntoQueryBuilder Into(string tableName, string schemaName = null)
    {
        _tableName = tableName;
        _schemaName = schemaName;
        return this;
    }

    public IInsertNoIntoQueryBuilder Into<TTable>() where TTable : ITable, new()
    {
        using var table = new TTable();
        return Into(table.Name, table.Schema);
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

    public IInsertHasValuesQueryBuilder Values(Dictionary<string, ISqlExpression> colsAndValues)
    {
        _columnsAndValuesDictionary = colsAndValues;
        return this;
    }


    public IInsertHasValuesQueryBuilder Values(Dictionary<string, AbstractSqlLiteral> colsAndValuesAsLiterals)
    {
        _columnsAndValuesDictionary =
            colsAndValuesAsLiterals.ToDictionary(x => x.Key, x => (ISqlExpression)x.Value);
        return this;
    }


    public IInsertHasValuesQueryBuilder Values(Action<ISelectQueryBuilder> builder)
    {
        using(var writer = CreateNewWriter())
        using(var select = new SelectQueryBuilder())
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
        if(!string.IsNullOrWhiteSpace(_schemaName))
        {
            writer.Write(_schemaName);
            writer.Write(C.DOT);
        }
        writer.Write(I(_tableName));
        writer.Write(C.SPACE);

        var columnNamesSafe = _columnNames?.Select(I).ToArray();
        var isColumnsAndValues1 = _columnNames is { Length: > 0 } && _valuesList.Count > 0 && _valuesList[0].Length > 0;

        var isColumnsAndValues2 = _columnsAndValuesDictionary is { Count: > 0 };
        if(isColumnsAndValues1 || isColumnsAndValues2)
        {
            ColumnMode(writer);
        }
        else if(!string.IsNullOrEmpty(_selection))//selection mode
        {
            if(_columnNames != null)
            {
                writer.BeginScope();
                writer.WriteJoined(columnNamesSafe);
                writer.EndScope();
            }
            writer.Write(C.SPACE);
            writer.Write(_selection);
        }
        else if(_valuesList is { Count: > 0 }) //normal model 
        {
            NormalMode(writer, columnNamesSafe);
        }
        else
        {
            writer.Write(C.DEFAULT);
            writer.Write(C.SPACE);
            writer.Write(C.VALUES);
        }
        writer.WriteLine();
    }

    private void NormalMode(ISqlWriter writer, string[] columnNamesSafe)
    {
        if(_columnNames != null)
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

        bool first = true;
        foreach(var values in _valuesList)
        {
            if(first)
            {
                first = false;
            }
            else
            {
                writer.Write(C.COMMA);
            }

            writer.BeginScope();
            for(int i = 0; i < values.Length; i++)
            {
                if(i != 0)
                {
                    writer.Write(C.COMMA);
                }

                writer.Write(values[i].ToSqlString());
            }
            writer.EndScope();
        }
        writer.Indent--;
    }

    private void ColumnMode(ISqlWriter writer)
    {
        var columnNames = _columnNames;
        if(columnNames == null)
        {
            if(_columnsAndValuesDictionary == null)
            {
                throw Bomb();
            }

            columnNames = _columnsAndValuesDictionary.Keys.ToArray();
        }
        if(_valuesList.Count == 0)
        {
            if(_columnsAndValuesDictionary == null)
            {
                throw Bomb();
            }

            var values = _columnsAndValuesDictionary.Values.ToArray();
            _valuesList.Add(values);
        }
        writer.WriteLine();
        writer.Indent++;
        writer.BeginScope();
        writer.WriteJoined(columnNames.ToArray());
        writer.EndScope();
        writer.WriteLine();
        writer.Indent--;

        writer.Write(C.VALUES);
        writer.WriteLine();
        writer.Indent++;

        bool first = true;
        foreach(var values in _valuesList)
        {
            if(first)
            {
                first = false;
            }
            else
            {
                writer.Write(C.COMMA);
            }
            writer.BeginScope();
            writer.WriteJoined(values.Select(x => x.ToSqlString()).ToArray());
            writer.EndScope();
        }

        writer.Indent--;

    }


    public IInsertHasValuesQueryBuilder Values(params ISqlExpression[] values)
    {
        _valuesList.Add(values);
        return this;
    }

    public IInsertHasValuesQueryBuilder Values(params AbstractSqlLiteral[] values)
    {
        var arr = values.Select(x => (ISqlExpression)x).ToArray();
        _valuesList.Add(arr);
        return this;
    }
}