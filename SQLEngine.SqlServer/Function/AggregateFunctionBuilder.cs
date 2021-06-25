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
        public IAggregateFunctionBuilder Min(AbstractSqlColumn column)
        {
            _expression = column;
            _functionName = C.MIN;
            return this;
        }
        public IAggregateFunctionBuilder Min(string columnName)
        {
            return Min(new SqlServerColumn(columnName));
        }

        public IAggregateFunctionBuilder Max(string columnName)
        {
            return Max(new SqlServerColumn(columnName));
        }

        public IAggregateFunctionBuilder Max(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.MAX;
            return this;
        }

        public IAggregateFunctionBuilder Max(AbstractSqlColumn column)
        {
            _expression = column;
            _functionName = C.MAX;
            return this;
        }
        public IAggregateFunctionBuilder Count(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.COUNT;
            return this;
        }
        public IAggregateFunctionBuilder Count(AbstractSqlColumn column)
        {
            _expression = column;
            _functionName = C.COUNT;
            return this;
        }
        public IAggregateFunctionBuilder Count(AbstractSqlLiteral literal)
        {
            _expression = literal;
            _functionName = C.COUNT;
            return this;
        }
        public IAggregateFunctionBuilder Count(string columnName)
        {
            return Count(new SqlServerColumn(columnName));
        }
        public IAggregateFunctionBuilder Sum(string columnName)
        {
            return Sum(new SqlServerColumn(columnName));
        }
        public IAggregateFunctionBuilder Sum(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.SUM;
            return this;
        }
        public IAggregateFunctionBuilder Sum(AbstractSqlColumn column)
        {
            _expression = column;
            _functionName = C.SUM;
            return this;
        }
        public IAggregateFunctionBuilder Sum(AbstractSqlLiteral literal)
        {
            _expression = literal;
            _functionName = C.SUM;
            return this;
        }

        public IAggregateFunctionBuilder Avg(ISqlExpression expression)
        {
            _expression = expression;
            _functionName = C.AVG;
            return this;
        }
        public IAggregateFunctionBuilder Avg(string columnName)
        {
            return Avg(new SqlServerColumn(columnName));
        }
        public IAggregateFunctionBuilder Avg(AbstractSqlColumn column)
        {
            _expression = column;
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

        public override void Build(ISqlWriter writer)
        {
            writer.Write(_functionName);
            writer.Write(C.BEGIN_SCOPE);
            if (_isDistinct.HasValue)
            {
                writer.Write(_isDistinct.Value ? C.DISTINCT : C.ALL);
                writer.Write(C.SPACE);
            }
            writer.Write(_expression.ToSqlString());
            writer.Write(C.END_SCOPE);
        }

    }
}