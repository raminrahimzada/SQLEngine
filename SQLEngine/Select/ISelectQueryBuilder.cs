namespace SQLEngine;

public interface ISelectQueryBuilder : ISelectWithSelectorQueryBuilder
{
    ISelectQueryBuilder Distinct();
    ISelectNoTopQueryBuilder Top(int count);
}