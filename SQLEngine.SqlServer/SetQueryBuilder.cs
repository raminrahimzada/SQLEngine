namespace SQLEngine.SqlServer
{
    internal class SetQueryBuilder : AbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder, 
        ISetNoSetNoToQueryBuilder
    {
        private AbstractSqlVariable _variable;
        private ISqlExpression _value;
        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //TODO
        }
        public ISetNeedToQueryBuilder Set(AbstractSqlVariable variable)
        {
            _variable = variable;
            return this;
        }

        public ISetNoSetNoToQueryBuilder To(ISqlExpression value)
        {
            _value = value;
            return this;
        }

        public override string Build()
        {
            Writer.Write(C.SET);
            Writer.Write2();
            Writer.Write(_variable.ToSqlString());
            Writer.Write(C.SPACE);
            Writer.Write2(C.EQUALS);
            Writer.Write(_value.ToSqlString());
            Writer.Write(C.SEMICOLON);
            return base.Build();
        }
    }
}