namespace SQLEngine.SqlServer
{
    internal class AlterTableRenameColumnQueryBuilder:AbstractQueryBuilder
        , IAlterTableNoNameRenameColumnQueryBuilder
        , IAlterTableNoNameRenameColumnNoNewNameQueryBuilder
    {
        private string _tableName;
        private string _columnNewName;
        private string _columnName;

        public AlterTableRenameColumnQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IAlterTableNoNameRenameColumnNoNewNameQueryBuilder To(string newName)
        {
            _columnNewName = newName;
            return this;
        }

        public AlterTableRenameColumnQueryBuilder Column(string columnName)
        {
            _columnName = columnName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            //using (var t = new ExecuteQueryBuilder())
            {
                var t = new ExecuteQueryBuilder();
                var fullColumnName = $"{I(_tableName)}.{I(_columnName)}";
                //https://stackoverflow.com/a/9355281/7901692

                t.Procedure("sys.sp_rename")
                    .Arg("objtype", "COLUMN".ToSQL())
                    .Arg("objname", fullColumnName.ToSQL())
                    .Arg("newname", _columnNewName.ToSQL())
                    .Build(writer);
            }
        }
    }
}