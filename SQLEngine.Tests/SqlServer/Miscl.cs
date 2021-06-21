using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Clear()
        {
            using (var b = Query.New)
            {
                b.Insert.Into("Users");
                b.Clear();
                SqlAssert.AreEqualQuery(b.Build(), string.Empty);
            }
        }

        [TestMethod]
        public void Test_Truncate_1()
        {
            using (var b = Query.New)
            {
                b.Truncate("Users");
                SqlAssert.AreEqualQuery(b.Build(), "truncate table Users");
            }
        }
        
        [TestMethod]
        public void Test_Truncate_2()
        {
            using (var b = Query.New)
            {
                b.Truncate<UserTable>();
                SqlAssert.AreEqualQuery(b.Build(), "truncate table Users");
            }
        }
        
        [TestMethod]
        public void Test_Declare_Unique()
        {
            using (var b = Query.New)
            {
                int counter = 0;
                for (int i = 0; i < 1000; i++)
                {
                    {
                        var id = b.DeclareNew<int>();
                        counter++;
                        Assert.AreEqual(id.ToSqlString(), $"@v{counter}");
                        SqlAssert.AreEqualQuery(b.Build(), $"declare {id} int;");
                        b.Clear();
                    }
                    {
                        var id = b.DeclareNew<int>(i);
                        counter++;
                        Assert.AreEqual(id.ToSqlString(), $"@v{counter}");
                        SqlAssert.AreEqualQuery(b.Build(), $"declare {id} int={i};");
                        b.Clear();
                    }
                    
                }
            }
        }
    }
}