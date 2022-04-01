using System;

namespace SQLEngine;

public static class Query
{
    /// <summary>
    /// Settings is Here
    /// </summary>
    public static class Settings
    {
        public static string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
        public static string DateFormat { get; set; } = "yyyy-MM-dd";
        public static byte DefaultPrecision { get; set; } = 18;
        public static byte DefaultScale { get; set; } = 4;
        public static int SqlErrorState { get; set; } = 47;
        public static string DatetimeOffsetFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff zzz";

        public static IEnumSqlStringConvertor EnumSqlStringConvertor { get; set; }
        public static IEscapeStrategy EscapeStrategy { get; set; }
        public static ITypeConvertor TypeConvertor { get; set; }
        public static IUniqueVariableNameGenerator UniqueVariableNameGenerator { get; set; }

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
    ///         q.Insert.Into....
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