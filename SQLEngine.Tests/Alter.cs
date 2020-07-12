using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Alter_Table_AddColumn()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        .AddColumn("Age")
                        .NotNull()
                        .OfType("DECIMAL")
                        .Size(18, 4)
                        .DefaultValue(18.ToSQL())
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE Users ADD COLUMN Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_DropColumn()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        .DropColumn("Age")
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
        public void Test_Alter_Table_RenameColumn()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        .RenameColumn("Age")
                        .To("Age_Of_User")
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
        public void Test_Alter_Table_AlterColumn()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        .AlterColumn("Name")
                        .NewType("VARCHAR")
                        .NotNull()
                        .Size(15)
                        .DefaultValue("Anonymous".ToSQL())
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Name VARCHAR(15) NOT NULL  DEFAULT ( N'Anonymous' )
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
        
        [TestMethod]
        public void Test_Alter_Table_AlterColumn_Decimal()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._alter
                        .Table("Users")
                        .AlterColumn("Weight")
                        .NewType("decimal")
                        .NotNull()
                        .Size(18, 4)
                        .ToString()
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NOT NULL 
";

                QueryAssert.AreEqual(queryThat, query);
            }
        }


        [TestMethod]
        public void Test_Alter_Table_1()
        {
            using (var t = Query.New)
            {
//                var queryThat = t
//                        ._alter
//                        .Table("Users")
//                        .AddIndex("IX_Unique_Email")
//                        .On("Email")
//                        .ToString()
//                    ;
//                const string query =
//                    @"
//ALTER TABLE  Users  ADD  COLUMN  Age  int 
//";

//                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}