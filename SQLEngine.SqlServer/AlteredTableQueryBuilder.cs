//namespace SQLEngine.SqlServer
//{
//    internal class AlteredTableQueryBuilder : AlterTableQueryBuilder
//    {
//        public AlterTableDropColumnQueryBuilder DropColumn(string columnName)
//        {
//            var builder = GetDefault<AlterTableDropColumnQueryBuilder>();
//            builder.ColumnName = columnName;
//            builder.TableName = TableName;
//            return builder;
//        }
//        public AlterTableDropConstraintQueryBuilder DropConstraint(string constraintName)
//        {
//            var builder = GetDefault<AlterTableDropConstraintQueryBuilder>();
//            builder.ConstraintName = constraintName;
//            builder.TableName = TableName;
//            return builder;
//        }
//        public AlterTableAlterColumnQueryBuilder AlterColumn(string columnName)
//        {
//            var builder = GetDefault<AlterTableAlterColumnQueryBuilder>();
//            builder.ColumnName = columnName;
//            builder.TableName = TableName;
//            builder.SchemaName = SchemaName;
//            return builder;
//        }

//        public AlterTableRenameColumnQueryBuilder RenameColumn(string columnName)
//        {
//            var builder = GetDefault<AlterTableRenameColumnQueryBuilder>();
//            builder.ColumnName = columnName;
//            builder.TableName = TableName;
//            return builder;
//        }
//    }
//}