namespace SQLEngine
{
    public interface IExceptVariableTypeNameDeclarationQueryBuilder : IAbstractQueryBuilder
    {
        IExceptDefaultValueNameDeclarationQueryBuilder Default(AbstractSqlLiteral literal);
        IExceptDefaultValueNameDeclarationQueryBuilder Default(ISqlExpression expression);
    }
}