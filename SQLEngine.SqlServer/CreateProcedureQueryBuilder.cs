using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class CreateProcedureQueryBuilder : AbstractQueryBuilder
    , ICreateProcedureQueryBuilder
    ,ICreateProcedureWithArgumentQueryBuilder
    , ICreateProcedureNeedBodyQueryBuilder
    , ICreateProcedureNoNameQueryBuilder
    , ICreateProcedureNoHeaderQueryBuilder
    {
        //for sp builder

        private class ArgumentModel
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public ProcedureArgumentDirectionTypes Direction { get; set; }

            public string Build()
            {
                if (Direction == ProcedureArgumentDirectionTypes.OUT)
                    return Name.AsSQLVariable() + " " + Type + " " + C.OUTPUT;
                return Name.AsSQLVariable() + " " + Type;
            }
        }

        private readonly List<ArgumentModel> _arguments;
        //private Action<IProcedureBodyQueryBuilder> _bodyBuilder;
        private string _body;
        private string _schemaName;
        private string _procedureName;
        private string _metaDataHeader;

        public CreateProcedureQueryBuilder()
        {
            _arguments = new List<ArgumentModel>();
        }

        public ICreateProcedureWithArgumentQueryBuilder ParameterOut(string name, string type)
        {
            _arguments.Add(new ArgumentModel
            {
                Name = name,
                Type = type,
                Direction = ProcedureArgumentDirectionTypes.OUT
            });
            return this;
        }
        public ICreateProcedureWithArgumentQueryBuilder Parameter(string name, string type)
        {
            _arguments.Add(new ArgumentModel
            {
                Name = name,
                Type = type,
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

        public ICreateProcedureNeedBodyQueryBuilder Body(Action<IProcedureBodyQueryBuilder> builder)
        {
            using (var t=new SqlServerProcedureBodyQueryBuilder())
            {
                builder(t);
                _body = t.Build();
            }
            //_bodyBuilder = builder;
            return this;
        }
        public override string Build()
        {
            if (!string.IsNullOrEmpty(_metaDataHeader))
                Writer.WriteLine(_metaDataHeader);
            Writer.Write(C.CREATE);
            Writer.Write2(C.PROCEDURE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(I(_schemaName));
                Writer.Write(C.DOT);
            }
            Writer.WriteLine(I(_procedureName));
            Writer.WriteLine(C.BEGIN_SCOPE);
            Writer.Indent++;
            Writer.WriteJoined(_arguments.Select(a => a.Build()), ",", true);
            Writer.Indent--;
            Writer.WriteLine();
            Writer.WriteLine(C.END_SCOPE);
            Writer.WriteLine(C.AS);
            Writer.WriteLine(C.BEGIN);
            Writer.Indent++;
            
            Writer.WriteLine(_body);

            Writer.Indent--;
            Writer.WriteLine(C.END);
            return base.Build();
        }

        public ICreateProcedureNoHeaderQueryBuilder Header(string metaDataHeader)
        {
            _metaDataHeader = metaDataHeader;
            return this;
        }
    }
}