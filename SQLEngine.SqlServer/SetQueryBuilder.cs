namespace SQLEngine.SqlServer
{
    internal class SetQueryBuilder : SqlServerAbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder, ISetNoSetNoToQueryBuilder
    {
        private string _variable;
        private string _value;
        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(_variable))
            {
                Bomb();
            }
            if (string.IsNullOrEmpty(_value))
            {
                Bomb();
            }
        }
        public ISetNeedToQueryBuilder Set(string variableName)
        {
            _variable = variableName;
            return this;
        }

        public ISetNoSetNoToQueryBuilder To(string value)
        {
            _value = value;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.SET);
            Writer.Write2();
            Writer.Write(_variable);
            Writer.Write2(SQLKeywords.EQUALS);
            Writer.Write(_value);
            Writer.Write(SQLKeywords.SEMICOLON);
            return base.Build();
        }
    }
}