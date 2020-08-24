using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.PostgreSql;

namespace SQLEngine.Tests.PostgreSql
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Declare_And_Init()
        {
            using (var q = Query.New)
            {
                //C is constants class
                q.Declare("i", C.INT, 1);

                string query = @"
DECLARE  i INT := 1;   
";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Declare_And_Init_2()
        {
            using (var q = Query.New)
            {
                q.Declare<int>("i", 1);
                const string query = @"
DECLARE  i INT  := 1;   
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_Only()
        {
            using (var q = Query.New)
            {
                q.Declare("i","INT");
                const string query = @"
DECLARE  i INT;
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Declare_Only_2()
        {
            using (var q = Query.New)
            {
                q.Declare<int>("i");
                const string query = @"
DECLARE  i INT;
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_And_Set()
        {
            using (var q = Query.New)
            {
                var x = q.Declare<int>("x", 47);

                q.Set(x, 48);
                const string query = @"
declare x int := 47
x := 48
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
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

DECLARE x UUID;
x := '00000000-0000-0000-0000-000000000000';
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_And_Set_With_Function()
        {
            using (var q = Query.New)
            {
                var tableName = q.Declare<string>("tableName");
                var objId = q.Declare<int>("objId");

                q.Set(tableName,"Users");
                

                //Here OBJECT_ID is extension method on SqlEngine.SqlServer 
                //You can write your own custom functions as extensions to use it like that
                //see ICustomFunctionCallExpressionBuilder extensions
                
                q.Set(objId, x => x.ObjectId(tableName));

                q.Print(objId);

                const string query = @"
DECLARE tableName VARCHAR;
DECLARE objId INT;

tableName := 'Users';
objId := tableName::regclass::oid;

RAISE INFO '%',objId;
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Declare_And_Set_With_Cast()
        {
            using (var q = Query.New)
            {
                var now = q.Declare<DateTime>("now");
                var today = q.Declare("today", C.DATE);

                q.Set(now, DateTime.Parse("01/01/2020"));
                
                q.Set(today, x => x.Cast(now, C.DATE));

                q.Print(today);

                const string query = @"

DECLARE now TIMESTAMP;
DECLARE today DATE;

now := '2020-01-01 00:00:00.000';
today := now::DATE;

RAISE INFO '%',today;

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Declare_And_Set_With_DateTime()
        {
            using (var q = Query.New)
            {
                var today = q.Declare<DateTime>("today");

                q.Set(today, DateTime.Parse("01/01/2020"));

                q.Print(today);

                const string query = @"

DECLARE  today TIMESTAMP 
today  := '2020-01-01 00:00:00.000'
RAISE INFO '%',today;

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
    }
}
