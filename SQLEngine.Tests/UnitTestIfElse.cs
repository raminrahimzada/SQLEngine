using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestIfElse
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var t = QueryBuilderFactory.New)
            {
                t.If(t.Helper.LessThan("@i", "@j"));
                t.Begin();
                t.End();
                t.ElseIf(t.Helper.GreaterThan("@i", "@j"));
                t.Begin();
                t.Set("@max", "@j");
                t.End();
                const string query = @"
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
    SET @max=444;
END
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
