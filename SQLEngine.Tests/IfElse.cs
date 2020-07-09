using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void TestMethod_Simple_If_Else_For_Max_Value()
        {
            using (var t = Query.New)
            {
                var i = t.Declare("i", "INT");
                var j = t.Declare("j", "INT");
                var max = t.Declare("max", "INT");
                
                t.If(t.Helper.LessThan(i, j));
                t.Begin();
                t.Set(max, j);
                t.End();
                t.ElseIf(t.Helper.GreaterThan(i, j));
                t.Begin();
                t.Set(max, i);
                t.End();
                t.Else();
                t.Begin();
                t.Set(max, 0.ToSQL());
                t.End();
              
                const string query = @"
declare @i int;
declare @j int;
declare @max int;

IF(@i < @j)
BEGIN
    SET @max = @j;
END

ELSE IF(@i > @j)
BEGIN
    SET @max=@i;
END

ELSE
BEGIN
    SET @max=0;
END
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
