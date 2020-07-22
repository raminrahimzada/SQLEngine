//namespace SQLEngine.SqlServer
//{
//    internal class AlterTableDropColumnQueryBuilder : AlteredTableQueryBuilder
//    {
//        internal string ColumnName;
//        public override void Build(ISqlWriter Writer)
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
//            Writer.Write(SQLKeywords.DROP);
//            Writer.Write2(SQLKeywords.COLUMN);
//            Writer.Write(I(ColumnName));
//            Writer.Write(SQLKeywords.SEMICOLON);

//            return;
//        }
//    }
//}