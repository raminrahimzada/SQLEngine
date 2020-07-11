using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {

        [TestMethod]
        public void Test_Delete_Table_1()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");
                
                var queryThat = b
                    ._delete
                    .Table("Users")
                    .Where(id == 111)
                    .ToString();

                var query = @"
DELETE from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        [TestMethod]
        public void Test_Delete_Table_2()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");

                var queryThat=b
                    ._delete
                    .Table<UserTable>()
                    .Where(id == 111)
                    .ToString();

                const string query = @"
DELETE from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
        
        [TestMethod]
        public void Test_Delete_Table_3()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");

                var queryThat = b._delete
                    .Top(10)
                    .Table<UserTable>()
                    .Where(id == 111)
                    .ToString();

                var query = @"
DELETE TOP(10) from Users WHERE Id = 111
";
                QueryAssert.AreEqual(queryThat, query);
            }
        }

        [TestMethod]
        public void Test_Delete_Table_4()
        {
            using (var b = Query.New)
            {
                var id = b.Column("Id");
                var isBlocked = b.Column("IsBlocked");

                var blockedUserIdList = b
                        ._select
                        .From<AttachmentsTable>()
                        .Select("UserId")
                        .Where(isBlocked == true)
                    ;
                var deleteQuery = b
                    ._delete
                    .Top(10)
                    .Table<UserTable>()
                    .Where(id.In(blockedUserIdList))
                    .ToString();

                var query = @"
DELETE TOP(10)   FROM Users 
WHERE 
(
    Id  IN (SELECT UserId
        FROM Attachments
        WHERE IsBlocked = 1)
)
";
                QueryAssert.AreEqual(deleteQuery, query);
            }
        }
    }
}