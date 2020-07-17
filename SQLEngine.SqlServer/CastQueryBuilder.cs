namespace SQLEngine.SqlServer
{
    internal sealed class CastQueryBuilder : AbstractQueryBuilder, ICastExpectCastQueryBuilder, ICastExpectCastAndToQueryBuilder
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

            Writer.Write(C.CAST);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(_expression);
            Writer.Write2(C.AS);
            Writer.Write(_type);
            Writer.Write(C.END_SCOPE);

            return base.Build();
        }
    }
}