using System;

namespace SQLEngine
{
    public  class AbstractConditionBuilder : AbstractQueryBuilder
    {
        public ExistsConditionBuilder Exists(Func<SelectQueryBuilder, SelectQueryBuilder> builder)
        {
            return new ExistsConditionBuilder().Exists(builder);
        }
    }
}