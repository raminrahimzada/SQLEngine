
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_If_Exists_Sub_Case()
    {
        using(var q = Query.New)
        {
            using(q.IfExists(select => select.Top(1).From("users")))
            {
                q.Print("at least one row");
            }

            var queryActual = q.Build();
            ;
            const string queryExpected = @"
IF( EXISTS(SELECT TOP(1)   * 
    FROM users
))
BEGIN
    print(N'at least one row')
END
";

            SqlAssert.EqualQuery(queryActual, queryExpected);
        }
    }
    [Fact]
    public void Test_If_Not_Exists_Sub_Case()
    {
        using(var q = Query.New)
        {
            using(q.IfNotExists(select => select.Top(1).From("users")))
            {
                q.Print("no single row");
            }

            var queryActual = q.Build();
            ;
            const string queryExpected = @"
IF( NOT EXISTS(SELECT TOP(1)   * 
    FROM users
))
BEGIN
    print(N'no single row')
END
";

            SqlAssert.EqualQuery(queryActual, queryExpected);
        }
    }

    [Fact]
    public void Test_Simple_If_Else_For_Max_Value()
    {
        using(var q = Query.New)
        {
            var i = q.Declare<int>("i", 7);
            var j = q.Declare<int>("j", 9);
            var max = q.Declare<int>("max");

            using(q.If(i < j))
            {
                q.Set(max, j);
            }

            q.ElseIf(i > j);
            q.Set(max, i);

            q.Else();
            q.Set(max, 0);

            var queryActual = q.ToString();
            ;
            const string queryExpected = @"
DECLARE @i INT = (7);
DECLARE @j INT = (9);
DECLARE @max INT;
IF(@i < @j)
BEGIN
    SET  @max  = @j;
END
ELSE IF(@i > @j)
SET  @max  = @i;
ELSE SET  @max  = 0;
";
            SqlAssert.EqualQuery(queryActual, queryExpected);
        }
    }

    [Fact]
    public void Test_Simple_If_Else_2()
    {
        using(var q = Query.New)
        {
            var i = q.Declare<int>("i", 7);
            var j = q.Declare<int>("j", 9);

            using(q.If(i == j))
            {
                q.Print("Equal");
            }

            q.Else();
            q.Print("Not-Equal");

            var queryThat = q.ToString();
            ;
            string query = @"
DECLARE @i INT = (7);
DECLARE @j INT = (9);
IF(@i = @j)
BEGIN
    print(N'Equal')
END
ELSE print(N'Not-Equal')
";
            SqlAssert.EqualQuery(queryThat, query);
        }
    }


    [Fact]
    public void Test_If_Disposable_1()
    {
        using(var q = Query.New)
        {
            q.Clear();
            var i = q.Declare<int>("i", 1);

            using(q.If(i <= 0))
            {
                q.Set(i, -i);
            }
            var queryThat = q.Build();
            ;
            string query = @"
DECLARE  @i INT  = (1);
IF(@i <= 0)
BEGIN
    SET  @i  = (0 - @i);
END
";
            SqlAssert.EqualQuery(queryThat, query);
        }
    }

    [Fact]
    public void Test_If_Disposable_2()
    {
        using(var q = Query.New)
        {
            var i = q.Declare<int>("i", 1);

            using(q.If(i <= 0))
            {
                q.Insert.Into<UserTable>().Value("Name", "Tesla");
            }
            var queryThat = q.Build();
            ;
            string query = @"
DECLARE  @i INT  = (1);
IF(@i <= 0)
BEGIN
    INSERT INTO dbo.Users   
        (Name)
    VALUES
        (N'Tesla')
END

";
            SqlAssert.EqualQuery(queryThat, query);
        }
    }

    [Fact]
    public void Test_If_Disposable_Else_1()
    {
        using(var q = Query.New)
        {
            var i = q.Declare<int>("i", 1);

            using(q.If(i <= 0))
            {
                q.Insert.Into<UserTable>().Value("Name", "Tesla");
            }

            using(q.Else2())
            {
                q.Update.Table<UserTable>().Value("Name", "Tesla").WhereColumnEquals("ID", 1);
            }
            var queryThat = q.Build();
            ;
            const string query = @"
DECLARE  @i INT  = (1);
IF(@i <= 0)
BEGIN
    INSERT INTO dbo.Users   
        (Name)
    VALUES
        (N'Tesla')
END
ELSE 
BEGIN
     UPDATE dbo.Users
         SET Name = N'Tesla'
         WHERE (ID=1)
END

";
            SqlAssert.EqualQuery(queryThat, query);
        }
    }
}