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
                q
                    .Select
                    .Top(1)
                    .From("Users")
                    .WhereColumnEquals("Id", 17);
                

                const string queryThat = @"
SELECT TOP(1)  * 
    FROM Users
    WHERE Id = 17
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_2()
        {
            using (var q = Query.New)
            {
                var userName = q.Column("UserName");
                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(userName == "admin")
                    ;

                var queryThat = @"
SELECT TOP(1)  * 
    FROM Users
    WHERE UserName = N'admin'
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Between()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");

                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(age.Between(10, 60))
                    ;

                const string queryThat = @"
SELECT TOP(1) * 
    FROM Users
    WHERE (Age BETWEEN 10 AND 60)
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_In()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");

                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(age.In(11, 22, 33))
                    ;

                const string queryThat = @"
SELECT TOP(1) * 
    FROM Users
    WHERE Age IN (11,22,33)
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_With_Alias_1()
        {
            using (var q = Query.New)
            {
                var userName = q.Column("UserName", "U");
                q
                    .Select
                    .Top(1)
                    .From<UserTable>("U")
                    .Where(userName == "admin")
                    ;

                var queryThat = @"
SELECT TOP(1)   * 
    FROM Users AS U
    WHERE U.UserName = N'admin'
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Simple_Filter()
        {
            using (var q = Query.New)
            {
                var filter = q.Helper.ColumnGreaterThan("Age", 18);
                q
                    .Select
                    .Top(1)
                    .Select("Name")
                    .Select("Surname")
                    .From("Users")
                    .Where(filter)
                    ;

                var queryThat = @"
SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE Age > 18
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_Complex_Filter_1()
        {
            using (var q = Query.New)
            {
                var filter1 = q.Helper.ColumnGreaterThan("Age", 18);
                var filter2 = q.Helper.ColumnLessThan("Height", 1.7);

                q
                    .Select
                    .Top(1)
                    .Select("Name")
                    .Select("Surname")
                    .From("Users")
                    .WhereAnd(filter1, filter2)
                    ;

                const string queryThat = @"

SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE (Age > 18) and (Height < 1.7)

";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Complex_Filter_2()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                var height = q.Column("Height");
                var id = q.Column("Id");

                q
                    .Select
                    .Top(1)
                    .Select("Name")
                    .Select("Surname")
                    .From("Users")
                    .Where(age > 18 & height <= 1.7 & id != 1)
                    ;

                const string queryThat = @"

SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE ((Age > 18) AND (Height <= 1.7)) AND (Id = 1)
";
                QueryAssert.AreEqual(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Select_With_Joins()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                q.Select
                        .Top(1)
                        .From("Users", "U")
                        .Select("Name").Select("Surname")
                        .InnerJoin("P", "Photos", "UserId")
                        .LeftJoin("A", "Attachments", "UserId")
                        .RightJoin("S", "Sales", "UserId")
                        .Where(age > 18)
                    ;

                const string query = @" 
SELECT TOP(1)  Name , Surname
    FROM Users AS U
	INNER JOIN	Photos AS P ON U.UserId = P.Id
	LEFT JOIN	Attachments AS A ON U.UserId = A.Id
	RIGHT JOIN	Sales AS S ON U.UserId = S.Id
    WHERE Age > 18
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_With_Joins_With_Alias()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age", "U");
                var photoUrl = q.Column("Url", "P");
                q.Select
                        .Top(1)
                        .Select(age)
                        .Select(photoUrl, "PhotoUrl")
                        .From("Users", "U")
                        .InnerJoin("P", "Photos", "UserId")
                        .LeftJoin("A", "Attachments", "UserId")
                        .RightJoin("S", "Sales", "UserId")
                        .Where(age > 18)
                        
                    ;

                const string query = @" 
SELECT TOP(1)  U.Age , P.Url as PhotoUrl
    FROM Users AS U
	INNER JOIN	Photos AS P ON U.UserId = P.Id
	LEFT JOIN	Attachments AS A ON U.UserId = A.Id
	RIGHT JOIN	Sales AS S ON U.UserId = S.Id
    WHERE U.Age > 18
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_With_Joins_With_Alias_And_Strong_Typed()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age", "U");
                var photoUrl = q.Column("Url", "P");
                var queryFromBuilder = q.Select
                        .Top(1)
                        .Select(age)
                        .Select(photoUrl, "PhotoUrl")
                        .From<UserTable>("U")
                        .InnerJoin<PhotosTable>("P", "UserId")
                        .LeftJoin<AttachmentsTable>("A", "UserId")
                        .RightJoin<SalesTable>("S","UserId")
                        .Where(age > 18)
                        
                    ;

                const string query = @" 
SELECT TOP(1)  U.Age , P.Url as PhotoUrl
    FROM Users AS U
	INNER JOIN	Photos AS P ON U.UserId = P.Id
	LEFT JOIN	Attachments AS A ON U.UserId = A.Id
	RIGHT JOIN	Sales AS S ON U.UserId = S.Id
    WHERE U.Age > 18
";

                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
    }
}
