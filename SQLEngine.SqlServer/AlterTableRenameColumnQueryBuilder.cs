//using System;

//namespace SQLEngine.SqlServer
//{
//    internal class AlterTableRenameColumnQueryBuilder : AlterTableQueryBuilder
//    {
//        internal string ColumnName;
//        private string _newColumnName;

//        public AlterTableRenameColumnQueryBuilder To(string newName)
//        {
//            _newColumnName = newName;
//            return this;
//        }

//        public override string Build()
//        {
//            throw new NotImplementedException();
//            //using (var t = new ExecuteQueryBuilder())
//            //{
//            //    var fullColumnName = $"{I(SchemaName)}.{I(TableName)}.{I(ColumnName)}";
//            //    //https://stackoverflow.com/a/9355281/7901692

//            //    var query = t.Procedure("sys.sp_rename")
//            //        .Arg("objtype", "COLUMN".ToSQL())
//            //        .Arg("objname", fullColumnName.ToSQL())
//            //        .Arg("newname", _newColumnName.ToSQL())
//            //        .Build();
//            //    Writer.WriteLine(query);
//            //}
//            //return base.Build();
//        }
//    }
//}