namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        public void Test_Alter_Table_1()
        {
            using (var t = Query.New)
            {
                var queryThat = t
                        ._create
                        .ToString()
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET NAME = N'Ramin' , SURNAME = N'Rahimzada' , AGE = 18 WHERE (ID = 41)";

                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}