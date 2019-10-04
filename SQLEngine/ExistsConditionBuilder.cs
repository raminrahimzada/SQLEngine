using System;

namespace SQLEngine
{
    public class ExistsConditionBuilder: AbstractConditionBuilder
    {
        public new ExistsConditionBuilder Exists(Func<SelectQueryBuilder, SelectQueryBuilder> builder)
        {
            var selection = builder.Invoke(GetDefault<SelectQueryBuilder>()).Build();
            return Exists(selection);
        }
        public ExistsConditionBuilder Exists(string selectQuery)
        {
            Writer.Write(selectQuery);
            return this;
        }
    }
}