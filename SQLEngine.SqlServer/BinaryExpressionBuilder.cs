namespace SQLEngine.SqlServer
{
    internal class BinaryExpressionBuilder : SqlServerAbstractQueryBuilder, IBinaryExpressionNopBuilder, IBinaryExpressionBuilder
    {
        public IBinaryExpressionNopBuilder Add(string left, string right)
        {
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(SQLKeywords._PLUS);
            Writer.Write(right);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Negate(string expression)
        {
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write2(SQLKeywords._MINUS);
            Writer.Write(expression);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Subtract(string left, string right)
        {
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(SQLKeywords._MINUS);
            Writer.Write(right);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Divide(string left, string right)
        {
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(SQLKeywords._DIVIDE);
            Writer.Write(right);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }
        public IBinaryExpressionNopBuilder Multiply(string left, string right)
        {
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(left);
            Writer.Write2(SQLKeywords._MULTIPLY);
            Writer.Write(right);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }

        public IBinaryExpressionNopBuilder Call(string functionName, params string[] parameters)
        {
            Writer.Write(functionName);
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.WriteJoined(parameters);
            Writer.Write(SQLKeywords.END_SCOPE);
            return this;
        }
    }
}