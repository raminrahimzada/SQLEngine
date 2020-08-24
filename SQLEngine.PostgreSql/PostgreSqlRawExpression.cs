namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlRawExpression : AbstractSqlExpression
    {
        internal static void Setup()
        {
            CreateEmpty = () => new PostgreSqlRawExpression();
        }
        public string Expression { get; set; }

        public PostgreSqlRawExpression()
        {
            
        }

        public PostgreSqlRawExpression(string expression)
        {
            Expression = expression;
        }
        public PostgreSqlRawExpression(params string[] expressions)
        {
            Expression = string.Concat(expressions);
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