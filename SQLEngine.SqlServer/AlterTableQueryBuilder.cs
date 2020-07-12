namespace SQLEngine.SqlServer
{
    internal class AlterTableQueryBuilder :
            AbstractQueryBuilder
            , IAlterTableQueryBuilder
            , IAlterTableNoNameQueryBuilder
            , IAlterTableNoNameDropColumnQueryBuilder
    {
        private string _tableName;
        public IAlterTableNoNameQueryBuilder TableName(string tableName)
        {
            _tableName = tableName;
            return this;
        }
      
        public IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName)
        {
            return GetDefault<AlterTableAddColumnQueryBuilder>().Table(_tableName).Column(columnName);
        }

        public IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName)
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write2(_tableName);
            Writer.Write2(SQLKeywords.DROP);
            Writer.Write2(SQLKeywords.COLUMN);
            Writer.Write2(columnName);
            return this;
        }

        public IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName)
        {
            return GetDefault<AlterTableRenameColumnQueryBuilder>().Table(_tableName).Column(columnName);
        }

        public IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName)
        {
            return GetDefault<AlterTableAlterColumnQueryBuilder>().Table(_tableName).Column(columnName);
        }
    }
}