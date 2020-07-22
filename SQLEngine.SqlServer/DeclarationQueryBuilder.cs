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
        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.DECLARE);
            writer.Write2();
            writer.Write(C.VARIABLE_HEADER);
            writer.Write(_variableName);
            writer.Write2(_type);
            if (!string.IsNullOrEmpty(_defaultValue))
            {
                writer.Write2(C.EQUALS);
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(_defaultValue);
                writer.Write(C.END_SCOPE);
            }

            writer.Write(C.SEMICOLON);
        }
    }
}