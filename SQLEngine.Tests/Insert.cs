using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Insert_By_Value()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into("Users")
                    .Value("Name", "Ramin")
                    .Value("Surname", "Rahimzada")
                    .Value("Age", 26)
                    .Value("Height", 1.84)
                    ;
                const string query =
                    "INSERT INTO Users (Name,Surname,Age,Height) VALUES (N'Ramin' , N'Rahimzada' , 26, 1.84)";
                
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Dictionary()
        {
            using (var q = Query.New)
            {
                var dict = new Dictionary<string,SqlServerLiteral>
                {
                    {"Name", "Ramin"},
                    {"Surname", "Rahimzada"},
                    {"Age", 26},
                };
                q.Insert
                    .Into("Users")
                    .Values(dict)
                    ;

                const string query = "INSERT INTO Users(Name , Surname , Age) VALUES (N'Ramin' , N'Rahimzada' , 26)";
                QueryAssert.AreEqual(q.Build(), query);
            }
        }
       
        [TestMethod]
        public void Test_Insert_By_Select()
        {
            using (var q = Query.New)
            {
                q
                    .Insert
                    .Into("Users")
                    .Values(
                        select => select.From("Users_Backup")
                    )
                    ;
                const string query = "INSERT INTO Users  SELECT  *  FROM Users_Backup";
                QueryAssert.AreEqual(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Select_Strong_Typed()
        {
            using (var q = Query.New)
            {
                var queryThat = q.Insert
                    .Into<UserTable>()
                    .Values(select =>
                        select
                            .From<AnotherUsersTable>()
                    ).ToString();
               
                const string query = "INSERT INTO Users  SELECT  *  FROM AnotherUsers";
                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}
