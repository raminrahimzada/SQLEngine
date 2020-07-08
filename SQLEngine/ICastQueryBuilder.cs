namespace SQLEngine
{
    public interface ICastQueryBuilder : IAbstractQueryBuilder
    {
        ICastExpectCastQueryBuilder Cast(string expression);
    }
}