namespace SQLEngine
{
    public interface ICastExpectCastQueryBuilder : ICastQueryBuilder
    {
        ICastExpectCastAndToQueryBuilder ToType(string type);
        ICastExpectCastAndToQueryBuilder ToType<T>();
    }
}