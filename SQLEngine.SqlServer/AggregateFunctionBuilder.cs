namespace SQLEngine.SqlServer
{
    internal class AggregateFunctionBuilder :AbstractQueryBuilder, IAggregateFunctionBuilder
    {
        private ISqlExpression _expression;
        private bool? _isDistinct;
        private string _functionName;

        public IAggregateFunctionBuilder Min(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.MIN;
            return this;
        }

        public IAggregateFunctionBuilder Max(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.MAX;
            return this;
        }

        public IAggregateFunctionBuilder Count(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.COUNT;
            return this;
        }

        public IAggregateFunctionBuilder Sum(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.SUM;
            return this;
        }

        public IAggregateFunctionBuilder Avg(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.AVG;
            return this;
        }

        public IAggregateFunctionBuilder Distinct()
        {
            _isDistinct = true;
            return this;
        }

        public IAggregateFunctionBuilder All()
        {
            _isDistinct = false;
            return this;
        }

        public override string Build()
        {
            Clear();
            Writer.Write(_functionName);
            Writer.Write(C.BEGIN_SCOPE);
            if (_isDistinct.HasValue)
            {
                Writer.Write(_isDistinct.Value ? C.DISTINCT : C.ALL);
                Writer.Write(C.SPACE);
            }
            Writer.Write(_expression.ToSqlString());
            Writer.Write(C.END_SCOPE);
            return base.Build();
        }
    }
}