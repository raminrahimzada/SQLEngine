using System.Text;

namespace SQLEngine.SqlServer
{
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

        public override AbstractSqlCondition In(params ISqlExpression[] expressions)
        {
            var sb = new StringBuilder();
            sb.Append(ToSqlString());
            sb.Append(C.SPACE);
            sb.Append(C.IN);
            sb.Append(C.BEGIN_SCOPE);
            for (var i = 0; i < expressions.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(C.COMMA);
                }
                sb.Append(expressions[i].ToSqlString());
            }
            sb.Append(C.END_SCOPE);
            return new SqlServerCondition(sb.ToString());
        }

        public override AbstractSqlCondition In(params AbstractSqlLiteral[] expressions)
        {
            var sb = new StringBuilder();
            sb.Append(ToSqlString());
            sb.Append(C.SPACE);
            sb.Append(C.IN);
            sb.Append(C.BEGIN_SCOPE);
            for (var i = 0; i < expressions.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(C.COMMA);
                }
                sb.Append(expressions[i].ToSqlString());
            }
            sb.Append(C.END_SCOPE);
            return new SqlServerCondition(sb.ToString());
        }

        public override AbstractSqlCondition NotIn(params ISqlExpression[] expressions)
        {
            var sb = new StringBuilder();
            sb.Append(ToSqlString());
            sb.Append(C.SPACE);
            sb.Append(C.NOT);
            sb.Append(C.SPACE);
            sb.Append(C.IN);
            sb.Append(C.BEGIN_SCOPE);
            for (var i = 0; i < expressions.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(C.COMMA);
                }
                sb.Append(expressions[i].ToSqlString());
            }
            sb.Append(C.END_SCOPE);
            return new SqlServerCondition(sb.ToString());
        }

        public override AbstractSqlCondition NotIn(params AbstractSqlLiteral[] expressions)
        {
            var sb = new StringBuilder();
            sb.Append(ToSqlString());
            sb.Append(C.SPACE);
            sb.Append(C.NOT);
            sb.Append(C.SPACE);
            sb.Append(C.IN);
            sb.Append(C.BEGIN_SCOPE);
            for (var i = 0; i < expressions.Length; i++)
            {
                if (i != 0)
                {
                    sb.Append(C.COMMA);
                }
                sb.Append(expressions[i].ToSqlString());
            }
            sb.Append(C.END_SCOPE);
            return new SqlServerCondition(sb.ToString());
        }

        public override AbstractSqlCondition IsNull()
        {
            var sql = "(" + ToSqlString() + " IS NULL)";
            return new SqlServerCondition(sql);
        }

        public override AbstractSqlCondition IsNotNull()
        {
            var sql = "(" + ToSqlString() + " IS NOT NULL)";
            return new SqlServerCondition(sql);
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

        protected override AbstractSqlCondition Greater(ISqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " > " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(ISqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " >= " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(ISqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " < " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(ISqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " <= " + abstractSqlVariable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " > " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " >= " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " < " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " <= " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " > " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " >= " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " < " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " <= " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " = " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " = " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " <> " + column.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " <> " + literal.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlVariable variable)
        {
            var expression = ToSqlString() + " = " + variable.ToSqlString();
            return SqlServerCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlVariable variable)
        {
            var expression = ToSqlString() + " <> " + variable.ToSqlString();
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