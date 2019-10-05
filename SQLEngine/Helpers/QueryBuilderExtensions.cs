namespace SQLEngine.Helpers
{
    public static class QueryBuilderExtensions
    {
        public static string IsNull(this AbstractQueryBuilder builder,string key)
        {
            return $"[{key}] IS NULL";
        }
        public static string IsNotNull(this AbstractQueryBuilder builder, string key)
        {
            return $"[{key}] IS NOT NULL";
        }
        public static string Equal(this AbstractQueryBuilder builder, string key, string value)
        {
            if (value.ToLower() == "NULL")
            {
                return builder.IsNull(key);
            }
            return $"[{key}]={value}";
        }
        public static string NotEqual(this AbstractQueryBuilder builder, string key, string value)
        {
            if (value.ToLower() == "NULL")
            {
                return builder.IsNotNull(key);
            }
            return $"[{key}]<>{value}";
        }
        public static string GreaterThan(this QueryBuilder builder, string key, string value)
        {
            return $"[{key}]>{value}";
        }
        public static string LessThan(this QueryBuilder builder, string key, string value)
        {
            return $"[{key}]<{value}";
        }
        public static string Equal(this QueryBuilder builder, string key, string value, string alias)
        {
            return $"{alias}.[{key}]={value}";
        }

        public static string BetWeen(this QueryBuilder builder, string expression, string starting, string ending)
        {
            return $"{expression} BETWEEN {starting} AND {ending}";
        }

     
        public static string As(this QueryBuilder builder, string tableName, string columnName, string alias)
        {
            return $"[{tableName}].[{columnName}] AS [{alias}]";
        }

        
        public static string Map(this QueryBuilder builder, string columnName, string alias)
        {
            return $"[{columnName}] AS [{alias}]";
        }

        public static string Top(this QueryBuilder builder, int count, string selection = "*")
        {
            return $"TOP({count}) {selection}";
        }


       
    }
}