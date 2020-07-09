using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class FunctionQueryBuilder : SqlServerQueryBuilder
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
            _arguments.Add(new ArgumentModel()
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
        public override string Build()
        {
            Writer.Write(SQLKeywords.CREATE);
            Writer.Write2(SQLKeywords.FUNCTION);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(I(_schemaName));
                Writer.Write(SQLKeywords.DOT);
            }
            Writer.WriteLine(I(_functionName));
            Writer.WriteLine(SQLKeywords.BEGIN_SCOPE);
            Writer.Indent++;
            Writer.WriteJoined(_arguments.Select(a => a.Build()));
            Writer.Indent--;
            Writer.WriteLine();
            Writer.WriteLine(SQLKeywords.END_SCOPE);
            Writer.Write(SQLKeywords.RETURNS);
            Writer.Write(SQLKeywords.SPACE);
            if (_schemaBuilding)
            {
                Writer.Write2(SQLKeywords.WITH);
                Writer.Write2(SQLKeywords.SCHEMABINDING);
            }
            Writer.WriteLine(_returnType);
            Writer.WriteLine(SQLKeywords.AS);
            Writer.WriteLine(SQLKeywords.BEGIN);
            Writer.Indent++;

            using (var o = Query.New)
            {
                o.Join(this);
                _bodyBuilder(o);
                Writer.Indent--;
                Writer.WriteLine(SQLKeywords.END);
                return base.Build();
            }
        }
    }
}