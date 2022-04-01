using System.Collections.Generic;

namespace SQLEngine.SqlServer;

internal sealed class ExecuteFunctionQueryBuilder :
    AbstractQueryBuilder,
    IExecuteFunctionNeedNameAndSchemaQueryBuilder
{
    private string _functionName;

    private readonly List<string>
        _parametersList = new();

    private string _schemaName;

    public override void Build(ISqlWriter writer)
    {
        if(!string.IsNullOrWhiteSpace(_schemaName))
        {
            writer.Write(_schemaName);
            writer.Write(C.DOT);
        }
        writer.Write(_functionName);
        writer.Write(C.SPACE);
        writer.Write(C.BEGIN_SCOPE);
        for(int i = 0; i < _parametersList.Count; i++)
        {
            if(i != 0)
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