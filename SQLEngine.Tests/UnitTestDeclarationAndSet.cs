using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestDeclarationAndSet
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var t = QueryBuilderFactory.New)
            {
                t.Declare("i", "INT", 1.ToSQL());
                
                const string query = @"
DECLARE  @i INT  = 1;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            using (var t = QueryBuilderFactory.New)
            {
                t.Declare("i","INT");
                const string query = @"
DECLARE  @i INT;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            using (var t = QueryBuilderFactory.New)
            {
                t.Set("x", 47.ToSQL());
                const string query = @"
SET @x = 47;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
