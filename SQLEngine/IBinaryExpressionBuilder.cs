namespace SQLEngine
{
    public interface IBinaryExpressionBuilder : IAbstractQueryBuilder
    {
        IBinaryExpressionNopBuilder Add(string left, string right);
        IBinaryExpressionNopBuilder Negate(string expression);
        IBinaryExpressionNopBuilder Subtract(string left, string right);
        IBinaryExpressionNopBuilder Divide(string left, string right);
        IBinaryExpressionNopBuilder Multiply(string left, string right);
        IBinaryExpressionNopBuilder Call(string functionName, params string[] parameters);
    }
}