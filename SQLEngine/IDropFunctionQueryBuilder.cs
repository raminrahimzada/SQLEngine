namespace SQLEngine
{
    public interface IDropFunctionQueryBuilder : IAbstractQueryBuilder
    {
        //IDropFunctionQueryBuilder FunctionName(string funcName);
        IDropFunctionNoSchemaQueryBuilder FromSchema(string schemaName);
    }
}