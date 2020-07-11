using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void TestMethod5()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._update
                        .Top(5)
                        .Table("Users")
                        .Value("NAME", "Ramin")
                        .Value("SURNAME", "Rahimzada")
                        .Value("AGE", 18)
                        .WhereColumnEquals("ID", 41)
                        .ToString()
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET NAME = N'Ramin' , SURNAME = N'Rahimzada' , AGE = 18 WHERE (ID = 41)";
                
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void TestMethod_Update_Filter_1()
        {
            using (var t =Query.New)
            {
                var id = t.Column("Id");

                var queryThat =
                        t
                            ._update
                            .Top(5)
                            .Table("Users")
                            .Value("Age", 21)
                            .Where(id == 17)
                            .ToString()
                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET Age = 21
     WHERE (Id = 17)
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void TestMethod_Update_Filter_2()
        {
            using (var t =Query.New)
            {
                var age = t.Column("Age");

                var queryThat =
                        t
                            ._update
                            .Top(5)
                            .Table("Users")
                            .Value("CanWatchMovie", true)
                            .Where(age >= 18)
                            .ToString()
                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET CanWatchMovie = 1
     WHERE (Age >= 18)
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }


        [TestMethod]
        public void TestMethod_Update_Filter_3()
        {
            using (var t = Query.New)
            {
                var lastLogin = t.Column("LastLoginDate");

                var queryThat =
                        t
                            ._update
                            .Top(5)
                            .Table("Users")
                            .Value("Blocked", true)
                            .Where(lastLogin <= DateTime.Parse("01/01/2000"))
                            .ToString()
                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET Blocked = 1
     WHERE (LastLoginDate <= '2000-01-01 00:00:00.000')
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        
        [TestMethod]
        public void TestMethod_Update_Filter_4()
        {
            using (var t = Query.New)
            {
                var id = t.Column("Id");
                var queryThat =
                        t
                            ._update
                            .Top(5)
                            .Table("Users")
                            .Value("Blocked", true)
                            .Value("BlockDate", DateTime.Parse("01/01/2020"))
                            .Where((id < 100) & (id > 10))
                            .ToString()
                    ;
                const string query =
                    @"
UPDATE TOP(5) Users
 SET Blocked = 1 , BlockDate = '2020-01-01 00:00:00.000'
 WHERE ((Id < 100) AND (Id > 10))
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}
