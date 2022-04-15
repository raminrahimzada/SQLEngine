
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Delete_Table_1()
    {
        using var q = Query.New;
        var id = q.Column("Id");

        q
            .Delete
            .Table("Users")
            .Where(id == 111)
            ;

        var query = @"
DELETE from Users WHERE Id = 111
";
        SqlAssert.EqualQuery(q.ToString(), query);
    }
    [Fact]
    public void Test_Delete_Table_2()
    {
        using var b = Query.New;
        var id = b.Column("Id");

        b
            .Delete
            .Table<UserTable>()
            .Where(id == 111)
            ;

        const string query = @"
DELETE from dbo.Users WHERE Id = 111
";
        SqlAssert.EqualQuery(b.ToString(), query);
    }

    [Fact]
    public void Test_Delete_Table_3()
    {
        using var b = Query.New;
        var id = b.Column("Id");

        b.Delete
            .Top(10)
            .Table<UserTable>()
            .Where(id == 111)
            ;

        var query = @"
DELETE TOP(10) from dbo.Users WHERE Id = 111
";
        SqlAssert.EqualQuery(b.ToString(), query);
    }

    [Fact]
    public void Test_Delete_Table_4()
    {
        using var q = Query.New;
        var id = q.Column("Id");
        var isBlocked = q.Column("IsBlocked");

        // query inside that `IN` statement below
        // SELECT UserId FROM Attachments WHERE IsBlocked = 1

        void BlockedUserIdList(ISelectQueryBuilder _) =>
            _.Select("UserId")
                .From("Attachments")
                .Where(isBlocked == true);

        q
            .Delete
            .Top(10)
            .Table<UserTable>()
            .Where(id.In(BlockedUserIdList))
            ;

        var query = @"
DELETE TOP(10)   FROM dbo.Users 
WHERE 
(
    Id  IN (SELECT UserId
        FROM Attachments
        WHERE IsBlocked = 1)
)
";
        SqlAssert.EqualQuery(q.ToString(), query);
    }
}