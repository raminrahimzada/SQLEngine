namespace SQLEngine
{
    public interface ICreateFunctionQueryBuilder : IAbstractQueryBuilder
    {
        ICreateFunctionNoNameQueryBuilder Name(string funcName);
    }
}