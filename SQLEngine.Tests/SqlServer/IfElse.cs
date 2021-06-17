﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Simple_If_Else_For_Max_Value()
        {
            using (var q = Query.New)
            {
                var i = q.Declare<int>("i",7);
                var j = q.Declare<int>("j",9);
                var max = q.Declare<int>("max");

                q.If(i < j);
                q.Set(max, j);
                
                q.ElseIf(i > j);
                q.Set(max, i);
                
                q.Else();
                q.Set(max, 0);

                var queryThat = q.ToString();
                ;
                const string query = @"
DECLARE  @i INT =7;
DECLARE  @j INT =9;
DECLARE  @max INT ;

IF(@i < @j)
    SET  @max  = @j;
ELSE IF(@i > @j)
    SET  @max  = @i;
ELSE
    SET  @max  = 0;

";
                SqlAssert.AreEqualQuery(queryThat, query);
            }
        }
        
        [TestMethod]
        public void Test_Simple_If_Else_2()
        {
            using (var q = Query.New)
            {
                var i = q.Declare<int>("i",7);
                var j = q.Declare<int>("j",9);

                q.If(i == j);
                q.Print("Equal");
                q.Else();
                q.Print("Not-Equal");

                var queryThat = q.ToString();
                ;
                string query = @"
DECLARE  @i INT  = (7);
DECLARE  @j INT  = (9);

IF(@i = @j)
    print(N'Equal')
ELSE
    print(N'Not-Equal')



";
                SqlAssert.AreEqualQuery(queryThat, query);
            }
        }
    }
}