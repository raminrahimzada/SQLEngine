using System;

using Xunit;
using C = SQLEngine.SqlServer.C;
using CustomFunctionCallExpressionBuilderExtensions = SQLEngine.SqlServer.CustomFunctionCallExpressionBuilderExtensions;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [Fact]
        public void Test_Declare_And_Init()
        {
            using (var q = Query.New)
            {
                //C is constants class
                q.Declare("i", C.INT, 1);

                string query = @"
DECLARE  @i INT  = 1;   
";

                SqlAssert.EqualQuery(q.ToString(), query);
              
            }
        }
        
        [Fact]
        public void Test_Declare_And_Init_2()
        {
            using (var q = Query.New)
            {
                q.Declare<int>("i", 1);
                const string query = @"
DECLARE  @i INT  = 1;   
";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }

        [Fact]
        public void Test_Declare_Only()
        {
            using (var q = Query.New)
            {
                q.Declare("i","INT");
                const string query = @"
DECLARE  @i INT;
";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }
        
        [Fact]
        public void Test_Declare_Only_2()
        {
            using (var q = Query.New)
            {
                q.Declare<int>("i");
                const string query = @"
DECLARE  @i INT;
";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }

        [Fact]
        public void Test_Declare_And_Set()
        {
            using (var q = Query.New)
            {
                var x = q.Declare<int>("x", 47);

                q.Set(x, 48);
                const string query = @"
declare @x int = 47
SET @x = 48
";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }

        [Fact]
        public void Test_Declare_And_Set_Guid()
        {
            using (var q = Query.New)
            {
                var x = q.Declare<Guid>("x");

                q.Set(x, Guid.Empty);
                const string query = @"

DECLARE  @x UNIQUEIDENTIFIER ;
SET  @x  = '00000000-0000-0000-0000-000000000000';
";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }

        [Fact]
        public void Test_Declare_And_Set_With_Function()
        {
            using (var q = Query.New)
            {
                var tableName = q.Declare<string>("tableName");
                
                q.Set(tableName,"Users");
                
                var objId = q.Declare<int>("objId");

                //Here OBJECT_ID is extension method on SqlEngine.SqlServer 
                //You can write your own custom functions as extensions to use it like that
                //see ICustomFunctionCallExpressionBuilder extensions
                
                q.Set(objId, x => CustomFunctionCallExpressionBuilderExtensions.ObjectId(x, tableName));

                q.Print(objId);

                const string query = @"

DECLARE  @tableName NVARCHAR ;
SET  @tableName  = N'Users';

DECLARE  @objId INT ;
SET  @objId  = OBJECT_ID(@tableName);

print(@objId)

";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }


        [Fact]
        public void Test_Declare_And_Set_With_Cast()
        {
            using (var q = Query.New)
            {
                var today = q.Declare<DateTime>("today");

                q.Set(today, DateTime.Parse("01/01/2020"));
                
                q.Set(today, x => CustomFunctionCallExpressionBuilderExtensions.Cast(x, today, C.DATE));

                q.Print(today);

                const string query = @"

DECLARE  @today DATETIME 
SET  @today  = '2020-01-01 00:00:00.000'
SET  @today  = CAST(@today AS DATE)
print(@today)

";
                SqlAssert.EqualQuery(q.ToString(), query);
            }
        }
    }
}
