using System;

namespace SQLEngine.SqlServer;

internal sealed class JoinModel
{
    private string JoinTypeString()
    {
        switch (JoinType)
        {
            case SqlServerJoinTypes.InnerJoin:
                return "INNER JOIN";
            case SqlServerJoinTypes.LeftJoin:
                return "LEFT JOIN";
            case SqlServerJoinTypes.RightJoin:
                return "RIGHT JOIN";
            default:
                throw new ArgumentOutOfRangeException(nameof(JoinType), JoinType, null);
        }
    }
    public string TableName { get; set; }
    public string TableAlias { get; set; }
    public string TableSchema { get; set; }
    public SqlServerJoinTypes JoinType { get; set; }
    public AbstractSqlCondition Condition { get; set; }

    public string JoinQuery()
    {
        return string.Concat(
            C.SPACE,
            JoinTypeString(),
            C.SPACE,
            string.IsNullOrWhiteSpace(TableSchema)?string.Empty: TableSchema+C.DOT,
            TableName,
            C.SPACE,
            C.AS,
            C.SPACE,
            TableAlias,
            C.SPACE,
            C.ON,
            C.SPACE,
            Condition.ToSqlString()
        );
    }
}