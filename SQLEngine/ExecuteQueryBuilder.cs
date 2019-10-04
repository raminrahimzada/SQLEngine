using System.Collections.Generic;
using System.Linq;

namespace SQLEngine
{
    public abstract class ExecuteQueryBuilder : AbstractQueryBuilder
    {
        private string _procedureName;

        private Dictionary<string, string> _parametersDictionary;
        private List<string> _parametersList;

        public ExecuteQueryBuilder Procedure(string procedureName)
        {
            _procedureName = procedureName;
            return this;
        }
        public ExecuteQueryBuilder Arg(Dictionary<string, string> paramsDictionary)
        {
            _parametersDictionary = paramsDictionary;
            return this;
        }
        public ExecuteQueryBuilder Arg(string parameterName, string parameterValue)
        {
            if (_parametersDictionary == null)
            {
                _parametersDictionary = new Dictionary<string, string>();
            }

            _parametersDictionary.Add(parameterName, parameterValue);
            return this;
        }

        public ExecuteQueryBuilder Arg0(string parameter)
        {
            return ArgI(0, parameter);
        }
        public ExecuteQueryBuilder Arg1(string parameter)
        {
            return ArgI(1, parameter);

        }
        public ExecuteQueryBuilder Arg2(string parameter)
        {
            return ArgI(2, parameter);

        }
        public ExecuteQueryBuilder Arg3(string parameter)
        {
            return ArgI(3, parameter);
        }

        private ExecuteQueryBuilder ArgI(int index, string parameterValue)
        {
            if (_parametersList == null)
            {
                _parametersList = new List<string>();
            }

            _parametersList.Insert(index, parameterValue);
            return this;
        }

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (_parametersDictionary != null)
            {
                if (_parametersList != null)
                {
                    Boom();
                }
            }
        }

        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write("EXECUTE ");
            Writer.Write(_procedureName);
            Writer.Write(" ");

            if (_parametersDictionary != null)
            {
                if (_parametersDictionary.Count > 0)
                {
                    var keys = _parametersDictionary.Keys.ToArray();
                    for (var i = 0; i < _parametersDictionary.Count; i++)
                    {
                        var key = keys[i];
                        var value = _parametersDictionary[key];

                        Writer.WriteLine("@");
                        Writer.WriteLine(key);
                        Writer.WriteLine("=");
                        Writer.WriteLine(value);
                        if (i != _parametersDictionary.Count - 1)
                        {
                            Writer.WriteLine("\r,");
                        }
                    }
                }
            }

            if (_parametersList != null)
            {
                if (_parametersList.Count > 0)
                {
                    var parameters = string.Join(" , ", _parametersList);
                    Writer.WriteLine(parameters);
                }
            }

            Writer.WriteLine(";");
            return base.Build();
        }
    }
}