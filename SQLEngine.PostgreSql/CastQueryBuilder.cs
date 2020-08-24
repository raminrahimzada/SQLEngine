namespace SQLEngine.PostgreSql
{
    internal sealed class CastQueryBuilder : AbstractQueryBuilder, ICastExpectCastQueryBuilder, ICastExpectCastAndToQueryBuilder
    {
        private ISqlExpression _expression;
        private string _type;

        public ICastExpectCastAndToQueryBuilder ToType(string type)
        {
            _type = type;
            return this;
        }

        public ICastExpectCastAndToQueryBuilder ToType<T>()
        {
            return ToType(Query.Settings.TypeConvertor.ToSqlType<T>());
        }

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (_expression==null)
            {
                Bomb();
            }
            if (string.IsNullOrEmpty(_type))
            {
                Bomb();
            }
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.CAST);
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(_expression.ToSqlString());
            writer.Write2(C.AS);
            writer.Write(_type);
            writer.Write(C.END_SCOPE);
        }

        public ICastExpectCastQueryBuilder Cast(ISqlExpression expression)
        {
            _expression = expression;
            return this;
        }

        public ICastExpectCastQueryBuilder Cast(AbstractSqlVariable variable)
        {
            _expression = variable;
            return this;
        }

        public ICastExpectCastQueryBuilder Cast(AbstractSqlLiteral literal)
        {
            _expression = literal;
            return this;
        }
    }
}