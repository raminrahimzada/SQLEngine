namespace SQLEngine.SqlServer;

internal sealed class DropFunctionQueryBuilder : AbstractQueryBuilder,
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
    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.DROP);
        writer.Write2(C.FUNCTION);
        if(!string.IsNullOrWhiteSpace(_schemaName))
        {
            writer.Write(_schemaName);
            writer.Write(C.DOT);
        }
        writer.Write(I(_functionName));
        writer.Write(C.SEMICOLON);
    }
}