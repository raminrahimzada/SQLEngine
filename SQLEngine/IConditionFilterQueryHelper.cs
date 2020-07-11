namespace SQLEngine
{
    public interface IConditionFilterQueryHelper
    {
        string Cast(ISqlExpression expression, string asType);
        string IsNull(ISqlExpression key);
        string IsNotNull(ISqlExpression key);
        string ColumnEquals(string columnName, ISqlExpression value);
        string Equal(ISqlExpression key, ISqlExpression value);
        string NotEqual(ISqlExpression key, ISqlExpression value);
        string GreaterThan(ISqlExpression key, ISqlExpression value);
        string ColumnGreaterThan(string columnName, ISqlExpression value);
        string ColumnLessThan(string columnName, ISqlExpression value);
        string GreaterEqualThan(ISqlExpression key, ISqlExpression value);
        string LessThan(ISqlExpression key, ISqlExpression value);
        //string Equal(string key, ISqlString value, string alias);
        string BetWeen(ISqlExpression expression, ISqlExpression starting, ISqlExpression ending);
        string And(params string[] filters);
        string Or(params string[] filters);
        string As(string tableName, string columnName, string alias);
        string Top(int count, string selection = "*");
        string True { get; }
        string False { get; }
        string In(string columnName, params string[] values);
        string NotIn(string columnName, params string[] values);
        string Exists(string selection);
        string NotExists(string selection);
        string LessThanOrEqual(string left, string right);
        string NotLike(string expression, string regex, string escape = "");
        string Like(string expression, string regex, string escape = "");
        string Call(string functionName, params string[] parameters);
    }
}