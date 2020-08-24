using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.PostgreSql;

namespace SQLEngine.Tests.PostgreSql
{
    [TestClass]
    public partial class AllTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Query.Setup<PostgreSqlQueryBuilder>();
            
            var _connectionString = "Server=localhost;User Id=postgres;Password=mysupersecurepasswordhere;Database=postgres;";
            using (var connection = new Npgsql.NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                //cmd.CommandText = "SET NOEXEC ON;SET PARSEONLY ON;";
                //cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT uuid_generate_v4();";
                var guid=cmd.ExecuteScalar();
                Assert.IsInstanceOfType(guid,typeof(Guid));
                cmd.Dispose();
                ;
            }
        }
    }
}