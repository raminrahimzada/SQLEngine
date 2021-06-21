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
        public SqlServerRawExpression(params string[] expressions)
        {
            Expression = string.Concat(expressions);
        }

        public  override string ToSqlString()
        {
            return Expression;
        }

        protected override void SetFrom(AbstractSqlLiteral literal)
        {
            Expression = literal == null ? C.NULL : literal.ToSqlString();
        }
        protected override void SetFrom(AbstractSqlVariable variable)
        {
            Expression = variable.ToSqlString();
        }
    }
}