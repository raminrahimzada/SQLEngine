using System.Text;

namespace SQLEngine.PostgreSql
{
    public class PostgreSqlVariable: AbstractSqlVariable
    {
        public PostgreSqlVariable(string name)
        {
            Name = name;
        }

        public override string ToSqlString()
        {
            return Name;
        }

        public override AbstractSqlExpression Add(AbstractSqlVariable y)
        {
            var sql = "(" + ToSqlString() + " + " + y.ToSqlString() + ")";
            return new PostgreSqlRawExpression(sql);
        }

        public override AbstractSqlExpression Subtract(AbstractSqlVariable y)
        {
            var sql = "(" + ToSqlString() + " - " + y.ToSqlString() + ")";
            return new PostgreSqlRawExpression(sql);
        }

        public override AbstractSqlCondition In(params AbstractSqlExpression[] expressions)
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
            return new PostgreSqlCondition(sb.ToString());
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
            return new PostgreSqlCondition(sb.ToString());
        }

        public override AbstractSqlCondition NotIn(params AbstractSqlExpression[] expressions)
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
            return new PostgreSqlCondition(sb.ToString());
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
            return new PostgreSqlCondition(sb.ToString());
        }

        public override AbstractSqlCondition IsNull()
        {
            var sql = "(" + ToSqlString() + " IS NULL)";
            return new PostgreSqlCondition(sql);
        }

        public override AbstractSqlCondition IsNotNull()
        {
            var sql = "(" + ToSqlString() + " IS NOT NULL)";
            return new PostgreSqlCondition(sql);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " > " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " >= " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " < " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlVariable abstractSqlVariable)
        {
            var expression = ToSqlString() + " <= " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " > " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " >= " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " < " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlExpression abstractSqlVariable)
        {
            var expression = ToSqlString() + " <= " + abstractSqlVariable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " > " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " >= " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " < " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " <= " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " > " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " >= " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition Less(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " < " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " <= " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " = " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " = " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlColumn column)
        {
            var expression = ToSqlString() + " <> " + column.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlLiteral literal)
        {
            var expression = ToSqlString() + " <> " + literal.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition EqualsTo(AbstractSqlVariable variable)
        {
            var expression = ToSqlString() + " = " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        protected override AbstractSqlCondition NotEqualsTo(AbstractSqlVariable variable)
        {
            var expression = ToSqlString() + " <> " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(expression);
        }

        public override AbstractSqlExpression Multiply(AbstractSqlVariable variable)
        {
            var expression = "(" + ToSqlString() + " * " + variable.ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        public override AbstractSqlExpression Multiply(AbstractSqlLiteral variable)
        {
            var expression = "(" + ToSqlString() + " * " + variable.ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        public override AbstractSqlExpression Add(AbstractSqlLiteral literal)
        {
            var expression = "(" + ToSqlString() + " + " + literal.ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        public override AbstractSqlExpression Divide(AbstractSqlVariable variable)
        {
            var expression = "(" + ToSqlString() + " / " + variable.ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        public override AbstractSqlExpression Subtract(AbstractSqlLiteral literal)
        {
            var expression = "(" + ToSqlString() + " - " + literal.ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        protected override AbstractSqlExpression SubtractReverse(AbstractSqlLiteral literal)
        {
            var expression = "(" + literal.ToSqlString() + " - " + ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        protected override AbstractSqlExpression DivideReverse(AbstractSqlLiteral literal)
        {
            var expression = "(" + literal.ToSqlString() + " / " + ToSqlString() + ")";
            return new PostgreSqlRawExpression(expression);
        }

        public override string ToString()
        {
            return ToSqlString();
        }
    }
}