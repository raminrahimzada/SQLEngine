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

        public override void Build(ISqlWriter writer)
        {

            writer.Write(C.CAST);
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(_expression);
            writer.Write2(C.AS);
            writer.Write(_type);
            writer.Write(C.END_SCOPE);
        }
    }
}