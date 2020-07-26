namespace SQLEngine.SqlServer
{
    internal class SqlServerColumnWithAsExpression : SqlServerColumn
    {
        private readonly string _asName;
        public SqlServerColumnWithAsExpression(string name,string asName) : base(name)
        {
            _asName = asName;
        }

        public override string ToSqlString()
        {
            return base.ToSqlString() + C.AS + _asName;
        }
    }
}