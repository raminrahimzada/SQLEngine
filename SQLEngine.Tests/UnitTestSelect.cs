//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SQLEngine.Builders;

//namespace SQLEngine.Tests
//{
//    [TestClass]
//    public class UnitTestSelect
//    {
//        [TestMethod]
//        public void TestMethod1()
//        {
//            using (var t=new SelectQueryBuilder())
//            {
//                t.From("U", "Users")
//                    .Top(1)
//                    .Selector("Name")
//                    .Selector("Surname")
//                    .InnerJoin("P", "Photos", "UserId", "Id")
//                    .LeftJoin("A", "Attachments", "UserId", "Id")
//                    .RightJoin("S", "Sales", "UserId", "Id")
//                    .Where(x =>
//                        x.Firstly(e => e.Greater("Age", "17"))
//                            .And(e => e.Equal("ID", "1"))
//                    )
//                    ;
//                const string query = @" 
//SELECT TOP(1)  Name , Surname FROM Users AS U
//    INNER JOIN  Photos AS P ON U.Id = P.UserId
//    LEFT JOIN   Attachments AS A ON U.Id = A.UserId
//    RIGHT JOIN  Sales AS S ON U.Id = S.UserId 
//WHERE (Age > 17)  AND (ID = 1)
//";
//                QueryAssert.AreEqual(t.Build(), query);
//            }
//        }
//    }
//}
