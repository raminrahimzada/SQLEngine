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
                        ._drop
                        .Function("fn_max")
                        .Schema("dbo")
                        .Build();
                var query = @"
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
                    ._drop
                    .Table("Users")
                    .FromSchema("dbo")
                    .Build();
                var query = @"
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
                    ._drop
                    .Table("Users")
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .Build();
                var query = @"
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
                    ._drop
                    .Table<UserTable>()
                    .Build();
                var query = @"
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
                    ._drop
                    .Table<UserTable>()
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .Build();
                var query = @"
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
                    ._drop
                    .View("VW_Users")
                    .FromSchema("dbo")
                    .Build();
                var query = @"
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
                    ._drop
                    .View("VW_Users")
                    .FromSchema("dbo")
                    .FromDB("facebook")
                    .Build();
                var query = @"
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
                    ._drop
                    .Database("facebook")
                    .Build();
                var query = @"
DROP DATABASE facebook
";
                QueryAssert.AreEqual(dropQuery, query);
            }
        }
        
       
    }
}