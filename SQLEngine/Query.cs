using System;

namespace SQLEngine
{
    public static class Query
    {
        public static class Settings
        {
            public static string DefaultIdColumnName = "Id";
            public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            public static string DateFormat = "yyyy-MM-dd";
            public static byte DefaultPrecision = 18;
            public static byte DefaultScale = 4;
        }

        private static Func<IQueryBuilder> _builderFunction;
        public static void Setup(Func<IQueryBuilder> builderFunction)
        {
            _builderFunction = builderFunction;
        }
        public static void Setup<T>() where T : IQueryBuilder,new()
        {
            _builderFunction = () => Activator.CreateInstance<T>();
        }

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