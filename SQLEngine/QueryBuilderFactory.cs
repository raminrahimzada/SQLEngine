using System;

namespace SQLEngine
{
    public static class QueryBuilderFactory
    {
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