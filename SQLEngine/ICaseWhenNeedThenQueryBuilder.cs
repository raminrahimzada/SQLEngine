namespace SQLEngine
{
    public interface ICaseWhenNeedThenQueryBuilder : ICaseWhenQueryBuilder
    {
        ICaseWhenNeedWhenQueryBuilder Then(string @then);
        ICaseWhenNeedWhenQueryBuilder ThenColumn(string tableAlias, string columnName);

    }
}