namespace SQLEngine
{
    public interface IExceptVariableTypeNameDeclarationQueryBuilder : IAbstractQueryBuilder
    {
        IExceptDefaultValueNameDeclarationQueryBuilder Default(string defaultValue);
    }
}