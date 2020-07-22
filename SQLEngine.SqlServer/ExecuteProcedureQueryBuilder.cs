using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class ExecuteProcedureQueryBuilder : AbstractQueryBuilder, IExecuteProcedureNeedNameQueryBuilder, IExecuteProcedureNeedArgQueryBuilder
    {
        private string _procedureName;
        private bool _withScope;

        private List<Tuple<string, string, ProcedureArgumentDirectionTypes>>
            _parametersDictionary;
        public IExecuteProcedureNeedArgQueryBuilder Name(string procedureName, bool useScoping = false)
        {
            _procedureName = procedureName;
            _withScope = useScoping;
            return this;
        }

        public IExecuteProcedureNeedArgQueryBuilder ArgOut(string parameterName, string parameterValue)
        {
            if (_parametersDictionary == null)
            {
                _parametersDictionary = new List<Tuple<string, string, ProcedureArgumentDirectionTypes>>();
            }

            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue, ProcedureArgumentDirectionTypes.OUT));
            return this;
        }
        public IExecuteProcedureNeedArgQueryBuilder Arg(string parameterName, string parameterValue)
        {
            if (_parametersDictionary == null)
            {
                _parametersDictionary = new List<Tuple<string, string, ProcedureArgumentDirectionTypes>>();
            }

            _parametersDictionary.Add(new Tuple<string, string, ProcedureArgumentDirectionTypes>(parameterName, parameterValue, ProcedureArgumentDirectionTypes.IN));
            return this;
        }


        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (_parametersDictionary == null || _parametersDictionary.Count == 0)
            {
                Bomb();
            }
        }

        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();
            writer.Write(C.EXECUTE);
            writer.Write(C.SPACE);

            writer.Write(_withScope ? I(_procedureName) : _procedureName);

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
    }
}