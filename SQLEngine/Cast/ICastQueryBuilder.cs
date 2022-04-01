namespace SQLEngine;

public interface ICastQueryBuilder : IAbstractQueryBuilder
{
    ICastExpectCastQueryBuilder Cast(ISqlExpression expression);
    ICastExpectCastQueryBuilder Cast(AbstractSqlVariable variable);
    ICastExpectCastQueryBuilder Cast(AbstractSqlLiteral literal);
}