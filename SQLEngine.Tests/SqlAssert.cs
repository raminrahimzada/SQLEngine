//Activate and configure connection string below  to check for sql compilation

//#define CHECK_QUERY_COMPILATION


using System.Linq;
using Xunit;


#if CHECK_QUERY_COMPILATION
using System.Data.SqlClient;
using SQLEngine.SqlServer;
#endif
namespace SQLEngine.Tests
{
    public static class SqlAssert
    {
        private static string[] FormatQuery(string query)
        {
            return query
                .Split(SPLITTER.ToCharArray())
                .Select(s => s.ToLowerInvariant())
                .Select(s => s.Trim(SPLITTER.ToCharArray()))
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
        }

        private const string SPLITTER = " \r\n\t;(),.=";

        public static void EqualQuery(string queryActual, string queryExpected)
        {
#if CHECK_QUERY_COMPILATION
            ValidateQueryInServer(queryActual);
            ValidateQueryInServer(queryExpected);
#endif
            var arrActual = FormatQuery(queryActual);
            var arrExpected = FormatQuery(queryExpected);
            Assert.Equal(arrExpected, arrActual);
        }

#if CHECK_QUERY_COMPILATION
        
        //Write Your Sql Server connection string here to test the actual queries in server
        private static string _connectionString;

        public static void ValidateQueryInServer(string sqlQuery)
        {
            if (string.IsNullOrWhiteSpace(sqlQuery)) return;
            using (var b = Query.New)
            {
                switch (b)
                {
                    case SqlServerQueryBuilder _:
                        _connectionString = "Server=MATRIX\\SERVER19;Database=master;Trusted_Connection=True;";
                        ValidateQueryInSqlServer(sqlQuery);
                        break;
                }
            }
        }
        public static void ValidateQueryInSqlServer(string sqlQuery)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
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
            catch (SqlException e)
            {
                //Invalid object name
                //if (e.Class == 16) return;
                //duplicate thing
                //if (e.Class == 11) return;
                Assert.True(false,e.Message);
            }
        }
#endif

    }
}