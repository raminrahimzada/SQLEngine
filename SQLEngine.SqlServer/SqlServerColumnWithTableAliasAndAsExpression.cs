namespace SQLEngine.SqlServer
{
    internal class SqlServerColumnWithTableAliasAndAsExpression : SqlServerColumnWithTableAlias
    {
        private readonly string _asName;

        public override string ToSqlString()
        {
            return base.ToSqlString() + C.AS + _asName;
        }

        public SqlServerColumnWithTableAliasAndAsExpression(string name, string tableAlias,string asName) : base(name, tableAlias)
        {
            _asName = asName;
        }
    }
}