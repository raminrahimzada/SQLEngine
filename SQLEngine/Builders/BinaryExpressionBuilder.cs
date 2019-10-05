using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public class BinaryExpressionBuilder : AbstractQueryBuilder
    {
        public BinaryExpressionBuilder Equal(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(EQUALS);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder NotEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(NOTEQUALS);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Less(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(LESS);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Greater(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(GREAT);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder LessEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(LESSOREQUAL);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder GreaterEqual(string left, string right)
        {
            Writer.Write(left);
            Writer.Write2(GREATOREQUAL);
            Writer.Write(right);
            return this;
        }
        public BinaryExpressionBuilder Between(string expression, string starting, string ending)
        {
            Writer.Write(expression);
            Writer.Write2(BETWEEN);
            Writer.Write(starting);
            Writer.Write2(AND);
            Writer.Write(ending);
            return this;
        }
        public BinaryExpressionBuilder IsNull(string expression)
        {
            Writer.Write(expression);
            Writer.Write2(ISNULL);
            return this;
        }
        public BinaryExpressionBuilder IsNotNull(string expression)
        {
            Writer.Write(expression);
            Writer.Write2(ISNOTNULL);
            return this;
        }
        public BinaryExpressionBuilder In(string expression, params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                Writer.Write(False);
                return this;
            }
            Writer.Write(expression);
            Writer.Write2(IN);
            Writer.WriteWithScoped(values);
            return this;
        }

        public BinaryExpressionBuilder NotIn(string expression, params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                Writer.Write(True);
                return this;
            }

            Writer.Write(expression);
            Writer.Write2(NOTIN);
            Writer.WriteWithScoped(values);
            return this;
        }
    }
}