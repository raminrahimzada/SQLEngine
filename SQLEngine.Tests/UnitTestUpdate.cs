using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.Builders;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestUpdate
    {
        [TestMethod]
        public void TestMethod5()
        {
            using (var t = new UpdateQueryBuilder())
            {
                var p = new
                {
                    ID=47,
                    Name = "Ramin",
                    Surname = "Rahimzada",
                    Age = 1,
                };
                t.Table("Users")
                    .Top(5)
                    .Value("NAME", p.Name.ToSQL())
                    .Value("SURNAME", p.Surname.ToSQL())
                    .Value("AGE", p.Age.ToSQL())
                    .Where(a => a.Equal("ID", p.ID.ToSQL()))
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET NAME = N'Ramin' , SURNAME = N'Rahimzada' , AGE = 1 WHERE (ID = 47)";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
        [TestMethod]
        public void TestMethod6()
        {
            using (var t = new UpdateQueryBuilder())
            {
                var dictUpdate = new Dictionary<string, string>
                {
                    { "COUNT", 47.ToSQL() }
                };

                t.Table("Users")
                    .Top(5)
                    .Values(dictUpdate)
                    .Where(a => a.Equal("ID", "1"))
                    ;
                const string query =
                    "UPDATE TOP(5)  Users SET COUNT = 47 WHERE (ID = 1)";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
