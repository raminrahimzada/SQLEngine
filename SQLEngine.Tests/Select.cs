using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Simple_Select_1()
        {
            using (var q = Query.New)
            {
                var queryThis = q
                    ._select
                    .Top(1)
                    .From("Users")
                    .WhereColumnEquals("Id", 17)
                    .ToString();

                var queryThat = @"
SELECT TOP(1)  * 
    FROM Users
    WHERE Id = 17
";
                QueryAssert.AreEqual(queryThis, queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Simple_Filter()
        {
            using (var q = Query.New)
            {
                var filter = q.Helper.ColumnGreaterThan("Age", 18);
                var queryThis = q
                    ._select
                    .Top(1)
                    .Selector("Name")
                    .Selector("Surname")
                    .From("Users")
                    .Where(filter)
                    .ToString();

                var queryThat = @"
SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE Age > 18
";
                QueryAssert.AreEqual(queryThis, queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_Complex_Filter_1()
        {
            using (var q = Query.New)
            {
                var filter1 = q.Helper.ColumnGreaterThan("Age", 18);
                var filter2 = q.Helper.ColumnLessThan("Height", 1.7);
                
                var queryThis = q
                    ._select
                    .Top(1)
                    .Selector("Name")
                    .Selector("Surname")
                    .From("Users")
                    .WhereAnd(filter1,filter2)
                    .ToString();

                var queryThat = @"

SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE (Age > 18) and (Height < 1.7)

";
                QueryAssert.AreEqual(queryThis, queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Complex_Filter_2()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                var height = q.Column("Height");

                var queryThis = q
                    ._select
                    .Top(1)
                    .Selector("Name")
                    .Selector("Surname")
                    .From("Users")
                    .Where(age > 18 & height <= 1)
                    .ToString();

                var queryThat = @"

SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE (Age > 18) and (Height < 1.7)

";
                QueryAssert.AreEqual(queryThis, queryThat);

            }
        }
        [TestMethod]
        public void Test_Select_With_Joins()
        {
            using (var t = Query.New)
            {
                var age = t.Column("Age");
                var queryFromBuilder = t._select
                        .Top(1)
                        .From("Users", "U")
                        .Selector("Name")
                        .Selector("Surname")
                        .InnerJoin("P", "Photos", "UserId")
                        .LeftJoin("A", "Attachments", "UserId")
                        .RightJoin("S", "Sales", "UserId")
                        .Where(age > 18)
                        .ToString()
                    ;

                const string query = @" 
SELECT TOP(1)  Name , Surname
    FROM Users AS U
	INNER JOIN	Photos AS P ON U.UserId = P.Id
	LEFT JOIN	Attachments AS A ON U.UserId = A.Id
	RIGHT JOIN	Sales AS S ON U.UserId = S.Id
    WHERE Age > 18
";

                QueryAssert.AreEqual(queryFromBuilder, query);
            }
        }
    }
}
