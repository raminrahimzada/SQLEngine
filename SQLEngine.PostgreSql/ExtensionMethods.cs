namespace SQLEngine.PostgreSql
{
    internal static class ExtensionMethods
    {
        internal static AbstractSqlLiteral ToSQL(this string str, bool isUnicode = true)
        {
            return AbstractSqlLiteral.From(str, isUnicode);
        }
        
        internal static string AsSQLVariable(this string variableName)
        {            
            return $"@{variableName}";
        }
    }
}
