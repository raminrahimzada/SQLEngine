using System;

namespace SQLEngine
{
    public static class Query
    {
        /// <summary>
        /// Settings is Here
        /// </summary>
        public static class Settings
        {
            public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            public static string DateFormat = "yyyy-MM-dd";
            public static byte DefaultPrecision = 18;
            public static byte DefaultScale = 4;
            public static int SQLErrorState = 47;
            public static string DatetimeOffsetFormat = "yyyy-MM-dd HH:mm:ss.fff zzz";

            public static IEnumSqlStringConvertor EnumSqlStringConvertor;
            public static IEscapeStrategy EscapeStrategy;
            public static ITypeConvertor TypeConvertor;
            public static IUniqueVariableNameGenerator UniqueVariableNameGenerator;
            [Obsolete("Warning: Experimental feature, Do not use")]
            public static IExpressionCompiler ExpressionCompiler { get; set; }
        }

        private static Func<IQueryBuilder> _builderFunction;

        /// <summary>
        /// Use This Method to specify Which RDBMS You will use
        /// </summary>        
        /// <param name="builderFunction"></param>
        // ReSharper disable once UnusedMember.Global
        public static void Setup(Func<IQueryBuilder> builderFunction)
        {
            _builderFunction = builderFunction;
        }
        /// <summary>
        /// Use This Method to specify Which RDBMS You will use
        /// <example>
        /// Query.Setup&gt;SqlServerQueryBuilder&lt;()
        /// </example>
        /// </summary>
        public static void Setup<T>() where T : IQueryBuilder,new()
        {
            _builderFunction = () => Activator.CreateInstance<T>();
            using (_builderFunction())
            {
                //Creating an Empty class for setting initialization
            }
        }

        /// <summary>
        /// Returns new IQueryBuilder object each times
        /// <br/>Use this inside using statement and build your queries
        /// <br/>But before all, please call <see cref="Query.Setup"/> method to specify sql dialect
        /// <example><code>using(var q = Query.New)
        /// <br/>{<br/>
        /// q.Insert.Into....
        /// <br/>}
        /// </code></example>
        /// </summary>
        public static IQueryBuilder New
        {
            get
            {
                if (_builderFunction == null)
                {
                    throw new SqlEngineException("Please use Query.Setup to setup");
                }

                return _builderFunction();
            }
        }
    }
}