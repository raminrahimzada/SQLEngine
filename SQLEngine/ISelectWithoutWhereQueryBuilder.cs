namespace SQLEngine
{
    public interface ISelectWithoutWhereQueryBuilder : IAbstractSelectQueryBuilder
    {
        ISelectOrderBuilder OrderBy(string orderFieldName);
        ISelectOrderBuilder OrderByDesc(string orderFieldName);
    }
}