using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer;

internal class CreateProcedureQueryBuilder : AbstractQueryBuilder
    , ICreateProcedureQueryBuilder
    , ICreateProcedureWithArgumentQueryBuilder
    , ICreateProcedureNeedBodyQueryBuilder
    , ICreateProcedureNoNameQueryBuilder
    , ICreateProcedureNoHeaderQueryBuilder
{
    private sealed class ArgumentModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ProcedureArgumentDirectionTypes Direction { get; set; }

        public string Build()
        {
            if(Direction == ProcedureArgumentDirectionTypes.OUT)
            {
                return $"{Name.AsSQLVariable()} {Type} {C.OUTPUT}";
            }

            return $"{Name.AsSQLVariable()} {Type}";
        }
    }

    private readonly List<ArgumentModel> _arguments;
    private string _body;
    private string _schemaName;
    private string _procedureName;
    private string _metaDataHeader;

    public CreateProcedureQueryBuilder()
    {
        _arguments = new List<ArgumentModel>();
    }

    public ICreateProcedureWithArgumentQueryBuilder Parameter<T>(string argName)
    {
        return Parameter(argName, Query.Settings.TypeConvertor.ToSqlType<T>());
    }

    public ICreateProcedureWithArgumentQueryBuilder ParameterOut(string argName, string argType)
    {
        _arguments.Add(new ArgumentModel
        {
            Name = argName,
            Type = argType,
            Direction = ProcedureArgumentDirectionTypes.OUT
        });
        return this;
    }

    public ICreateProcedureWithArgumentQueryBuilder ParameterOut<T>(string argName)
    {
        return ParameterOut(argName, Query.Settings.TypeConvertor.ToSqlType<T>());
    }

    public ICreateProcedureWithArgumentQueryBuilder Parameter(string argName, string argType)
    {
        _arguments.Add(new ArgumentModel
        {
            Name = argName,
            Type = argType,
            Direction = ProcedureArgumentDirectionTypes.IN
        });
        return this;
    }
    public ICreateProcedureNoNameQueryBuilder Name(string procedureName)
    {
        _procedureName = procedureName;
        return this;
    }
    public ICreateProcedureQueryBuilder Schema(string schemaName)
    {
        _schemaName = schemaName;
        return this;
    }

    public ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> body)
    {
        using(var t = new SqlServerProcedureBodyQueryBuilder())
        {
            body(t);
            _body = t.Build();
        }
        return this;
    }
    public override void Build(ISqlWriter writer)
    {
        if(!string.IsNullOrEmpty(_metaDataHeader))
        {
            writer.WriteLine(_metaDataHeader);
        }

        writer.Write(C.CREATE);
        writer.Write2(C.PROCEDURE);
        if(!string.IsNullOrEmpty(_schemaName))
        {
            writer.Write(I(_schemaName));
            writer.Write(C.DOT);
        }
        writer.WriteLine(I(_procedureName));
        writer.WriteLine(C.BEGIN_SCOPE);
        writer.Indent++;
        writer.WriteJoined(_arguments.Select(a => a.Build()), ",", true);
        writer.Indent--;
        writer.WriteLine();
        writer.WriteLine(C.END_SCOPE);
        writer.WriteLine(C.AS);
        writer.WriteLine(C.BEGIN);
        writer.Indent++;

        writer.WriteLine(_body);

        writer.Indent--;
        writer.WriteLine(C.END);
    }

    public ICreateProcedureNoHeaderQueryBuilder Header(string procedureHeaderMetaData)
    {
        _metaDataHeader = procedureHeaderMetaData;
        return this;
    }
}