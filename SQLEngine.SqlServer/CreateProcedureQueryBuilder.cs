using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class CreateProcedureQueryBuilder : SqlServerAbstractQueryBuilder
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
                    return Name.AsSQLVariable() + " " + Type + " " + SQLKeywords.OUTPUT;
                return Name.AsSQLVariable() + " " + Type;
            }
        }

        private readonly List<ArgumentModel> _arguments;
        private Action<IQueryBuilder> _bodyBuilder;
        private string _schemaName;
        private string _procedureName;
        private string _metaDataHeader;

        public CreateProcedureQueryBuilder()
        {
            _arguments = new List<ArgumentModel>();
        }

        public ICreateProcedureWithArgumentQueryBuilder ArgumentOut(string name, string type)
        {
            _arguments.Add(new ArgumentModel
            {
                Name = name,
                Type = type,
                Direction = ProcedureArgumentDirectionTypes.OUT
            });
            return this;
        }
        public ICreateProcedureWithArgumentQueryBuilder Argument(string name, string type)
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

        public ICreateProcedureNeedBodyQueryBuilder Body(Action<IQueryBuilder> builder)
        {
            _bodyBuilder = builder;
            return this;
        }
        public override string Build()
        {
            if (!string.IsNullOrEmpty(_metaDataHeader))
                Writer.WriteLine(_metaDataHeader);
            Writer.Write(SQLKeywords.CREATE);
            Writer.Write2(SQLKeywords.PROCEDURE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(I(_schemaName));
                Writer.Write(SQLKeywords.DOT);
            }
            Writer.WriteLine(I(_procedureName));
            Writer.WriteLine(SQLKeywords.BEGIN_SCOPE);
            Writer.Indent++;
            Writer.WriteJoined(_arguments.Select(a => a.Build()), ",", true);
            Writer.Indent--;
            Writer.WriteLine();
            Writer.WriteLine(SQLKeywords.END_SCOPE);
            Writer.WriteLine(SQLKeywords.AS);
            Writer.WriteLine(SQLKeywords.BEGIN);
            Writer.Indent++;

            using (var o = QueryBuilderFactory.New)
            {
                o.Join(this);
                _bodyBuilder(o);
                Writer.Indent--;
                Writer.WriteLine(SQLKeywords.END);
                return base.Build();
            }
        }

        public ICreateProcedureNoHeaderQueryBuilder Header(string metaDataHeader)
        {
            _metaDataHeader = metaDataHeader;
            return this;
        }
    }
}