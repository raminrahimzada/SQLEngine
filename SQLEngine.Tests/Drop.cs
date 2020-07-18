using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Drop_Function_1()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Function("fn_max")
                    .FromSchema("dbo")
                    .ToString();
                const string query = @"
DROP FUNCTION dbo.fn_max;
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        [TestMethod]
        public void Test_Drop_Table_1()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Table("Users")
                    .FromSchema("dbo")
                    .ToString();
                const string query = @"
DROP TABLE dbo.Users 
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        [TestMethod]
        public void Test_Drop_Table_2()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Table("Users")
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .ToString();
                const string query = @"
DROP TABLE facebook.dbo.Users 
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        [TestMethod]
        public void Test_Drop_Table_3()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Table<UserTable>()
                    .ToString();
                const string query = @"
DROP TABLE Users 
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        [TestMethod]
        public void Test_Drop_Table_4()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Table<UserTable>()
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .ToString();
                const string query = @"
DROP TABLE facebook.dbo.Users 
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }       [TestMethod]
        public void Test_Drop_View_1()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .View("VW_Users")
                    .FromSchema("dbo")
                    .ToString();
                const string query = @"
DROP VIEW dbo.VW_Users 
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        [TestMethod]
        public void Test_Drop_View_2()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .View("VW_Users")
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .ToString();
                const string query = @"
DROP VIEW facebook.dbo.VW_Users
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }

        [TestMethod]
        public void Test_Drop_Database()
        {
            using (var b = Query.New)
            {
                var dropQuery = b
                    .Drop
                    .Database("facebook")
                    .ToString();
                const string query = @"
DROP DATABASE facebook
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        
       
    }
}