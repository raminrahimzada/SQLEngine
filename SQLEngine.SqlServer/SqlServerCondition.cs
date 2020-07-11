namespace SQLEngine.SqlServer
{
    public class SqlServerCondition : AbstractSqlCondition
    {
        private string _rawSqlString;

        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        public override AbstractSqlCondition And(AbstractSqlCondition condition)
        {
            var result = "(" + this.ToSqlString() + ") AND (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public override AbstractSqlCondition Or(AbstractSqlCondition condition)
        {
            var result = "(" + this.ToSqlString() + ") OR (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public static SqlServerCondition Raw(string rawSqlString)
        {
            return new SqlServerCondition
            {
                _rawSqlString = rawSqlString
            };
        }
    }
}