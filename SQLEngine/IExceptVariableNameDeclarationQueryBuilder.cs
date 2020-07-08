namespace SQLEngine
{
    public interface IExceptVariableNameDeclarationQueryBuilder : IAbstractQueryBuilder
    {
        IExceptVariableTypeNameDeclarationQueryBuilder OfType(string type);
    }
}