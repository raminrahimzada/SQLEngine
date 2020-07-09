using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void TestMethod_Declare_And_Init()
        {
            using (var t = Query.New)
            {
                t.Declare("i", "INT", 1.ToSQL());
                const string query = @"
DECLARE  @i INT  = 1;
";
                QueryAssert.AreEqual(t.ToString(), query);
            }
        }

        [TestMethod]
        public void TestMethod_Declare_Only()
        {
            using (var t = Query.New)
            {
                t.Declare("i","INT");
                const string query = @"
DECLARE  @i INT;
";
                QueryAssert.AreEqual(t.ToString(), query);
            }
        }

        [TestMethod]
        public void TestMethod_Declare_And_Set()
        {
            using (var t = Query.New)
            {
                var x = t.Declare("x", "INT", 47.ToSQL());

                t.Set(x, 48.ToSQL());
                const string query = @"
declare @x int = 47
SET @x = 48
";
                QueryAssert.AreEqual(t.ToString(), query);
            }
        }
    }
}
