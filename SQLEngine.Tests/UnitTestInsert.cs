//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SQLEngine.Builders;

//namespace SQLEngine.Tests
//{
//    [TestClass]
//    public class UnitTestInsert
//    {
//        [TestMethod]
//        public void TestMethod1()
//        {
//            using (var t = new InsertQueryBuilder())
//            {
//                var data = new object[]
//                {
//                    "Ramin", 
//                    "Rahimzada", 
//                    1
//                }
//                .Select(o => o.ToSQL());
//                //
//                t.Into("Users");
//                t.Values(data);
//                const string query = "INSERT INTO Users VALUES (N'Ramin' , N'Rahimzada' , 1)";
//                QueryAssert.AreEqual(t.Build(),query);
//            }
//        }
//        [TestMethod]
//        public void TestMethod2()
//        {
//            using (var t = new InsertQueryBuilder())
//            {
//                var data = new object[]
//                {
//                    "Ramin", 
//                    "Rahimzada", 
//                    1
//                }
//                .Select(o => o.ToSQL());
//                //
//                var cols = new[] {"NAME", "SURNAME", "AGE"};
//                //
//                t.Into("Users").Columns(cols).Values(data);
//                const string query = "INSERT INTO Users(NAME , SURNAME , AGE) VALUES (N'Ramin' , N'Rahimzada' , 1)";
//                QueryAssert.AreEqual(t.Build(),query);
//            }
//        }
//        [TestMethod]
//        public void TestMethod3()
//        {
//            using (var t = new InsertQueryBuilder())
//            {
//                //person info
//                var p = new
//                {
//                    Name = "Ramin",
//                    Surname = "Rahimzada",
//                    Age = 1
//                };
//                //query
//                t.Into("Users")
//                    .Value("NAME",p.Name.ToSQL() )
//                    .Value("SURNAME", p.Surname.ToSQL())
//                    .Value("AGE", p.Age.ToSQL())
//                    ;
//                const string query = "INSERT INTO Users(NAME , SURNAME , AGE) VALUES (N'Ramin' , N'Rahimzada' , 1)";
//                QueryAssert.AreEqual(t.Build(), query);
//            }
//        }
//        [TestMethod]
//        public void TestMethod4()
//        {
//            using (var t = new InsertQueryBuilder())
//            {
//                t.Into("Users")
//                    .Values(x => x.From("Users_Backup"))
//                    ;
//                const string query = "INSERT INTO Users  SELECT  *  FROM Users_Backup";
//                QueryAssert.AreEqual(t.Build(), query);
//            }
//        }
//    }
//}
