namespace SQLEngine
{
    
    public interface ISelectQueryBuilder : ISelectWithSelectorQueryBuilder
    {
        ISelectNoTopQueryBuilder Top(int count);
        ISelectQueryBuilder Distinct();
    }
}