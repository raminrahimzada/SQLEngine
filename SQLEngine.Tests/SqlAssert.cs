#define CHECK_QUERY_COMPILATION

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if CHECK_QUERY_COMPILATION
using System.Data.SqlClient;
#endif

namespace SQLEngine.Tests
{
    public static class SqlAssert
    {
        public static void AreEqualQuery(string queryActual, string queryExpected)
        {
            const string splitter = " \r\n\t;(),.=";
            string[] FormatQuery(string query)
            {
                return query
                    .Split(splitter.ToCharArray())
                    .Select(s=>s.ToLowerInvariant())
                    .Select(s => s.Trim(splitter.ToCharArray()))
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();
            }
#if CHECK_QUERY_COMPILATION
            ValidateQueryInSqlServer(queryActual);
            ValidateQueryInSqlServer(queryExpected);
#endif
            var arrActual = FormatQuery(queryActual);
            var arrExpected = FormatQuery(queryExpected);

            CollectionAssert.AreEqual(arrExpected, arrActual);
        }

#if CHECK_QUERY_COMPILATION

        private const string
            ConnectionString =
                "Server=.\\SERVER17;Database=SqlEngineTest;Trusted_Connection=True;";

        public static void ValidateQueryInSqlServer(string sqlQuery)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    
                    //https://docs.microsoft.com/en-us/sql/t-sql/statements/set-noexec-transact-sql?redirectedfrom=MSDN&view=sql-server-ver15
                    //https://docs.microsoft.com/en-us/sql/t-sql/statements/set-parseonly-transact-sql?view=sql-server-ver15
                    cmd.CommandText = "SET NOEXEC ON;SET PARSEONLY ON;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = sqlQuery;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
#endif

    }
}