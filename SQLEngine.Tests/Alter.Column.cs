using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {

        [TestMethod]
        public void Test_Alter_Table_AddColumn()
        {
            using (var q = Query.New)
            {
                q
                    .Alter
                    .Table("Users")
                    .AddColumn("Age")
                    .NotNull()
                    .OfType("DECIMAL")
                    .Size(18, 4)
                    .DefaultValue(18);

                const string query =
                    @"
ALTER TABLE Users ADD COLUMN Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

                QueryAssert.AreEqual(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Alter_Table_AddColumn_OfType_Strong_Typed()
        {
            using (var q = Query.New)
            {
                q
                        .Alter
                        .Table("Users")
                        .AddColumn("Age")
                        .NotNull()
                        .OfType<decimal>()
                        .Size(18, 4)
                        .DefaultValue(18)
                    ;
                const string query =
                    @"
ALTER TABLE Users ADD COLUMN Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_DropColumn()
        {
            using (var q = Query.New)
            {
                q
                        .Alter
                        .Table("Users")
                        .DropColumn("Age")
                    ;
                const string query =
                    @"
ALTER TABLE  Users  DROP  COLUMN  Age 
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_RenameColumn()
        {
            using (var q = Query.New)
            {
                q
                        .Alter
                        .Table("Users")
                        .RenameColumn("Age")
                        .To("Age_Of_User")
                    ;
                const string query =
                    @"
EXECUTE sys.sp_rename  @objtype=N'COLUMN'
	,@objname=N'Users.Age'
	,@newname=N'Age_Of_User';
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Alter_Table_AlterColumn()
        {
            using (var q = Query.New)
            {
                q
                        .Alter
                        .Table("Users")
                        .AlterColumn("Name")
                        .Type("VARCHAR")
                        .NotNull()
                        .Size(15)
                        .DefaultValue("Anonymous")
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Name VARCHAR(15) NOT NULL  DEFAULT ( N'Anonymous' )
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Alter_Table_AlterColumn_Decimal()
        {
            using (var q = Query.New)
            {
                q
                        .Alter
                        .Table<UserTable>() //another form you can use
                        .AlterColumn("Weight")
                        .Type("decimal")
                        .NotNull()
                        .Size(18, 4)
                    ;
                const string query =
                    @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NOT NULL 
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

    }
}