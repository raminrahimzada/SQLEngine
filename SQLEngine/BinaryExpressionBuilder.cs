namespace SQLEngine
{
    public class BinaryExpressionBuilder : AbstractQueryBuilder
    {
        public BinaryExpressionBuilder Equal(string left, string right)
        {
            Writer.Write(left);
            Writer.Write("=");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder NotEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write("<>");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Less(string left, string right)
        {
            Writer.Write(left);
            Writer.Write(" < ");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Greater(string left, string right)
        {
            Writer.Write(left);
            Writer.Write(" > ");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder LessEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write(" <= ");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder GreaterEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write(" >= ");
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Between(string expression, string starting, string ending)
        {
            Writer.Write(expression);
            Writer.Write(" BETWEEN ");
            Writer.Write(starting);
            Writer.Write(" AND ");
            Writer.Write(ending);
            return this;
        }
        public BinaryExpressionBuilder IsNull(string expression)
        {
            Writer.Write(expression);
            Writer.Write(" IS NULL ");
            return this;
        }
        public BinaryExpressionBuilder IsNotNull(string expression)
        {
            Writer.Write(expression);
            Writer.Write(" IS NOT NULL ");
            return this;
        }
        public BinaryExpressionBuilder In(string expression, params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                Writer.Write(False);
                return this;
            }
            Writer.Write("[");
            Writer.Write(expression);
            Writer.Write("] IN (");
            Writer.Write(string.Join(",", values));
            Writer.Write(")");
            return this;
        }

        public BinaryExpressionBuilder NotIn(string expression, params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                Writer.Write(True);
                return this;
            }

            Writer.Write("[");
            Writer.Write(expression);
            Writer.Write("] NOT IN (");
            Writer.Write(string.Join(",", values));
            Writer.Write(")");
            return this;
        }
    }
}