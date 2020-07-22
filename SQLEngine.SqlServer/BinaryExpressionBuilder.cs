using System;

namespace SQLEngine.SqlServer
{
    internal class BinaryExpressionBuilder : IBinaryExpressionNopBuilder, IBinaryExpressionBuilder
    {
        private Action<ISqlWriter> _internalBuilder;

        public IBinaryExpressionNopBuilder Add(string left, string right)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(left);
                Writer.Write2(C._PLUS);
                Writer.Write(right);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }
        public IBinaryExpressionNopBuilder Negate(string expression)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write2(C._MINUS);
                Writer.Write(expression);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }
        public IBinaryExpressionNopBuilder Subtract(string left, string right)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(left);
                Writer.Write2(C._MINUS);
                Writer.Write(right);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }
        public IBinaryExpressionNopBuilder Divide(string left, string right)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(left);
                Writer.Write2(C._DIVIDE);
                Writer.Write(right);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }
        public IBinaryExpressionNopBuilder Multiply(string left, string right)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(left);
                Writer.Write2(C._MULTIPLY);
                Writer.Write(right);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }

        public IBinaryExpressionNopBuilder Call(string functionName, params string[] parameters)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(functionName);
                Writer.Write(C.BEGIN_SCOPE);
                Writer.WriteJoined(parameters);
                Writer.Write(C.END_SCOPE);
            };
            return this;
        }

        public void Dispose()
        {

        }

        public void Build(ISqlWriter writer)
        {
            _internalBuilder(writer);
        }

        public IAbstractQueryBuilder Assign(ISqlExpression left, ISqlExpression right)
        {
            _internalBuilder = Writer =>
            {
                Writer.Write(left.ToSqlString());
                Writer.Write(C.EQUALS);
                Writer.Write(right.ToSqlString());
            };
            return this;
        }
    }
}