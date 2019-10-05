using System;
using System.Text;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public class IfConditionBuilder : AbstractConditionBuilder
    {
        private readonly StringBuilder _stringBuilder;
        public IfConditionBuilder()
        {
            _stringBuilder = new StringBuilder();
        }
        public IfConditionBuilder Firstly(string condition)
        {
            _stringBuilder.Append(condition);
            return this;
        }
        public IfConditionBuilder Firstly(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();
            _stringBuilder.Append(condition);
            return this;
        }
        public IfConditionBuilder And(string condition)
        {
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(AND);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder And(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(AND);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder And(Func<IfConditionBuilder, IfConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<IfConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(AND);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder And(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(AND);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder And(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(AND);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder Or(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(OR);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder Or(Func<IfConditionBuilder, IfConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<IfConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(OR);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder Or(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            string condition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(OR);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }
        public IfConditionBuilder Or(string condition)
        {
            _stringBuilder.Insert(0, BEGIN_SCOPE);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            _stringBuilder.Append(OR);
            _stringBuilder.Append(BEGIN_SCOPE);
            _stringBuilder.Append(condition);
            _stringBuilder.Append(END_SCOPE);
            _stringBuilder.Append(SPACE);
            return this;
        }

        public override string Build()
        {
            ValidateAndThrow();
            return _stringBuilder.ToString();
        }
    }
}