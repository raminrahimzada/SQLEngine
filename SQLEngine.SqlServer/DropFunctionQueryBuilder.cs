namespace SQLEngine.SqlServer
{
    internal class DropFunctionQueryBuilder : AbstractQueryBuilder,
        IDropFunctionNoSchemaQueryBuilder,
        IDropFunctionQueryBuilder
    {
        private string _functionName;
        private string _schemaName;

        public IDropFunctionQueryBuilder FunctionName(string funcName)
        {
            _functionName = funcName;
            return this;
        }
        public IDropFunctionNoSchemaQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }
        public override string Build()
        {
            Writer.Write(SQLKeywords.DROP);
            Writer.Write2(SQLKeywords.FUNCTION);
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                Writer.Write(_schemaName);
                Writer.Write(SQLKeywords.DOT);
            }
            Writer.Write(I(_functionName));
            Writer.Write(SQLKeywords.SEMICOLON);
            return base.Build();
        }
    }
}