using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestInsert
    {
        [TestMethod]
        public void TestMethod_Insert_By_Value()
        {
            using (var q = Query.New)
            {
                q.Insert(i => i.Into("Users")
                    .Value("Name", "Ramin".ToSQL())
                    .Value("Surname", "Rahimzada".ToSQL())
                    .Value("Age", 26.ToSQL())
                );
                const string query = "INSERT INTO Users (Name,Surname,Age) VALUES (N'Ramin' , N'Rahimzada' , 26)";
                
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void TestMethod_Insert_By_Dictionary()
        {
            using (var q = Query.New)
            {
                var dict = new Dictionary<string, string>
                {
                    {"Name", "Ramin".ToSQL()},
                    {"Surname", "Rahimzada".ToSQL()},
                    {"Age", 26.ToSQL()},
                };
                q.Insert(i => i
                    .Into("Users")
                    .Values(dict)
                );

                const string query = "INSERT INTO Users(Name , Surname , Age) VALUES (N'Ramin' , N'Rahimzada' , 26)";
                QueryAssert.AreEqual(q.Build(), query);
            }
        }
       
        [TestMethod]
        public void TestMethod_Insert_By_Select()
        {
            using (var q = Query.New)
            {
                q.Insert(insert =>
                    insert.Into("Users")
                        .Values(
                            select => select.From("Users_Backup")
                        )
                );
                const string query = "INSERT INTO Users  SELECT  *  FROM Users_Backup";
                QueryAssert.AreEqual(q.Build(), query);
            }
        }
    }
}
