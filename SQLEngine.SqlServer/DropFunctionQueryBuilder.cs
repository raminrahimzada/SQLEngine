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
        public IDropFunctionNoSchemaQueryBuilder FromSchema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }
        public override string Build()
        {
            Writer.Write(C.DROP);
            Writer.Write2(C.FUNCTION);
            if (!string.IsNullOrWhiteSpace(_schemaName))
            {
                Writer.Write(_schemaName);
                Writer.Write(C.DOT);
            }
            Writer.Write(I(_functionName));
            Writer.Write(C.SEMICOLON);
            return base.Build();
        }
    }
}