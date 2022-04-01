namespace SQLEngine;

public interface IUniqueVariableNameGenerator
{
    string New();
    void Reset();
}