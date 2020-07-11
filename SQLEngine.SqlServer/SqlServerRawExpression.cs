namespace SQLEngine.SqlServer
{
    public class SqlServerRawExpression : ISqlExpression
    {
        public string Expression { get; set; }

        public SqlServerRawExpression(string expression)
        {
            Expression = expression;
        }
        public string ToSqlString()
        {
            return Expression;
        }
    }
}