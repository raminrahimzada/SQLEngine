namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlColumnWithAsExpression : PostgreSqlColumn
    {
        private readonly string _asName;
        public PostgreSqlColumnWithAsExpression(string name,string asName) : base(name)
        {
            _asName = asName;
        }

        public override string ToSqlString()
        {
            return base.ToSqlString() + C.AS + _asName;
        }
    }
}