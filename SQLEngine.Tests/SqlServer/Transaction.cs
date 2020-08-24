using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Transaction()
        {
            using (var q=Query.New)
            {
                q.BeginTransaction();
                
                q.Delete.Table<UserTable>();

                q.CommitTransaction();


                var query = @"
BEGIN TRANSACTION
 DELETE  FROM Users
COMMIT TRANSACTION

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Transaction_With_Names()
        {
            using (var q = Query.New)
            {
                q.BeginTransaction("outerTran");
                q.BeginTransaction("innerTran");

                q.Delete.Table<UserTable>();
                
                q.CommitTransaction("innerTran");
                q.CommitTransaction("outerTran");


                var query = @"
BEGIN TRANSACTION outerTran
    BEGIN TRANSACTION innerTran
        DELETE  FROM Users
    COMMIT TRANSACTION innerTran
COMMIT TRANSACTION outerTran

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Transaction_With_Try()
        {
            using (var q = Query.New)
            {
                q.Try(t =>
                {
                    t.BeginTransaction();
                    t.Delete.Table<UserTable>();
                    t.Execute
                        .Procedure("SendMoney")
                        .Schema("dbo")//this is optional
                        .Arg("from","Alice")
                        .Arg("to","Bob")
                        .Arg("amount",25.0M)
                        ;
                    t.CommitTransaction();
                }).Catch(c =>
                {
                    c.Print(c.ErrorMessage());
                    c.Print("Failed, rolling back...");
                    c.RollbackTransaction();
                });


                var query = @"
BEGIN TRY
    BEGIN TRANSACTION
        DELETE  FROM Users
        EXECUTE dbo.SendMoney  @from=N'Alice'
	        ,@to=N'Bob'
	        ,@amount=25.0;
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    print(ERROR_MESSAGE())
    print(N'Failed, rolling back...')
    ROLLBACK TRANSACTION
END CATCH
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
    }
}