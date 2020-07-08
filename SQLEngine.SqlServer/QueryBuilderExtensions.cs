using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    public static class QueryBuilderExtensions
    {
        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <param name="builder">The builder.</param>
        public static void DeleteAll<TTable>(this SqlServerQueryBuilder builder)
            where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                var tableName = table.Name;
                builder.Delete(x => x.Table(tableName));
            }
        }
        public static ISelectWithoutWhereQueryBuilder WhereEquals(this ISelectWithoutFromQueryBuilder builder, string left, string right)
        {
            string whereClause;
            using (var b = new SqlServerQueryBuilder())
                whereClause = b.Helper.Equal(left, right);
            return builder.Where(whereClause);
        }

        public static IDeleteExceptWhereQueryBuilder WhereAnd(this IDeleteExceptTableNameQueryBuilder builder,
            params string[] conditions)
        {
            using (var b = new SqlServerQueryBuilder())
            {
                return builder.Where(b.Helper.And(conditions));
            }
        }
        public static IDeleteExceptWhereQueryBuilder WhereEquals(this IDeleteExceptTableNameQueryBuilder builder, string left, string right)
        {
            string whereClause;
            using (var b = new SqlServerQueryBuilder())
                whereClause = b.Helper.Equal(left, right);
            return builder.Where(whereClause);
        }
        public static IUpdateNoTableAndValuesAndWhereQueryBuilder WhereEquals(this IUpdateNoTableAndValuesQueryBuilder builder, string left, string right)
        {
            string whereClause;
            using (var b = new SqlServerQueryBuilder())
                whereClause = b.Helper.Equal(left, right);
            return builder.Where(whereClause);
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
        /// Throws the exception.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="args">The arguments.</param>
        public static void ThrowException(this IQueryBuilder builder, string exceptionMessage, params string[] args)
        {
            const int SQL_ERROR_STATE = 47;
            var list = new List<string>(args.Length + 1) { exceptionMessage.ToSQL() };
            list.AddRange(args);
            var errorMessageVar = builder.DeclareRandom("EXCEPTION", SQLKeywords.NVARCHARMAX);
            builder.Set(errorMessageVar, $"{SQLKeywords.FORMATMESSAGE}({list.JoinWith(", ")})");
            builder.AddExpression($"{SQLKeywords.RAISERROR}({errorMessageVar}, 18, {SQL_ERROR_STATE}) {SQLKeywords.WITH} {SQLKeywords.NOWAIT}");
        }
        /// <summary>
        /// Sets the no count on.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void SetNoCountOn(this SqlServerQueryBuilder builder)
        {
            builder.AddExpression("SET NOCOUNT ON;");
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
 
        public static void Truncate<TTable>(this SqlServerQueryBuilder builder) where TTable : ITable, new()
        {
            using (var table = new TTable())
            {
                builder.Truncate(table.Name);
            }
        }
  
        public static ISelectWithSelectorQueryBuilder SelectorAll(this ISelectQueryBuilder builder, string tableAlias = null)
        {
            if (string.IsNullOrEmpty(tableAlias))
                return builder.Selector(SQLKeywords.WILCARD);

            return builder.SelectorCol(tableAlias, SQLKeywords.WILCARD);
        }
        public static ISelectWithSelectorQueryBuilder SelectorAll(this ISelectWithSelectorQueryBuilder builder, string tableAlias = null)
        {
            if (string.IsNullOrEmpty(tableAlias))
                return builder.Selector(SQLKeywords.WILCARD);

            return builder.SelectorCol(tableAlias, SQLKeywords.WILCARD);
        }
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