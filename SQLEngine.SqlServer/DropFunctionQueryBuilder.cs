namespace SQLEngine.SqlServer
{
    internal class DropFunctionQueryBuilder : SqlServerQueryBuilder, IDropFunctionQueryBuilder
    {
        private string _functionName;
        public IDropFunctionQueryBuilder FunctionName(string funcName)
        {
            _functionName = funcName;
            return this;
        }
        public override string Build()
        {
            Writer.Write(SQLKeywords.DROP);
            Writer.Write2(SQLKeywords.FUNCTION);
            Writer.Write(I(_functionName));
            Writer.Write(SQLKeywords.SEMICOLON);
            return base.Build();
        }
    }
}