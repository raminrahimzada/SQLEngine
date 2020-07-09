using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class CreateFunctionQueryBuilder : SqlServerQueryBuilder, 
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


        public IAbstractCreateFunctionQueryBuilder Body(Action<IQueryBuilder> body)
        {
            using (var builder= Query.New)
            {
                body(builder);
                this._body = builder.Build();
                return this;
            }
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.CREATE);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(SQLKeywords.FUNCTION);
            Writer.Write(SQLKeywords.SPACE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(_schemaName);
                Writer.Write(SQLKeywords.DOT);
            }
            Writer.Write(_name);
            Writer.WriteLine();
            Writer.WriteLine(SQLKeywords.BEGIN_SCOPE);
            Indent++;
            Writer.WriteLineJoined(_parameters);
            Indent--;
            Writer.WriteLine(SQLKeywords.END_SCOPE);
            Writer.Write(SQLKeywords.RETURNS);
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(_returnType);
            Writer.WriteLine();
            Writer.Write(SQLKeywords.BEGIN);
            Writer.WriteLine();
            Indent++;
            Writer.WriteEx(_body);
            Indent--;
            Writer.Write(SQLKeywords.END);
            
            return base.Build();
        }
    }
}