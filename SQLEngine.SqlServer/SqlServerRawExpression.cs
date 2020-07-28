namespace SQLEngine.SqlServer
{
    internal class SqlServerRawExpression : AbstractSqlExpression
    {
        internal static void Setup()
        {
            CreateEmpty = () => new SqlServerRawExpression();
        }
        public string Expression { get; set; }

        public SqlServerRawExpression()
        {
            
        }

        public SqlServerRawExpression(string expression)
        {
            Expression = expression;
        }

        public  override string ToSqlString()
        {
            return Expression;
        }

        protected override void SetFrom(AbstractSqlLiteral literal)
        {
            Expression = literal.ToSqlString();
        }
        protected override void SetFrom(AbstractSqlVariable variable)
        {
            Expression = variable.ToSqlString();
        }
    }
}