
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Complex_Query_1()
    {
        using var q = Query.New;
        q.Comment("Declaring variables");

        var x = q.Declare<int>("x");
        var y = q.Declare<int>("y");

        var add = q.Declare<int>("add");
        var subtract = q.Declare<int>("subtract");
        var multiply = q.Declare<int>("multiply");
        var divide = q.Declare<int>("divide");


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


        var query = @"

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
        SqlAssert.EqualQuery(q.ToString(), query);
    }
}