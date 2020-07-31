using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class ExecuteProcedureQueryBuilder : AbstractQueryBuilder, IExecuteProcedureNeedNameQueryBuilder, IExecuteProcedureNeedArgQueryBuilder
    {
        private string _procedureName;
        private string _schemaName;

        private readonly List<Tuple<string, string, ProcedureArgumentDirectionTypes>>
            _parametersDictionary = new List<Tuple<string, string, ProcedureArgumentDirectionTypes>>();

        public IExecuteProcedureNeedArgQueryBuilder Name(string procedureName)
        {
            _procedureName = procedureName;
            return this;
        }
        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();
            writer.Write(C.EXECUTE);
            writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                writer.Write(_schemaName);
                writer.Write(C.DOT);
            }
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

        public IExecuteProcedureNeedArgQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
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