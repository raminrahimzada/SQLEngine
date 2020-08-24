namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlColumnWithTableAlias : PostgreSqlColumn
    {
        private readonly string _tableAlias;

        public PostgreSqlColumnWithTableAlias(string name,string tableAlias) : base(name)
        {
            _tableAlias = tableAlias;
        }

        public override string ToSqlString()
        {
            return _tableAlias + "." + base.ToSqlString();
        }
    }
}