namespace SQLEngine.SqlServer
{
    internal class AlterTableQueryBuilder : SqlServerAbstractQueryBuilder
    {
        internal string TableName;
        internal string SchemaName;
        public AlteredTableQueryBuilder AlterTable(string tableName, string schemaName = null)
        {
            var builder = GetDefault<AlteredTableQueryBuilder>();
            builder.TableName = tableName;
            builder.SchemaName = schemaName;
            return builder;
        }
    }
}