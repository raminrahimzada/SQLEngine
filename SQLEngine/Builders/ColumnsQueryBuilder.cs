using static SQLEngine.SQLKeywords;
namespace SQLEngine.Builders
{
    public class ColumnsQueryBuilder : AbstractQueryBuilder
    {
        public ColumnQueryBuilder Column(string columnName)
        {
            ColumnQueryBuilder builder = GetDefault<ColumnQueryBuilder>();
            builder.Name(columnName);
            return builder;
        }

        public ColumnQueryBuilder Datetime(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(DATETIME);
        }
        public ColumnQueryBuilder Long(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(BIGINT);
        }

        public ColumnQueryBuilder Int(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(INT);
        }
        public ColumnQueryBuilder Date(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(DATE);
        }

        public ColumnQueryBuilder Byte(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(TINYINT);
        }

        public ColumnQueryBuilder String(string columnName, bool isUniCode = true, bool isVariable = true)
        {
            var type = isVariable ? isUniCode ? NVARCHAR : VARCHAR : isUniCode ? NCHAR : CHAR;
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(type);
        }

        public ColumnQueryBuilder Decimal(string columnName, byte precision = 18, byte scale = 4)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(DECIMAL).Precision(precision).Scale(scale);
        }

        public ColumnQueryBuilder Bool(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(BIT);
        }

        public override string Build()
        {
            Boom();
            return string.Empty;
        }
    }
}