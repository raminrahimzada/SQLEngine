namespace SQLEngine.SqlServer
{
    internal static class ExtensionMethods
    {
        internal static SqlServerLiteral ToSQL(this string str, bool isUnicode = true)
        {
            return SqlServerLiteral.From(str, isUnicode);
        }
        
        internal static string AsSQLVariable(this string variableName)
        {
            return $"@{variableName}";
        }
    }
}
