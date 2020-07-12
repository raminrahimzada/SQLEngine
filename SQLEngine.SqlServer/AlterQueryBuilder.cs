using System;

namespace SQLEngine.SqlServer
{
    internal class AlterQueryBuilder : AbstractQueryBuilder,IAlterQueryBuilder
    {
        private string _tableName;

        public AlterTableRenameColumnQueryBuilder RenameColumn(string name)
        {
            throw new NotImplementedException();
        }

        public AlterTableDropColumnQueryBuilder DropColumn(string name)
        {
            throw new NotImplementedException();
        }
    }

    //public class AlterQueryBuilder : AbstractQueryBuilder
    //{
    //    public AlterTableQueryBuilder Table { get; } = GetDefault<AlterTableQueryBuilder>();
    //    public AlterViewQueryBuilder View { get; } = GetDefault<AlterViewQueryBuilder>();
    //}
}