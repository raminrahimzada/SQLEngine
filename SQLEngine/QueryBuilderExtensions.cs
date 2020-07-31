namespace SQLEngine
{
    public static class QueryBuilderExtensions
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <param name="builder">The builder.</param>
        public static void DeleteAll<TTable>(this IQueryBuilder builder)
            where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                var tableName = table.Name;
                builder.Delete.Table(tableName);
            }
        }
        public static IDeleteExceptTableNameQueryBuilder Table<TTable>(this IDeleteQueryBuilder builder)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.Table(tableName);
            }            
        }
        
        
        /// <summary>
        /// Tables the specified builder.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>UpdateQueryBuilder.</returns>
        public static IUpdateNoTableQueryBuilder Table<TTable>(this IUpdateQueryBuilder builder)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.Table(tableName);
            }
                
        }
        public static IUpdateNoTableQueryBuilder Table<TTable>(this IUpdateNoTopQueryBuilder builder) 
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.Table(tableName);
            }                
        }

        /// <summary>
        /// Foreigns the key.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>ColumnQueryBuilder.</returns>
        public static IColumnQueryBuilder ForeignKey<TTable>(this IColumnQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.ForeignKey(tableName, ID);
            }
        }
        public static IColumnQueryBuilder ForeignKey<TTable>(this IColumnsCreateQueryBuilder builder, string columnName) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.Long(columnName).ForeignKey(tableName, ID);
            }
        }

        /// <summary>
        /// Froms the specified builder.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>SelectQueryBuilder.</returns>
        public static ISelectWithoutFromQueryBuilder From<TTable>(this ISelectQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.From(tableName);
            }
        }

        public static ISelectWithoutFromQueryBuilder From<TTable>(this ISelectWithSelectorQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.From(tableName);
            }
                
        }
        public static ISelectWithoutFromQueryBuilder From<TTable>(this ISelectQueryBuilder builder, string alias) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.From(tableName, alias);
            }
        }
        public static ISelectWithoutFromQueryBuilder From<TTable>(this ISelectWithSelectorQueryBuilder builder, string alias) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                return builder.From(tableName, alias);
            }
        }
        public static IJoinedQueryBuilder InnerJoin(this ISelectQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName,string ID)
        {
            return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder InnerJoin(this ISelectWithoutFromQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName, string ID)
        {
            return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder InnerJoin(this IJoinedQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName, string ID)
        {
            return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder InnerJoin(this ISelectWithSelectorQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName, string ID)
        {
            return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder LeftJoin<TTable>(this ISelectWithoutFromQueryBuilder builder, string alias,
             string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.LeftJoin(alias, tableName, mainTableColumnName, ID);
            }
                
        }
        public static IJoinedQueryBuilder LeftJoin<TTable>(this IJoinedQueryBuilder builder, string alias,
             string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.LeftJoin(alias, tableName, mainTableColumnName, ID);
            }
        }
        public static IJoinedQueryBuilder RightJoin<TTable>(this IJoinedQueryBuilder builder, string alias,
             string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.RightJoin(alias, tableName, mainTableColumnName, ID);
            }
        }
      
        public static IJoinedQueryBuilder InnerJoin<TTable>(this ISelectWithoutFromQueryBuilder builder, string alias,
            string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.InnerJoin(alias, tableName, mainTableColumnName,ID);
            }
            
        }
        public static IJoinedQueryBuilder InnerJoin<TTable>(this IJoinedQueryBuilder builder, string alias,
            string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
            }
            
        }
        public static IJoinedQueryBuilder InnerJoin<TTable>(this ISelectWithSelectorQueryBuilder builder, string alias,
            string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.InnerJoin(alias, tableName, mainTableColumnName, ID);
            }
            
        }
       
        public static IJoinedQueryBuilder InnerJoin<TTable>(this ISelectQueryBuilder builder, string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.InnerJoin(null, tableName, mainTableColumnName, ID);
            }
            
        }
       
        public static IJoinedQueryBuilder RightJoin<TTable>(this ISelectWithoutFromQueryBuilder builder, string alias,
            string mainTableColumnName)
            where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                var tableName = table.Name;
                var ID = table.PrimaryColumnName;
                return builder.RightJoin(alias, tableName, mainTableColumnName, ID);
            }
            
        }
         
        public static IJoinedQueryBuilder RightJoin(this ISelectQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName,string ID)
        {
            return builder.RightJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder RightJoin(this ISelectWithoutFromQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName, string ID)
        {
            return builder.RightJoin(alias, tableName, mainTableColumnName, ID);
        }
      
        public static IInsertNoIntoQueryBuilder Into<TTable>(this IInsertQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                return builder.Into(table.Name);
            }
        }
 
        public static void Truncate<TTable>(this IQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                builder.Truncate(table.Name);
            }
        }
  
        //public static ISelectWithSelectorQueryBuilder SelectorAll(this ISelectQueryBuilder builder, string tableAlias = null)
        //{
        //    if (string.IsNullOrEmpty(tableAlias))
        //        return builder.SelectColumn(SQLKeywords.WILCARD);

        //    return builder.SelectColumn(tableAlias, SQLKeywords.WILCARD);
        //}
        //public static ISelectWithSelectorQueryBuilder SelectorAll(this ISelectWithSelectorQueryBuilder builder, string tableAlias = null)
        //{
        //    if (string.IsNullOrEmpty(tableAlias))
        //        return builder.SelectColumn(SQLKeywords.WILCARD);

        //    return builder.SelectCol(tableAlias, SQLKeywords.WILCARD);
        //}
        public static IJoinedQueryBuilder LeftJoin(this ISelectWithoutFromQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName,string ID)
        {
            return builder.LeftJoin(alias, tableName, mainTableColumnName, ID);
        }
        public static IJoinedQueryBuilder LeftJoin(this IJoinedQueryBuilder builder, string alias, string tableName,
            string mainTableColumnName,string ID)
        {
            return builder.LeftJoin(alias, tableName, mainTableColumnName, ID);
        }
    }
}