using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.Builders;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestDeclarationAndSet
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var t = new DeclarationQueryBuilder())
            {
                t.Declare("i").OfType("INT").Default("1");
                const string query = @"
DECLARE  @i INT  = 1;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            using (var t = new DeclarationQueryBuilder())
            {
                t.Declare("i").OfType("INT");
                const string query = @"
DECLARE  @i INT;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            using (var t = new SetQueryBuilder())
            {
                t.Set("x").To("47");
                const string query = @"
SET @x = 47;
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
