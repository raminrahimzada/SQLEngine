namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlCondition : AbstractSqlCondition
    {
        public static void Setup()
        {
            CreateEmpty = () => new PostgreSqlCondition();
        }

        private string _rawSqlString;

        public PostgreSqlCondition(string rawSqlString)
        {
            _rawSqlString = rawSqlString;
        }
        public PostgreSqlCondition(params string[] rawSqlStringParts)
        {
            _rawSqlString = string.Concat(rawSqlStringParts);
        }

        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        protected override void SetRaw(bool rawValue)
        {
            _rawSqlString = rawValue ? C.TRUE : C.FALSE;
        }

        protected override void SetRaw(bool? rawValue)
        {
            if (rawValue == null)
            {
                _rawSqlString = C.NULL;
            }
            else
            {
                SetRaw(rawValue.Value);
            }
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

        public static PostgreSqlCondition Raw(string rawSqlString)
        {
            return new PostgreSqlCondition(rawSqlString);
        }

        
    }
}