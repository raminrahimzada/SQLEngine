using System;

namespace SQLEngine.SqlServer
{
    internal class AlterQueryBuilder : AbstractQueryBuilder,IAlterQueryBuilder
    {
        public IAlterTableNoNameQueryBuilder Table(string tableName)
        {
            return GetDefault<AlterTableQueryBuilder>().TableName(tableName);
        }
    }

    public class AlterTableQueryBuilder :
        AbstractQueryBuilder
        , IAlterTableQueryBuilder
        , IAlterTableNoNameQueryBuilder
        , IAlterTableNoNameAlterColumnQueryBuilder
        , IAlterTableNoNameRenameColumnQueryBuilder
        , IAlterTableNoNameDropColumnQueryBuilder
        , IAlterTableNoNameAddColumnQueryBuilder
    {
        private string _tableName;
        public IAlterTableNoNameQueryBuilder TableName(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public IAlterTableNoNameAddColumnQueryBuilder AddColumn(string columnName, string columnType, bool? canBeNull = null,
            int? size = null, int? scale = null, ISqlExpression columnDefaultValue = null)
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write2(_tableName);
            Writer.Write2(SQLKeywords.ADD);
            Writer.Write2(SQLKeywords.COLUMN);
            Writer.Write2(columnName);
            Writer.Write2(columnType);
            if (canBeNull != null)
            {
                if (!canBeNull.Value)
                {
                    Writer.Write2(SQLKeywords.NOT);
                }
                Writer.Write2(SQLKeywords.NULL);
            }

            if (columnDefaultValue != null)
            {
                Writer.Write2(SQLKeywords.DEFAULT);
                Writer.Write2(columnDefaultValue.ToSqlString());
            }
            
            return this;
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

        public IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName, string columnNewName)
        {
            using (var t = new ExecuteQueryBuilder())
            {
                var fullColumnName = $"{I(_tableName)}.{I(columnName)}";
                //https://stackoverflow.com/a/9355281/7901692

                var query = t.Procedure("sys.sp_rename")
                    .Arg("objtype", "COLUMN".ToSQL())
                    .Arg("objname", fullColumnName.ToSQL())
                    .Arg("newname", columnNewName.ToSQL())
                    .Build();
                Writer.WriteLine(query);
            }

            return this;
        }

        public IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName, string newType, bool? canBeNull = true,
            int? size = null,
            int? scale = null)
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            Writer.Write(_tableName);
            Writer.Write2(SQLKeywords.ALTER);
            Writer.Write(SQLKeywords.COLUMN);
            Writer.Write2(columnName);
            Writer.Write(newType);
           
            if (size != null)
            {
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(size);
                if (scale != null)
                {
                    Writer.Write(SQLKeywords.COMMA);
                    Writer.Write(scale);
                }
                Writer.Write(SQLKeywords.END_SCOPE);
            }
            if (canBeNull != null)
            {
                if (!canBeNull.Value)
                {
                    Writer.Write2(SQLKeywords.NOT);
                }
                Writer.Write2(SQLKeywords.NULL);
            }
            return this;
        }
    }
}