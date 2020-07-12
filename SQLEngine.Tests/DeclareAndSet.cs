using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Declare_And_Init()
        {
            using (var q = Query.New)
            {
                q.Declare("i", "INT", 1);
                const string query = @"
DECLARE  @i INT  = 1;   
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Declare_And_Init_2()
        {
            using (var q = Query.New)
            {
                q.Declare("i", 1);
                const string query = @"
DECLARE  @i INT  = 1;   
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_Only()
        {
            using (var q = Query.New)
            {
                q.Declare("i","INT");
                const string query = @"
DECLARE  @i INT;
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Declare_Only_2()
        {
            using (var q = Query.New)
            {
                q.Declare<int>("i");
                const string query = @"
DECLARE  @i INT;
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_And_Set()
        {
            using (var q = Query.New)
            {
                var x = q.Declare("x", 47);

                q.Set(x, 48);
                const string query = @"
declare @x int = 47
SET @x = 48
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_And_Set_Guid()
        {
            using (var q = Query.New)
            {
                var x = q.Declare<Guid>("x");

                q.Set(x, Guid.Empty);
                const string query = @"

declare @x UNIQUEIDENTIFIER

SET @x = '00000000-0000-0000-0000-000000000000'
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }
    }
}
