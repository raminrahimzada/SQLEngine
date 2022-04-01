using System;
using SQLEngine.SqlServer;
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Simple_Select_1_Expression_1()
    {
        using (var q = Query.New)
        {
            q
                .Select
                .Top(1)
                .From<UserTable>()
                .Where(x => x.IdInteger == 17);
                

            const string queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE IdInteger = 17
";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }

    [Fact]
    public void Test_Simple_Select_Expression_2()
    {
        using (var q = Query.New)
        {
            q
                .Select
                .Top(1)
                .From<UserTable>()
                .Where(x=>x.Name == "admin")
                ;

            var queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE Name = N'admin'
";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }
    [Fact]
    public void Test_Simple_Select_3_Expression()
    {
        using (var q = Query.New)
        {
            q
                .Select
                .Top(1)
                .From<UserTable>()
                .Where(x=>x.Age > 18)
                ;

            var queryThat = @"
        SELECT TOP(1)  * 
            FROM dbo.Users
            WHERE Age > 18
        ";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }
    [Fact]
    public void Test_Simple_Select_Order_Expression()
    {
        using (var q = Query.New)
        {
            q
                .Select
                .Top(1)
                .From<UserTable>()
                .Where(x=>x.Age == 17)
                .OrderBy("Id");


            const string queryThat = @"
        SELECT TOP(1)   * 
            FROM dbo.Users
            WHERE Age = 17
            ORDER BY Id

        ";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }

    //[Fact]
    //public void Test_Simple_Select_Order_Expression_Order()
    //{
    //    using (var q = Query.New)
    //    {
    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(x => x.Age == 17)
    //            .OrderBy(x=>x.Id);


    //        const string queryThat = @"
    //SELECT TOP(1)   * 
    //    FROM Users
    //    WHERE Age = 17
    //    ORDER BY Id

    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}
    [Fact]
    public void Test_Simple_Select_With_function_Expression()
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

                .From<UserTable>()
                ;


            const string queryThat = @"
        SELECT TOP(1)  Name, LEN(Name), TRIM(Name)
            FROM dbo.Users

        ";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }
    [Fact]
    public void Test_Simple_Select_Assign_Expression()
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
                .From<UserTable>()
                .Where(x=>x.IdInteger == 17)
                .OrderBy(id);

            const string queryThat = @"
        DECLARE  @myCreatedDate DATETIME ;
        SELECT TOP(1)  @myCreatedDate=CreatedDate
            FROM dbo.Users
            WHERE IdInteger = 17
            ORDER BY Id
        ";
            SqlAssert.EqualQuery(q.ToString(), queryThat);

        }
    }
    //[Fact]
    //public void Test_Simple_Select_Between_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        var age = q.Column("Age");
    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(x=>x.Age.Between(10,60))
    //            ;

    //        const string queryThat = @"
    //SELECT TOP(1) * 
    //    FROM Users
    //    WHERE (Age BETWEEN 10 AND 60)
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}
    //[Fact]
    //public void Test_Simple_Select_Between_2_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        var now = DateTime.Parse("2020-07-22");

    //        //literal sample for example only date example
    //        var toDate = q.Literal(now, includeTime: false);
    //        var fromDate = q.Literal(now.AddDays(-1), includeTime: false);

    //        var age = q.Column("Age");


    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(x=>x.DateOfBirth.Between(fromDate, toDate))
    //            ;

    //        const string queryThat = @"
    //SELECT TOP(1)   * 
    //    FROM Users
    //    WHERE (Age BETWEEN '2020-07-21' AND '2020-07-22')
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}

    //[Fact]
    //public void Test_Simple_Select_In_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(x=>x.Age.In(11, 22, 33))
    //            ;

    //        const string queryThat = @"
    //SELECT TOP(1) * 
    //    FROM Users
    //    WHERE Age IN (11,22,33)
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}

    //[Fact]
    //public void Test_Simple_Select_Like_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        var name = q.Column("Name");
    //        var surname = q.Column("Surname");

    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(x => x.Name.Like("J_hn") && x.Surname.Like("Sm_th"))
    //            ;

    //        const string queryThat = @"
    //SELECT TOP(1)   * 
    //    FROM Users
    //    WHERE (Name LIKE N'J_hn') AND (Surname LIKE N'Sm_th')
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}

    //[Fact]
    //public void Test_Simple_Select_In_With_Select_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>()
    //            .Where(
    //                x=>x.Age.In(
    //                    s => s
    //                        .Top(10)
    //                        .Select("AnotherAge")
    //                        .From<AnotherUsersTable>()
    //                        .OrderByDesc("CreateDate")
    //                )
    //            )
    //            ;

    //        const string queryThat = @"
    //SELECT TOP(1)   * 
    //    FROM Users
    //    WHERE Age IN (
    //        SELECT TOP(10)  AnotherAge
    //            FROM AnotherUsers
    //            ORDER BY CreateDate DESC
    //    )
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}

    //[Fact]
    //public void Test_Simple_Select_With_Alias_1_Expression()
    //{
    //    using (var q = Query.New)
    //    {
    //        q
    //            .Select
    //            .Top(1)
    //            .From<UserTable>("U")
    //            .Where(x => x.Name == "admin")
    //            ;

    //        var queryThat = @"
    //SELECT TOP(1)   * 
    //    FROM Users AS U
    //    WHERE U.Name = N'admin'
    //";
    //        SqlAssert.EqualQuery(q.ToString(), queryThat);

    //    }
    //}
    //        [Fact]
    //        public void Test_Simple_Select_Simple_Filter()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var age = q.Column("Age");
    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select("Name")
    //                    .Select("Surname")
    //                    .From("Users")
    //                    .Where(age > 18)
    //                    ;

    //                var queryThat = @"
    //SELECT TOP(1)  Name , Surname
    //    FROM Users
    //    WHERE Age > 18
    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }

    //        [Fact]
    //        public void Test_Simple_Select_Complex_Filter_1()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var age = q.Column("Age");
    //                var height = q.Column("Height");

    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select("Name")
    //                    .Select("Surname")
    //                    .From("Users")

    //                    .Where(age > 18 & height < 1.7)
    //                    //or like that
    //                    //.Where(age > 18 & height < 1.7)
    //                    ;

    //                const string queryThat = @"

    //SELECT TOP(1)  Name , Surname
    //    FROM Users
    //    WHERE (Age > 18) and (Height < 1.7)

    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Simple_Select_Complex_Filter_2()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var age = q.Column("Age");
    //                var height = q.Column("Height");
    //                var id = q.Column("Id");

    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select("Name")
    //                    .Select("Surname")
    //                    .From("Users")
    //                    .Where(age > 18 & height <= 1.7 & id != 1)
    //                    ;

    //                const string queryThat = @"

    //SELECT TOP(1)  Name , Surname
    //    FROM Users
    //    WHERE (Age > 18) AND (Height <= 1.7) AND (Id = 1)
    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }

    //        [Fact]
    //        public void Test_Select_Group_1()
    //        {
    //            using (var q=Query.New)
    //            {
    //                q
    //                    .Select
    //                    .Select("Age")
    //                    //aggregate functions
    //                    .Select(x => x.Count("Id"))
    //                    .Select(x => x.Sum("Weight"))
    //                    .Select(x => x.Count("Name").Distinct())

    //                    .From("Users")
    //                    .GroupBy("Age")
    //                    ;

    //                string query= @"
    //SELECT Age , COUNT(Id) , SUM(Weight) , COUNT(DISTINCT Name)
    //    FROM Users
    //    GROUP BY Age

    //";
    //                SqlAssert.EqualQuery(q.ToString(), query);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Select_Group_2()
    //        {
    //            using (var q=Query.New)
    //            {
    //                var all = q.Column("*");
    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select("Age")
    //                    .Select(x => x.Count(all))
    //                    .From<UserTable>("U")
    //                    .GroupBy("Age")
    //                    ;

    //                string query= @"
    //SELECT TOP(1)  Age , COUNT(*)
    //    FROM Users AS U
    //    GROUP BY Age
    //";
    //                SqlAssert.EqualQuery(q.ToString(), query);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Select_Group_3()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var all = q.Column("*");

    //                //TODO this should be rafactored
    //                //lack of api 
    //                var condition = q.RawCondition("count(Age) > 5");

    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select("Age")
    //                    .Select(x => x.Count(all))
    //                    .From<UserTable>("U")
    //                    .GroupBy("Age")
    //                    .Having(condition)
    //                    .OrderByDesc(x => x.Count("Id"))
    //                    ;

    //                string query = @"
    //SELECT TOP(1)  Age , COUNT(*)
    //    FROM Users AS U
    //    GROUP BY Age 
    //    HAVING count(Age) > 5
    //    ORDER BY COUNT(Id)
    //";
    //                SqlAssert.EqualQuery(q.ToString(), query);

    //            }
    //        }


    //        [Fact]
    //        public void Test_Simple_Select_Case_When()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var age = q.Column("Age");

    //                q
    //                    .Select
    //                    .Top(1)
    //                    .SelectAs(x =>
    //                            x
    //                                .When(age <= 18).Then("Teeneger")
    //                                .When(age > 18).Then("Non-Teeneger")

    //                        , "AgeStatus"
    //                    )
    //                    .From<UserTable>()
    //                    ;

    //                const string queryThat = @"
    //SELECT TOP(1)  
    //    (CASE WHEN Age <= 18 THEN N'Teeneger'
    //          WHEN Age > 18  THEN N'Non-Teeneger'
    //    END) AS AgeStatus
    //    FROM Users
    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Simple_Select_Case_When_End()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var gender = q.Column("Gender");

    //                q
    //                    .Select
    //                    .Top(1)
    //                    .SelectAs(x =>

    //                            x
    //                                .When(gender == 1).Then("Male")
    //                                .When(gender == 2).Then("Female")
    //                                .Else("LGBT")

    //                        , "AgeStatus"
    //                    )
    //                    .From<UserTable>()
    //                    ;

    //                const string queryThat = @"
    //SELECT TOP(1)  
    //    (CASE WHEN Gender = 1 THEN N'Male'
    //          WHEN Gender = 2 THEN N'Female'
    //            ELSE N'LGBT' 
    //        END
    //    ) AS AgeStatus
    //    FROM Users
    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Simple_Select_Variable()
    //        {
    //            using (var q = Query.New)
    //            {
    //                var id = q.Declare<int>("id");

    //                q.Select.Select(id);

    //                const string queryThat = @"
    //DECLARE  @id INT;

    //SELECT @id

    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryThat);

    //            }
    //        }
    //        [Fact]
    //        public void Test_Select_Keywords_Order()
    //        {
    //            using (var q=Query.New)
    //            {
    //                var id = q.Column("ID");
    //                var age = q.Column("Age");
    //                q
    //                    .Select
    //                    .Top(1)
    //                    .Select(id)
    //                    .Select(age)
    //                    .Select(x => x.Count(1))
    //                    .SelectLiteral(123)
    //                    .From("table1")
    //                    .GroupBy(id)
    //                    .GroupBy(age)
    //                    .Having(age > id)
    //                    .OrderBy(o => o.Avg(age));
    //                const string queryExpected = @"
    //SELECT TOP(1)  ID , Age , COUNT(1) , 123
    //    FROM table1
    //    GROUP BY ID,Age 
    //    HAVING Age > ID
    //    ORDER BY AVG(Age)

    //";
    //                SqlAssert.EqualQuery(q.ToString(), queryExpected);
    //            }
    //        }
}