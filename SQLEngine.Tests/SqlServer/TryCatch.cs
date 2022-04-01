
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Simple_Try_Catch()
    {
        using (var q = Query.New)
        {
            q.Try(f =>
                {
                    f.Drop.Table<UserTable>();
                })
                .Catch(e =>
                {
                    e.Print("Error Occured");
                    e.Print(e.ErrorMessage());
                });

            var query = @"
BEGIN TRY
    DROP TABLE Users;
END TRY
BEGIN CATCH
    print(N'Error Occured')
    print(ERROR_MESSAGE())
END CATCH

";
            ;
            SqlAssert.EqualQuery(q.ToString(),query);
        }
    }
}