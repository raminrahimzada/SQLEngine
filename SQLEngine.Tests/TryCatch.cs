using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
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
                SqlAssert.AreEqualQuery(q.ToString(),query);
            }
        }
    }
}