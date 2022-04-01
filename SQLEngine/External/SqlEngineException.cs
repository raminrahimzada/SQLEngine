using System;
using System.Runtime.Serialization;

namespace SQLEngine;

[Serializable]
public class SqlEngineException : Exception, ISerializable
{
    public SqlEngineException(string message) : base(message)
    {
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}