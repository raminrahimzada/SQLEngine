namespace SQLEngine.SqlServer
{
    public class SqlServerVariable: ISqlVariable
    {
        public string Name { get; set; }

        public SqlServerVariable(string name)
        {
            Name = name;
        }

        public string ToSqlString()
        {
            return "@" + Name;
        }

        public override string ToString()
        {
            return ToSqlString();
        }
    }
}