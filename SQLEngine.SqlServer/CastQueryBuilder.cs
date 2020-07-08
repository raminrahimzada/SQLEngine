namespace SQLEngine.SqlServer
{
    internal sealed class CastQueryBuilder : SqlServerAbstractQueryBuilder, ICastExpectCastQueryBuilder, ICastExpectCastAndToQueryBuilder
    {
        private string _expression;
        private string _type;

        public ICastExpectCastQueryBuilder Cast(string expression)
        {
            _expression = expression;
            return this;
        }

        public ICastExpectCastAndToQueryBuilder ToType(string type)
        {
            _type = type;
            return this;
        }

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(_expression))
            {
                Bomb();
            }
            if (string.IsNullOrEmpty(_type))
            {
                Bomb();
            }
        }

        public override string Build()
        {

            Writer.Write(SQLKeywords.CAST);
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(_expression);
            Writer.Write2(SQLKeywords.AS);
            Writer.Write(_type);
            Writer.Write(SQLKeywords.END_SCOPE);

            return base.Build();
        }
    }
}