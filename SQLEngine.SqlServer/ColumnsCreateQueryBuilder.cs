namespace SQLEngine.SqlServer
{
    internal class ColumnsCreateQueryBuilder : SqlServerQueryBuilder, IColumnsCreateQueryBuilder
    {
        public byte DefaultPrecision => 18;
        public byte DefaultScale => 4;

        public IColumnQueryBuilder Column(string columnName)
        {
            IColumnQueryBuilder builder = GetDefault<ColumnQueryBuilder>();
            builder.Name(columnName);
            return builder;
        }

        public IColumnQueryBuilder Datetime(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.DATETIME);
        }
        public IColumnQueryBuilder Long(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.BIGINT);
        }

        public IColumnQueryBuilder Int(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.INT);
        }
        public IColumnQueryBuilder Date(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.DATE);
        }
        public IColumnQueryBuilder Binary(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.VARBINARYMAX);
        }
        public IColumnQueryBuilder Byte(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.TINYINT);
        }

        public IColumnQueryBuilder String(string columnName, bool isUniCode = true, bool isVariable = true)
        {
            var type = isVariable ? isUniCode ? SQLKeywords.NVARCHAR : SQLKeywords.VARCHAR : isUniCode ? SQLKeywords.NCHAR : SQLKeywords.CHAR;
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(type);
        }

        public IColumnQueryBuilder Decimal(string columnName, byte precision = 18, byte scale = 4)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.DECIMAL).Precision(precision).Scale(scale);
        }

        public IColumnQueryBuilder Bool(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.BIT);
        }

        public IColumnQueryBuilder Short(string columnName)
        {
            return GetDefault<ColumnQueryBuilder>().Name(columnName).Type(SQLKeywords.SMALLINT);
        }

        public override string Build()
        {
            throw Bomb();
        }
    }
}