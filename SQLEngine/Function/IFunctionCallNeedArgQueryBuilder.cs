namespace SQLEngine
{
    public interface IFunctionCallNeedArgQueryBuilder : IFunctionCallQueryBuilder
    {
        IFunctionCallNeedArgQueryBuilder Arg(string argument);
    }
}