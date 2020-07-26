namespace SQLEngine.SqlServer
{
    public class SqlServerCondition : AbstractSqlCondition
    {
        private readonly string _rawSqlString;

        public SqlServerCondition(string rawSqlString)
        {
            _rawSqlString = rawSqlString;
        }

        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        public override AbstractSqlCondition And(AbstractSqlCondition condition)
        {
            var result = "(" + ToSqlString() + ") AND (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public override AbstractSqlCondition Or(AbstractSqlCondition condition)
        {
            var result = "(" + ToSqlString() + ") OR (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public static SqlServerCondition Raw(string rawSqlString)
        {
            return new SqlServerCondition(rawSqlString);
        }
    }
}