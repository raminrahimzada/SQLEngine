namespace SQLEngine.SqlServer
{
    internal sealed class DeclarationQueryBuilder : AbstractQueryBuilder,
        IExceptVariableNameDeclarationQueryBuilder,
        IExceptVariableTypeNameDeclarationQueryBuilder,
        IExceptDefaultValueNameDeclarationQueryBuilder,
        IDeclarationQueryBuilder
    {
        private string _variableName;
        private string _type;
        private string _defaultValue;

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(_variableName))
            {
                Bomb();
            }
            if (string.IsNullOrEmpty(_type))
            {
                Bomb();
            }
        }

        public IExceptVariableNameDeclarationQueryBuilder Declare(string variableName)
        {
            _variableName = variableName;
            return this;
        }

        IExceptVariableTypeNameDeclarationQueryBuilder IExceptVariableNameDeclarationQueryBuilder.OfType(string type)
        {
            _type = type;
            return this;
        }
        IExceptDefaultValueNameDeclarationQueryBuilder IExceptVariableTypeNameDeclarationQueryBuilder.Default(string defaultValue)
        {
            _defaultValue = defaultValue;
            return this;
        }
        public override string Build()
        {
            Clear();
            Writer.Write(C.DECLARE);
            Writer.Write2();
            Writer.Write(C.VARIABLE_HEADER);
            Writer.Write(_variableName);
            Writer.Write2(_type);
            if (!string.IsNullOrEmpty(_defaultValue))
            {
                Writer.Write2(C.EQUALS);
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(_defaultValue);
                Writer.Write(C.END_SCOPE);
            }

            Writer.Write(C.SEMICOLON);
            return base.Build();
        }
    }
}