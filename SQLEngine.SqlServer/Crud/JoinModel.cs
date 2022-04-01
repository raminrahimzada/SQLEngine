using System;

namespace SQLEngine.SqlServer;

internal sealed class JoinModel
{
    private string JoinTypeString()
    {
        return JoinType switch
        {
            SqlServerJoinTypes.InnerJoin => "INNER JOIN",
            SqlServerJoinTypes.LeftJoin => "LEFT JOIN",
            SqlServerJoinTypes.RightJoin => "RIGHT JOIN",
            _ => throw new Exception("Invalid JoinType : " + JoinType)
        };
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
            string.IsNullOrWhiteSpace(TableSchema) ? string.Empty : TableSchema + C.DOT,
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