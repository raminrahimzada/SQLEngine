using System;
using System.Collections.Generic;

namespace SQLEngine.PostgreSql
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
        private FunctionBodyQueryBuilder _body;
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

        public ICreateFunctionNoNameQueryBuilder Parameter<T>(string paramName)
        {
            return Parameter(paramName, Query.Settings.TypeConvertor.ToSqlType<T>());
        }

        public ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns(string returnType)
        {
            _returnType = returnType;
            return this;
        }

        public ICreateFunctionNoNameAndParametersAndReturnTypeQueryBuilder Returns<T>()
        {
            return Returns(Query.Settings.TypeConvertor.ToSqlType<T>());
        }



        public IAbstractCreateFunctionQueryBuilder Body(Action<IFunctionBodyQueryBuilder> body)
        {
            var builder = new FunctionBodyQueryBuilder();
            {
                body(builder);
                _body = builder;
                return this;
            }
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.CREATE);
            writer.Write(C.SPACE);
            writer.Write(C.FUNCTION);
            writer.Write(C.SPACE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                writer.Write(_schemaName);
                writer.Write(C.DOT);
            }
            writer.Write(_name);
            writer.WriteLine();
            writer.WriteLine(C.BEGIN_SCOPE);
            Indent++;
            writer.WriteLineJoined(_parameters);
            Indent--;
            writer.WriteLine(C.END_SCOPE);
            writer.Write(C.RETURNS);
            writer.Write(C.SPACE);
            writer.Write(_returnType);
            writer.WriteLine();
            writer.Write(C.BEGIN);
            writer.WriteLine();
            Indent++;
            _body.Build(writer);
            Indent--;
            writer.Write(C.END);
        }
    }
}