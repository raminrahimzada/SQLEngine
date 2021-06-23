﻿//Activate and configure connection string below  to check for sql compilation

//#define CHECK_QUERY_COMPILATION


using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if CHECK_QUERY_COMPILATION
using System.Data.SqlClient;
using SQLEngine.PostgreSql;
using SQLEngine.SqlServer;
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
            ValidateQueryInServer(queryActual);
            ValidateQueryInServer(queryExpected);
#endif
            var arrActual = FormatQuery(queryActual);
            var arrExpected = FormatQuery(queryExpected);

            CollectionAssert.AreEqual(arrExpected, arrActual);
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

                    case PostgreSqlQueryBuilder _:
                        _connectionString = "Server=localhost;User Id=postgres;Password=mysupersecurepasswordhere;Database=postgres;";
                        //ValidateQueryInPostgreSql(sqlQuery);
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
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        public static void ValidateQueryInPostgreSql(string sqlQuery)
        {
            try
            {
                using (var connection = new Npgsql.NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();

                    //cmd.CommandText = "SET NOEXEC ON;SET PARSEONLY ON;";
                    //cmd.ExecuteNonQuery();
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