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
                
                var add = q.Declare("add", "int");
                var subtract = q.Declare("subtract", "int");
                var multiply = q.Declare("multiply", "int");
                var divide = q.Declare("divide", "int");


                q.Comment("Setting values");
                q.Set(x, 17);
                q.Set(y, 13);
                
                
                q.Comment("doing some calculations");
                q.Set(add, x + y);
                q.Set(subtract, x - y);
                q.Set(multiply, x * y);
                q.Set(divide, x / y);

                q.Comment("printing the results");

                q.Print(add);
                q.Print(subtract);
                q.Print(multiply);
                q.Print(divide);
                var query = q.ToString();

                var queryOriginal = @"

/*Declaring variables*/ 
DECLARE  @x int ;
DECLARE  @y int ;
DECLARE  @add int ;
DECLARE  @subtract int ;
DECLARE  @multiply int ;
DECLARE  @divide int ;

/*Setting values*/ 
SET  @x  = 17;
SET  @y  = 13;

/*doing some calculations*/ 
SET  @add  = (@x + @y);
SET  @subtract  = (@x - @y);
SET  @multiply  = (@x * @y);
SET  @divide  = (@x / @y);

/*printing the results*/ 
print(@add)
print(@subtract)
print(@multiply)
print(@divide)



";
                QueryAssert.AreEqual(query, queryOriginal);

            }
        }
    }
}