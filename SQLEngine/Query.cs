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
            public static string DefaultIdColumnName = "Id";
            public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            public static string DateFormat = "yyyy-MM-dd";
            public static byte DefaultPrecision = 18;
            public static byte DefaultScale = 4;
            public static int SQLErrorState = 47;
        }

        private static Func<IQueryBuilder> _builderFunction;
        
        /// <summary>
        /// Use This Method to specify Which RDBMS You will use
        /// </summary>
        /// <param name="builderFunction"></param>
        public static void Setup(Func<IQueryBuilder> builderFunction)
        {
            _builderFunction = builderFunction;
        }
        /// <summary>
        /// Use This Method to specify Which RDBMS You will use
        /// </summary>
        public static void Setup<T>() where T : IQueryBuilder,new()
        {
            _builderFunction = () => Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Returns new IQueryBuilder object each times
        /// Use this inside using statement and build your queries 
        /// </summary>
        public static IQueryBuilder New
        {
            get
            {
                if (_builderFunction == null)
                {
                    throw new Exception("Please use QueryBuilderFactory.Use to setup");
                }

                return _builderFunction();
            }
        }
    }
}