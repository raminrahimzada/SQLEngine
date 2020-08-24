namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlColumnWithTableAliasAndAsExpression : PostgreSqlColumnWithTableAlias
    {
        private readonly string _asName;

        public override string ToSqlString()
        {
            return base.ToSqlString() + C.AS + _asName;
        }

        public PostgreSqlColumnWithTableAliasAndAsExpression(string name, string tableAlias,string asName) : base(name, tableAlias)
        {
            _asName = asName;
        }
    }
}