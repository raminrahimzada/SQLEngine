using System;
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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_3()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(age > 18)
                    ;

                var queryThat = @"
SELECT TOP(1)  * 
    FROM Users
    WHERE Age > 18
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Order()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                q
                    .Select
                    .Top(1)
                    .From("Users")
                    .Where(age==17)
                    .OrderBy("Id");


                const string queryThat = @"
SELECT TOP(1)   * 
    FROM Users
    WHERE Age = 17
    ORDER BY Id

";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_With_function()
        {
            using (var q = Query.New)
            {
                var name = q.Column("Name");
                q
                    .Select
                    .Top(1)
                    .Select(name)
                    .Select(x => x.Len(name))
                    .Select(x => x.Trim(name))
                    
                    .From("Users")
                    ;


                const string queryThat = @"
SELECT TOP(1)  Name, LEN(Name), TRIM(Name)
    FROM Users

";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Assign()
        {
            using (var q = Query.New)
            {
                //variables
                var myCreatedDate = q.Declare<DateTime>("myCreatedDate");
                
                //columns
                var createdDate = q.Column("CreatedDate");
                var id = q.Column("Id");

                q
                    .Select
                    .Top(1)
                    .SelectAssign(myCreatedDate, createdDate)
                    .From("Users")
                    .Where(id == 17)
                    .OrderBy(id);


                const string queryThat = @"
DECLARE  @myCreatedDate DATETIME ;
SELECT TOP(1)  @myCreatedDate=CreatedDate
    FROM Users
    WHERE Id = 17
    ORDER BY Id
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Between_2()
        {
            using (var q = Query.New)
            {
                var now = DateTime.Parse("2020-07-22");

                //literal sample for example only date example
                var toDate = q.Literal(now, includeTime: false);
                var fromDate = q.Literal(now.AddDays(-1), includeTime: false);

                var age = q.Column("Age");


                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(age.Between(fromDate, toDate))
                    ;

                const string queryThat = @"
SELECT TOP(1)   * 
    FROM Users
    WHERE (Age BETWEEN '2020-07-21' AND '2020-07-22')
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_Like()
        {
            using (var q = Query.New)
            {
                var name = q.Column("Name");
                var surname = q.Column("Surname");

                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(name.Like("J_hn") & surname.Like("Sm_th"))
                    ;

                const string queryThat = @"
SELECT TOP(1)   * 
    FROM Users
    WHERE (Name LIKE N'J_hn') AND (Surname LIKE N'Sm_th')
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_In_With_Select()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");

                q
                    .Select
                    .Top(1)
                    .From<UserTable>()
                    .Where(
                        age.In(
                            s => s
                                .Top(10)
                                .Select("AnotherAge")
                                .From<AnotherUsersTable>()
                                .OrderByDesc("CreateDate")
                        )
                    )
                    ;

                const string queryThat = @"
SELECT TOP(1)   * 
    FROM Users
    WHERE Age IN (
        SELECT TOP(10)  AnotherAge
            FROM AnotherUsers
            ORDER BY CreateDate DESC
    )
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Simple_Filter()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                q
                    .Select
                    .Top(1)
                    .Select("Name")
                    .Select("Surname")
                    .From("Users")
                    .Where(age > 18)
                    ;

                var queryThat = @"
SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE Age > 18
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }

        [TestMethod]
        public void Test_Simple_Select_Complex_Filter_1()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                var height = q.Column("Height");

                q
                    .Select
                    .Top(1)
                    .Select("Name")
                    .Select("Surname")
                    .From("Users")

                    .WhereAnd(age > 18, height < 1.7)
                    //or like that
                    //.Where(age > 18 & height < 1.7)
                    ;

                const string queryThat = @"

SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE (Age > 18) and (Height < 1.7)

";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

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
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Select_With_Joins()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");
                q
                    .Select
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

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_With_Joins_With_Alias()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age", "U");
                var photoUrl = q.Column("Url", "P");
                q
                    .Select
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

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_With_Joins_With_Alias_And_Strong_Typed()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age", "U");
                var photoUrl = q.Column("Url", "P");
                q
                    .Select
                    .Top(1)
                    .Select(age)
                    .Select(photoUrl, "PhotoUrl")
                    .From<UserTable>("U")
                    .InnerJoin<PhotosTable>("P", "UserId")
                    .LeftJoin<AttachmentsTable>("A", "UserId")
                    .RightJoin<SalesTable>("S", "UserId")
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

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Group_1()
        {
            using (var q=Query.New)
            {
                q
                    .Select
                    .Select("Age")
                    //aggregate functions
                    .Select(x => x.Count("Id"))
                    .Select(x => x.Sum("Weight"))
                    .Select(x => x.Count("Name").Distinct())

                    .From("Users")
                    .GroupByDesc("Age")
                    ;

                string query= @"
SELECT Age , COUNT(Id) , SUM(Weight) , COUNT(DISTINCT Name)
    FROM Users
    GROUP BY Age DESC

";
                SqlAssert.AreEqualQuery(q.ToString(), query);

            }
        }
        [TestMethod]
        public void Test_Select_Group_2()
        {
            using (var q=Query.New)
            {
                var all = q.Column("*", "U");
                q
                    .Select
                    .Top(1)
                    .Select("Age")
                    .Select(x => x.Count(all))
                    .From<UserTable>("U")
                    .GroupByDesc("Age")
                    ;

                string query= @"
SELECT TOP(1)  Age , COUNT(U.*)
    FROM Users AS U
    GROUP BY Age DESC
";
                SqlAssert.AreEqualQuery(q.ToString(), query);

            }
        }
        [TestMethod]
        public void Test_Select_Group_3()
        {
            using (var q = Query.New)
            {
                var all = q.Column("*", "U");
                
                //TODO this should be rafactored
                //lack of api 
                var condition = q.RawCondition("count(Age) > 5");

                q
                    .Select
                    .Top(1)
                    .Select("Age")
                    .Select(x => x.Count(all))
                    .From<UserTable>("U")
                    .GroupBy("Age")
                    .Having(condition)
                    .OrderByDesc(x => x.Count("Id"))
                    ;

                string query = @"
SELECT TOP(1)  Age , COUNT(U.*)
    FROM Users AS U
    GROUP BY Age 
    HAVING count(Age) > 5
    ORDER BY COUNT(Id)
";
                SqlAssert.AreEqualQuery(q.ToString(), query);

            }
        }


        [TestMethod]
        public void Test_Simple_Select_Case_When()
        {
            using (var q = Query.New)
            {
                var age = q.Column("Age");

                q
                    .Select
                    .Top(1)
                    .SelectAs(x =>
                            x
                                .When(age <= 18).Then("Teeneger")
                                .When(age > 18).Then("Non-Teeneger")

                        , "AgeStatus"
                    )
                    .From<UserTable>()
                    ;

                const string queryThat = @"
SELECT TOP(1)  
    (CASE WHEN Age <= 18 THEN N'Teeneger'
          WHEN Age > 18  THEN N'Non-Teeneger'
    END) AS AgeStatus
    FROM Users
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
        [TestMethod]
        public void Test_Simple_Select_Case_When_End()
        {
            using (var q = Query.New)
            {
                var gender = q.Column("Gender");

                q
                    .Select
                    .Top(1)
                    .SelectAs(x =>

                            x
                                .When(gender == 1).Then("Male")
                                .When(gender == 2).Then("Female")
                                .Else("LGBT")

                        , "AgeStatus"
                    )
                    .From<UserTable>()
                    ;

                const string queryThat = @"
SELECT TOP(1)  
    (CASE WHEN Gender = 1 THEN N'Male'
          WHEN Gender = 2 THEN N'Female'
            ELSE N'LGBT' 
        END
    ) AS AgeStatus
    FROM Users
";
                SqlAssert.AreEqualQuery(q.ToString(), queryThat);

            }
        }
    }
}
