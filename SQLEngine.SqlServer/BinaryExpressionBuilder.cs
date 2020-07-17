namespace SQLEngine.SqlServer
{
    internal class BinaryExpressionBuilder : AbstractQueryBuilder, IBinaryExpressionNopBuilder, IBinaryExpressionBuilder
    {
        public IBinaryExpressionNopBuilder Add(string left, string right)
        {
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(C._PLUS);
            Writer.Write(right);
            Writer.Write(C.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Negate(string expression)
        {
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write2(C._MINUS);
            Writer.Write(expression);
            Writer.Write(C.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Subtract(string left, string right)
        {
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(C._MINUS);
            Writer.Write(right);
            Writer.Write(C.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Divide(string left, string right)
        {
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(C._DIVIDE);
            Writer.Write(right);
            Writer.Write(C.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Multiply(string left, string right)
        {
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(C._MULTIPLY);
            Writer.Write(right);
            Writer.Write(C.END_SCOPE);
            return this;
        }

        public IBinaryExpressionNopBuilder Call(string functionName, params string[] parameters)
        {
            Writer.Write(functionName);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.WriteJoined(parameters);
            Writer.Write(C.END_SCOPE);
            return this;
        }
    }
}