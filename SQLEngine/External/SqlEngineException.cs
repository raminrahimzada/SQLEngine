using System;

namespace SQLEngine
{
    [Serializable]
    public class SqlEngineException : Exception
    {
        public SqlEngineException(string message) : base(message)
        {
        }
    }
}