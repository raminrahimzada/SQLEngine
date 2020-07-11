using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void TestMethod_Simple_If_Else_For_Max_Value()
        {
            using (var q = Query.New)
            {
                var i = q.Declare("i",7);
                var j = q.Declare("j",9);
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
                QueryAssert.AreEqual(queryThat, query);
            }
        }
    }
}
