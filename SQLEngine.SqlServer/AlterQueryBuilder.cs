using System;

namespace SQLEngine.SqlServer
{
    internal class AlterQueryBuilder : SqlServerAbstractQueryBuilder
    {
        private string _tableName;
        public AlterQueryBuilder(string tableName)
        {
            _tableName = tableName;
        }


        public AlterTableRenameColumnQueryBuilder RenameColumn(string name)
        {
            throw new NotImplementedException();
        }

        public AlterTableDropColumnQueryBuilder DropColumn(string name)
        {
            throw new NotImplementedException();
        }
    }

    //public class AlterQueryBuilder : SqlServerAbstractQueryBuilder
    //{
    //    public AlterTableQueryBuilder Table { get; } = GetDefault<AlterTableQueryBuilder>();
    //    public AlterViewQueryBuilder View { get; } = GetDefault<AlterViewQueryBuilder>();
    //}
}