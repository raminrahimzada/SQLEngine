using System;
using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public class DeleteQueryBuilder : AbstractQueryBuilder
    {
        private string _tableName;
        private int? _topClause;
        private string _whereCondition;
        public DeleteQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public DeleteQueryBuilder Top(int? count)
        {
            _topClause = count;
            return this;
        }
        public DeleteQueryBuilder Where(string condition)
        {
            _whereCondition = condition;
            return this;
        }
        public DeleteQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            return this;
        }
        public DeleteQueryBuilder Where(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();
            return this;
        }
        public DeleteQueryBuilder Where(Func<ExistsConditionBuilder, ExistsConditionBuilder> builder)
        {
            _whereCondition = builder.Invoke(GetDefault<ExistsConditionBuilder>()).Build();
            return this;
        }

        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write2(DELETE);
            if (_topClause != null)
            {
                Writer.Write(TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write2();
            }
            Writer.Write2(FROM);

            Writer.Write(I(_tableName));

            if (!string.IsNullOrEmpty(_whereCondition))
            {
                Writer.Write2(WHERE);
                Writer.WriteScoped(_whereCondition);
            }
            return base.Build();
        }
    }
}