using SQLEngine.Helpers;

namespace SQLEngine.Builders
{
    public class DeclarationQueryBuilder : AbstractQueryBuilder
    {
        private string _variableName;
        private string _type;
        private string _defaultValue;

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(_variableName))
            {
                Boom();
            }
            if (string.IsNullOrEmpty(_type))
            {
                Boom();
            }
        }

        public DeclarationQueryBuilder Declare(string variableName)
        {
            _variableName = variableName;
            return this;
        }
        public DeclarationQueryBuilder OfType(string type)
        {
            _type = type;
            return this;
        }
        public DeclarationQueryBuilder Default(string defaultValue)
        {
            _defaultValue = defaultValue;
            return this;
        }
        public override string Build()
        {
            Writer.Write(SQLKeywords.DECLARE);
            Writer.Write2();
            Writer.Write(SQLKeywords.VARIABLE_HEADER);
            Writer.Write(_variableName);
            Writer.Write2(_type);
            if (!string.IsNullOrEmpty(_defaultValue))
            {
                Writer.Write2(SQLKeywords.EQUALS);
                Writer.Write(_defaultValue);
            }

            Writer.Write(SQLKeywords.SEMICOLON);
            return base.Build();
        }
    }
}