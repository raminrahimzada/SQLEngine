using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Simple_Select_StrongType_1()
    {
        using var q = Query.New;
        q
            .Select
            .Top(1)
            .From<UserTable>()
            .Where(x => x.Age > 18)
            ;

        var queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE Age > 18
";
        SqlAssert.EqualQuery(q.ToString(), queryThat);
    }

    [Fact]
    public void Test_Simple_Select_StrongType_2()
    {
        const byte age = 18;
        using var q = Query.New;
        q
            .Select
            .Top(1)
            .From<UserTable>()
            .Where(x => x.Age < age)
            ;

        var queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE Age < 18
";
        SqlAssert.EqualQuery(q.ToString(), queryThat);
    }
    
    [Fact]
    public void Test_Simple_Select_StrongType_3()
    {
        const byte age = 18;
        using var q = Query.New;
        q
            .Select
            .Top(1)
            .From<UserTable>()
            .Where(x => x.Age == age*3)
            ;

        var queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE Age = 54
";
        SqlAssert.EqualQuery(q.ToString(), queryThat);
    }
    
    [Fact]
    public void Test_Simple_Select_StrongType_4()
    {
        const int age = 121;
        using var q = Query.New;
        q
            .Select
            .Top(1)
            .From<UserTable>()
            .Where(x => x.IdInteger * x.Weight <= age)
            ;

        const string queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE (IdInteger) * (Weight) <= 121
";
        var actual = q.ToString();
        SqlAssert.EqualQuery(actual, queryThat);
    }
    
    [Fact]
    public void Test_Simple_Select_StrongType_5()
    {
        const int age = 121;
        using var q = Query.New;
        q
            .Select
            .Top(1)
            .From<UserTable>()
            .Where(x => x.IdInteger * 5 + 1 <= age)
            ;

        const string queryThat = @"
SELECT TOP(1)  * 
    FROM dbo.Users
    WHERE (IdInteger) * (5) + 1 <= 121
";
        var actual = q.ToString();
        SqlAssert.EqualQuery(actual, queryThat);
    }
}