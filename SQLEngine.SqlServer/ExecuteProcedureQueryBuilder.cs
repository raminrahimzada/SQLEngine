using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    
    internal class ExecuteFunctionQueryBuilder : 
        AbstractQueryBuilder, 
        IExecuteFunctionNeedNameQueryBuilder,
        IExecuteFunctionNeedNameAndSchemaQueryBuilder
    {
        private string _functionName;

        private readonly List<string>
            _parametersList = new List<string>();

        private string _schemaName;

        public override void Build(ISqlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                writer.Write(_schemaName);
                writer.Write(C.DOT);
            }
            writer.Write(_functionName);
            writer.Write(C.SPACE);
            writer.Write(C.BEGIN_SCOPE);
            for (int i = 0; i < _parametersList.Count; i++)
            {
                if (i != 0)
                {
                    writer.Write(C.COMMA);
                }
                writer.Write(_parametersList[i]);
            }
            writer.Write(C.END_SCOPE);
        }

        public IExecuteFunctionNeedNameQueryBuilder Name(string functionName)
        {
            _functionName = functionName;
            return this;
        }

        public IExecuteFunctionNeedNameQueryBuilder Arg(ISqlExpression parameterValue)
        {
            _parametersList.Add(parameterValue.ToSqlString());
            return this;
        }

        public IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlLiteral parameterValue)
        {
            _parametersList.Add(parameterValue.ToSqlString());
            return this;
        }

        public IExecuteFunctionNeedNameQueryBuilder Arg(AbstractSqlVariable parameterValue)
        {
            _parametersList.Add(parameterValue.ToSqlString());
            return this;
        }

        public IExecuteFunctionNeedNameAndSchemaQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }
    }
    internal class ExecuteProcedureQueryBuilder : AbstractQueryBuilder, IExecuteProcedureNeedNameQueryBuilder, IExecuteProcedureNeedArgQueryBuilder
    {
        private string _procedureName;

        private readonly List<Tuple<string, string, ProcedureArgumentDirectionTypes>>
            _parametersDictionary = new List<Tuple<string, string, ProcedureArgumentDirectionTypes>>();

        public IExecuteProcedureNeedArgQueryBuilder Name(string procedureName)
        {
            _procedureName = procedureName;
            return this;
        }

        public IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, string parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue, ProcedureArgumentDirectionTypes.OUT));
            return this;
        }
        public IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, string parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue, ProcedureArgumentDirectionTypes.IN));
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();
            writer.Write(C.EXECUTE);
            writer.Write(C.SPACE);

            writer.Write(_procedureName);

            writer.Write2();

            if (_parametersDictionary != null)
            {
                if (_parametersDictionary.Count > 0)
                {
                    var i = 0;
                    foreach (var p in _parametersDictionary)
                    {
                        var key = p.Item1;
                        var value = p.Item2;
                        var direction = p.Item3;

                        writer.Write(C.VARIABLE_HEADER);
                        writer.Write(key);
                        writer.Write(C.EQUALS);
                        writer.Write(value);
                        if (direction == ProcedureArgumentDirectionTypes.OUT)
                        {
                            writer.Write2(C.OUTPUT);
                        }
                        if (i != _parametersDictionary.Count - 1)
                        {
                            writer.WriteNewLine();
                            writer.Write(C.COMMA);
                        }
                        i++;
                    }
                }
            }

            writer.WriteLine(C.SEMICOLON);
        }

        public IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, AbstractSqlVariable parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue.ToSqlString(), ProcedureArgumentDirectionTypes.IN));
            return this;
        }

        public IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, AbstractSqlVariable parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue.ToSqlString(), ProcedureArgumentDirectionTypes.OUT));
            return this;
        }

        public IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, AbstractSqlLiteral parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue.ToSqlString(), ProcedureArgumentDirectionTypes.IN));
            return this;
        }

        public IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, AbstractSqlLiteral parameterValue)
        {
            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue.ToSqlString(), ProcedureArgumentDirectionTypes.OUT));
            return this;
        }
    }
}