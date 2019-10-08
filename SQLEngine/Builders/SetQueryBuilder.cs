using SQLEngine.Helpers;

namespace SQLEngine.Builders
{
    public class SetQueryBuilder : AbstractQueryBuilder
    {
        private string _variableName;
        private string _value;
        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(_variableName))
            {
                Boom();
            }
            if (string.IsNullOrEmpty(_value))
            {
                Boom();
            }
        }
        public SetQueryBuilder Set(string variableName)
        {
            _variableName = variableName;
            return this;
        }

        public SetQueryBuilder To(string value)
        {
            _value = value;
            return this;
        }

        public override string Build()
        {
            Writer.Write(SQLKeywords.SET);
            Writer.Write2();
            Writer.Write(SQLKeywords.VARIABLE_HEADER);
            Writer.Write(_variableName);
            Writer.Write2(SQLKeywords.EQUALS);
            Writer.Write(_value);
            return base.Build();
        }
    }
}