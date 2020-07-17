using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class CreateFunctionQueryBuilder : AbstractQueryBuilder, 
        IAbstractCreateFunctionQueryBuilder, 
        ICreateFunctionQueryBuilder,
        ICreateFunctionNoNameQueryBuilder,
        ICreateFunctionNoNameAndParametersQueryBuilder,
        ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder
    {
        private string _name;
        private string _schemaName;
        private string _returnType;
        private string _body;
        private readonly List<string> _parameters;

        public CreateFunctionQueryBuilder()
        {
            _parameters=new List<string>();
        }
        public ICreateFunctionNoNameQueryBuilder Name(string funcName)
        {
            _name = funcName;
            return this;
        }

        public ICreateFunctionNoNameQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public ICreateFunctionNoNameQueryBuilder Parameter(string paramName, string paramType)
        {
            _parameters.Add("@" + paramName + " " + paramType);
            return this;
        }

        public ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns(string returnType)
        {
            _returnType = returnType;
            return this;
        }


        public IAbstractCreateFunctionQueryBuilder Body(Action<IFunctionBodyQueryBuilder> body)
        {
            using (var builder= new FunctionBodyQueryBuilder())
            {
                body(builder);
                _body = builder.Build();
                return this;
            }
        }

        public override string Build()
        {
            Writer.Write(C.CREATE);
            Writer.Write(C.SPACE);
            Writer.Write(C.FUNCTION);
            Writer.Write(C.SPACE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(_schemaName);
                Writer.Write(C.DOT);
            }
            Writer.Write(_name);
            Writer.WriteLine();
            Writer.WriteLine(C.BEGIN_SCOPE);
            Indent++;
            Writer.WriteLineJoined(_parameters);
            Indent--;
            Writer.WriteLine(C.END_SCOPE);
            Writer.Write(C.RETURNS);
            Writer.Write(C.SPACE);
            Writer.Write(_returnType);
            Writer.WriteLine();
            Writer.Write(C.BEGIN);
            Writer.WriteLine();
            Indent++;
            Writer.WriteEx(_body);
            Indent--;
            Writer.Write(C.END);
            
            return base.Build();
        }
    }
}