using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Alter_Table_1()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                    ._alter
                    .Table("Users")
                    .AddColumn("Age","int")
                    //.DropColumn("Age")
                    //.RenameColumn("Age","Age_Of_User")
                    //.AlterColumn("Weight", newType: "DECIMAL", precision: 18, scale:4,maxLength:20, isFixedLength: true,isUnicode:true)
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE  Users  ADD  COLUMN  Age  int 
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_2()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        //.AddColumn("Age", "int")
                        .DropColumn("Age")
                        //.RenameColumn("Age","Age_Of_User")
                        //.AlterColumn("Weight", newType: "DECIMAL", precision: 18, scale:4,maxLength:20, isFixedLength: true,isUnicode:true)
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE  Users  DROP  COLUMN  Age 
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_3()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        //.AddColumn("Age", "int")
                        //.DropColumn("Age")
                        .RenameColumn("Age","Age_Of_User")
                        //.AlterColumn("Weight", newType: "DECIMAL", precision: 18, scale:4,maxLength:20, isFixedLength: true,isUnicode:true)
                        .ToString()
                    ;
                const string query =
                    @"
EXECUTE sys.sp_rename  @objtype=N'COLUMN'
	,@objname=N'Users.Age'
	,@newname=N'Age_Of_User';


";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_4()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        //.AddColumn("Age", "int")
                        //.DropColumn("Age")
                        //.RenameColumn("Age","Age_Of_User")
                        .AlterColumn("Name", newType: "varchar",false,15)
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Name varchar(15) NOT  NULL 
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        
        [TestMethod]
        public void Test_Alter_Table_5()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        //.AddColumn("Age", "int")
                        //.DropColumn("Age")
                        //.RenameColumn("Age","Age_Of_User")
                        .AlterColumn("Weight", newType: "decimal",true,18,4)
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NULL 
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}