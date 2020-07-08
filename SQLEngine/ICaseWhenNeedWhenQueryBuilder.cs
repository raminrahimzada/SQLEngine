namespace SQLEngine
{
    public interface ICaseWhenNeedWhenQueryBuilder : ICaseWhenQueryBuilder
    {
        ICaseWhenNeedThenQueryBuilder When(string @when);
        ICaseWhenNeedThenQueryBuilder WhenEquals(string columnName, string expression);
    }
}