//namespace SQLEngine.SqlServer
//{
//    internal class AlterTableAlterColumnQueryBuilder : AbstractQueryBuilder
//    {
//        internal string SchemaName; 
//        internal string TableName;
//        internal string ColumnName;

//        private string _type;
//        private bool? _nullable;
//        private string _asExpression;
//        private bool? _asExpressionPersisted;


//        public AlterTableAlterColumnQueryBuilder Type(string type)
//        {
//            _type = type;
//            return this;
//        }

//        public AlterTableAlterColumnQueryBuilder Nullable(bool nullable = true)
//        {
//            _nullable = nullable;
//            return this;
//        }

//        public AlterTableAlterColumnQueryBuilder As(string expression, bool isPersisted)
//        {
//            _asExpression = expression;
//            _asExpressionPersisted = isPersisted;
//            return this;
//        }

//        public override string Build()
//        {
//            Writer.Write(SQLKeywords.ALTER);
//            Writer.Write2(SQLKeywords.TABLE);
//            if (!string.IsNullOrEmpty(SchemaName))
//            {
//                Writer.Write(I(SchemaName));
//                Writer.Write(SQLKeywords.DOT);
//            }

//            Writer.Write(I(TableName));
//            Writer.Write(SQLKeywords.SPACE);
//            Writer.Write(SQLKeywords.ALTER);
//            Writer.Write(SQLKeywords.COLUMN);
//            Writer.Write2(I(ColumnName));
//            Writer.Write2(_type);

//            Writer.Write(SQLKeywords.SEMICOLON);

//            return base.Build();
//        }
//    }
//}