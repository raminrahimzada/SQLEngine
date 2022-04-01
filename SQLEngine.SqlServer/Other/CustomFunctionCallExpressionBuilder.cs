using System;

namespace SQLEngine.SqlServer;

internal class CustomFunctionCallExpressionBuilder :AbstractQueryBuilder, ICustomFunctionCallNopBuilder, ICustomFunctionCallExpressionBuilder
{
    private Action<ISqlWriter> _internalBuilder;

    public ICustomFunctionCallNopBuilder Call(string functionName, params ISqlExpression[] parameters)
    {
        _internalBuilder = writer =>
        {
            writer.Write(functionName);
            writer.Write(C.BEGIN_SCOPE);
            for (var index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];
                if (index != 0)
                {
                    writer.Write(C.COMMA);
                }

                writer.Write(parameter.ToSqlString());
            }

            writer.Write(C.END_SCOPE);
        };
        return this;
    }
 
    public override void Build(ISqlWriter writer)
    {
        _internalBuilder(writer);
    }

    public IAbstractQueryBuilder Assign(AbstractSqlVariable left, ISqlExpression right)
    {
        _internalBuilder = writer =>
        {
            writer.Write(left.ToSqlString());
            writer.Write(C.EQUALS);
            writer.Write(right.ToSqlString());
        };
        return this;
    }
}