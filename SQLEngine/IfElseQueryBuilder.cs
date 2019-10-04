using System;

namespace SQLEngine
{
    public class IfElseQueryBuilder: AbstractQueryBuilder
    {
        public IfElseQueryBuilder If(string condition)
        {
            Writer.Write("IF");
            Writer.WriteWithScoped(condition);
            return this;
        }
        public IfElseQueryBuilder If(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            var condition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();

            Writer.Write("IF");
            Writer.WriteWithScoped(condition);
            return this;
        }
        public IfElseQueryBuilder If(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            var condition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();

            Writer.Write("EXISTS");
            Writer.WriteWithScoped(condition);

            return this;
        }
        public IfElseQueryBuilder ElseIf(string condition)
        {
            Writer.Write("ELSE IF");
            Writer.WriteWithScoped(condition);
            return this;
        }
        public IfElseQueryBuilder ElseIf(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            var condition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();
            Writer.Write("ELSE IF ( EXISTS");
            Writer.WriteWithScoped(condition);
            Writer.Write(" )");

            return this;
        }
        public IfElseQueryBuilder ElseIf(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            var condition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();

            Writer.Write("ELSE IF");
            Writer.WriteWithScoped(condition);
            return this;
        }

        public IfElseQueryBuilder Then(string rawQuery)
        {
            Writer.WriteLine();
            Writer.WriteWithBeginEnd(rawQuery);
            return this;
        }
        public IfElseQueryBuilder Then(Func<AbstractQueryBuilder, AbstractQueryBuilder> builder)
        {
            var query = builder.Invoke(GetDefault());
            Writer.WriteWithBeginEnd(query.Build());
            return this;
        }

        public IfElseQueryBuilder Else(string expression)
        {
            Writer.WriteLine("ELSE");
            Writer.WriteWithBeginEnd(expression);
            return this;
        }
    }
}