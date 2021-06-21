using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class FunctionQueryBuilder : AbstractQueryBuilder
    {
        class ArgumentModel
        {
            public string Name { get; set; }
            public string Type { get; set; }

            public string Build()
            {
                return Name.AsSQLVariable() + " " + Type;
            }
        }

        private readonly List<ArgumentModel> _arguments;
        private Action<IQueryBuilder> _bodyBuilder;
        private string _schemaName;
        private string _functionName;
        private string _returnType;
        private bool _schemaBuilding;

        public FunctionQueryBuilder()
        {
            _arguments = new List<ArgumentModel>();
        }

        public FunctionQueryBuilder Argument(string name, string type)
        {
            _arguments.Add(new ArgumentModel
            {
                Name = name,
                Type = type
            });
            return this;
        }
        public FunctionQueryBuilder Name(string functionName)
        {
            _functionName = functionName;
            return this;
        }
        public FunctionQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public FunctionQueryBuilder Returns(string returnType)
        {
            _returnType = returnType;
            return this;
        }
        public FunctionQueryBuilder SchemaBuilding()
        {
            _schemaBuilding = true;
            return this;
        }
        public FunctionQueryBuilder Body(Action<IQueryBuilder> builder)
        {
            _bodyBuilder = builder;
            return this;
        }
        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.CREATE);
            writer.Write2(C.FUNCTION);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                writer.Write(I(_schemaName));
                writer.Write(C.DOT);
            }
            writer.WriteLine(I(_functionName));
            writer.WriteLine(C.BEGIN_SCOPE);
            writer.Indent++;
            writer.WriteJoined(_arguments.Select(a => a.Build()));
            writer.Indent--;
            writer.WriteLine();
            writer.WriteLine(C.END_SCOPE);
            writer.Write(C.RETURNS);
            writer.Write(C.SPACE);
            if (_schemaBuilding)
            {
                writer.Write2(C.WITH);
                writer.Write2(C.SCHEMABINDING);
            }
            writer.WriteLine(_returnType);
            writer.WriteLine(C.AS);
            writer.WriteLine(C.BEGIN);
            writer.Indent++;

            using (var o = Query.New)
            {
                _bodyBuilder(o);
                writer.Indent--;
                writer.WriteLine(C.END);
            }
        }
    }
}