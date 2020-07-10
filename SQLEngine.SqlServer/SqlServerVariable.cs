namespace SQLEngine.SqlServer
{
    public class SqlServerRawExpression : ISqlExpression
    {
        public string Expression { get; set; }

        public SqlServerRawExpression(string expression)
        {
            Expression = expression;
        }
        public string ToSqlString()
        {
            return Expression;
        }
    }
    public class SqlServerVariable: AbstractSqlVariable
    {
        public SqlServerVariable(string name)
        {
            Name = name;
        }

        public override string ToSqlString()
        {
            return "@" + Name;
        }

        public override ISqlExpression Add(AbstractSqlVariable y)
        {
            var sql = "(" + ToSqlString() + " + " + y.ToSqlString() + ")";
            return new SqlServerRawExpression(sql);
        }

        public override ISqlExpression Subtract(AbstractSqlVariable y)
        {
            var sql = "(" + ToSqlString() + " - " + y.ToSqlString() + ")";
            return new SqlServerRawExpression(sql);
        }

        public override string ToString()
        {
            return ToSqlString();
        }
    }
}