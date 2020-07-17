using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class ExecuteProcedureQueryBuilder : ExecuteQueryBuilder, IExecuteProcedureNeedNameQueryBuilder, IExecuteProcedureNeedArgQueryBuilder
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

        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write(C.EXECUTE);
            Writer.Write(C.SPACE);

            Writer.Write(_withScope ? I(_procedureName) : _procedureName);

            Writer.Write2();

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

                        Writer.Write(C.VARIABLE_HEADER);
                        Writer.Write(key);
                        Writer.Write(C.EQUALS);
                        Writer.Write(value);
                        if (direction == ProcedureArgumentDirectionTypes.OUT)
                        {
                            Writer.Write2(C.OUTPUT);
                        }
                        if (i != _parametersDictionary.Count - 1)
                        {
                            Writer.WriteNewLine();
                            Writer.Write(C.COMMA);
                        }
                        i++;
                    }
                }
            }

            Writer.WriteLine(C.SEMICOLON);
            return base.Build();
        }
    }
}