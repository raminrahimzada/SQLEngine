using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Complex_Query_1()
        {
            using (var q=Query.New)
            {
                q.Comment("Declaring variables");

                var x = q.Declare("x", "int");
                var y = q.Declare("y", "int");
                var plus = q.Declare("plus", "int");
                var minus = q.Declare("minus", "int");


                q.Comment("Setting values");
                q.Set(x, 17.ToSQL());
                q.Set(y, 13.ToSQL());
                
                
                q.Comment("doing some calculations");
                q.Set(plus, x + y);
                q.Set(minus, x - y);

                q.Comment("printing the results");

                q.Print(plus);
                q.Print(minus);
                var query = q.ToString();

                var queryOriginal = @"

/*Declaring variables*/
DECLARE  @x int ;
DECLARE  @y int ;
DECLARE  @plus int ;
DECLARE  @minus int ;

/*Setting values*/
SET  @x  = 17;
SET  @y  = 13;

/*doing some calculations*/
SET  @plus  = (@x + @y);
SET  @minus  = (@x - @y);


/*printing the results*/
print(@plus)
print(@minus)



";
                QueryAssert.AreEqual(query, queryOriginal);

            }
        }
    }
}