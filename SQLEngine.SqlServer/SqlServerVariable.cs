namespace SQLEngine.SqlServer
{
    internal class SqlServerVariable: AbstractSqlVariable
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

        protected override AbstractSqlCondition Greater(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " > " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " >= " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " < " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " <= " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        public override ISqlExpression Multiply(AbstractSqlVariable variable)
        {
            var expression = "(" + ToSqlString() + " * " + variable.ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        public override ISqlExpression Multiply(AbstractSqlLiteral variable)
        {
            var expression = "(" + ToSqlString() + " * " + variable.ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        public override ISqlExpression Add(AbstractSqlLiteral literal)
        {
            var expression = "(" + ToSqlString() + " + " + literal.ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        public override ISqlExpression Divide(AbstractSqlVariable variable)
        {
            var expression = "(" + ToSqlString() + " / " + variable.ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        public override ISqlExpression Subtract(AbstractSqlLiteral literal)
        {
            var expression = "(" + ToSqlString() + " - " + literal.ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        protected override ISqlExpression SubtractReverse(AbstractSqlLiteral literal)
        {
            var expression = "(" + literal.ToSqlString() + " - " + ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        protected override ISqlExpression DivideReverse(AbstractSqlLiteral literal)
        {
            var expression = "(" + literal.ToSqlString() + " / " + ToSqlString() + ")";
            return SqlServerCondition.Raw(expression);
        }

        public override string ToString()
        {
            return ToSqlString();
        }
    }
}