namespace SQLEngine.SqlServer
{
    internal class SetQueryBuilder : AbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder, 
        ISetNoSetNoToQueryBuilder
    {
        private AbstractSqlVariable _variable;
        private ISqlExpression _value;
       
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

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.SET);
            writer.Write2();
            writer.Write(_variable.ToSqlString());
            writer.Write(C.SPACE);
            writer.Write2(C.EQUALS);
            writer.Write(_value.ToSqlString());
            writer.Write(C.SEMICOLON);
        }
    }
}