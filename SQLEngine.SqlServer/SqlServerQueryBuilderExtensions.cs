namespace SQLEngine.SqlServer
{
    public static class SqlServerQueryBuilderExtensions
    {
        public static void SetNoCountOn(this SqlServerQueryBuilder builder)
        {
            builder.AddExpression("SET NOCOUNT ON;");
        }
    }
}