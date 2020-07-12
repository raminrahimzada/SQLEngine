namespace SQLEngine.SqlServer
{
    internal class SqlServerColumnWithTableAlias : SqlServerColumn
    {
        private readonly string _tableAlias;

        public SqlServerColumnWithTableAlias(string name,string tableAlias) : base(name)
        {
            _tableAlias = tableAlias;
        }

        public override string ToSqlString()
        {
            return _tableAlias + "." + base.ToSqlString();
        }
    }
}