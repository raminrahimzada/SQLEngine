namespace SQLEngine.SqlServer
{
    internal class SqlServerRawExpression : AbstractSqlExpression,ISqlExpression
    {
        public string Expression { get; set; }

        public SqlServerRawExpression(string expression)
        {
            Expression = expression;
        }
        public override string ToSqlString()
        {
            return Expression;
        }
    }
}