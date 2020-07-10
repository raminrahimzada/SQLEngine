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
                        .Value("NAME", "Ramin".ToSQL())
                        .Value("SURNAME", "Rahimzada".ToSQL())
                        .Value("AGE", 18.ToSQL())
                        .WhereEquals("ID", 41.ToSQL())
                        .ToString()
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET NAME = N'Ramin' , SURNAME = N'Rahimzada' , AGE = 18 WHERE (ID = 41)";
                
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void TestMethod6()
        {
            using (var t =Query.New)
            {
                var queryThat =
                        t
                            ._update
                            .Top(5)
                            .Table("Users")
                            .Value("Age", 21.ToSQL())
                            .WhereEquals("Id", "11")
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET Age = 21 WHERE Id = 11 ";
                QueryAssert.AreEqual(queryThat.Build(), query);
            }
        }
    }
}
