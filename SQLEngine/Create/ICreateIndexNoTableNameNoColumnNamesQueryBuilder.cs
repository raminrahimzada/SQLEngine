namespace SQLEngine;

public interface ICreateIndexNoTableNameNoColumnNamesQueryBuilder : IAbstractQueryBuilder
{
    ICreateIndexNoTableNameNoColumnNamesNoUniqueQueryBuilder Unique(bool isUnique = true);
}