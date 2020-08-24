using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Update_1()
        {
            using (var q = Query.New)
            {
                q
                    .Update
                    .Top(5)
                    .Table("Users")
                    .Value("NAME", "Ramin")
                    .Value("SURNAME", "Rahimzada")
                    .Value("AGE", 18)
                    .WhereColumnEquals("ID", 41)
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET NAME = N'Ramin' , SURNAME = N'Rahimzada' , AGE = 18 WHERE (ID = 41)";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Update_Filter_1()
        {
            using (var q = Query.New)
            {
                var id = q.Column("Id");

                q
                    .Update
                    .Top(5)
                    .Table("Users")
                    .Value("Age", 21)
                    .Where(id == 17)
                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET Age = 21
     WHERE (Id = 17)
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Update_Filter_2()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");

                q
                    .Update
                    .Top(5)
                    .Table("Users")
                    .Value("CanWatchMovie", true)
                    .Where(age >= 18)
                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET CanWatchMovie = 1
     WHERE (Age >= 18)
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Update_Filter_3()
        {
            using (var q = Query.New)
            {
                var lastLogin = q.Column("LastLoginDate");


                q
                    .Update
                    .Top(5)
                    .Table("Users")
                    .Value("Blocked", true)
                    .Where(lastLogin <= DateTime.Parse("01/01/2000"))

                    ;
                const string query =
                    @"
 UPDATE TOP(5) Users
     SET Blocked = 1
     WHERE (LastLoginDate <= '2000-01-01 00:00:00.000')
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Update_Filter_4()
        {
            using (var q = Query.New)
            {
                var id = q.Column("Id");

                q
                    .Update
                    .Top(5)
                    .Table("Users")
                    .Value("Blocked", true)
                    .Value("BlockDate", DateTime.Parse("01/01/2020"))
                    .Where((id < 100) & (id > 10))

                    ;
                const string query =
                    @"
UPDATE TOP(5) Users
 SET Blocked = 1 , BlockDate = '2020-01-01 00:00:00.000'
 WHERE ((Id < 100) AND (Id > 10))
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
    }
}