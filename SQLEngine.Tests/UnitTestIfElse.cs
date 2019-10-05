using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.Builders;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestIfElse
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var t = new IfElseQueryBuilder())
            {
                t.If(x => x.Less("@i", "@j"))
                    .Then("SET @max = @j;")
                    .ElseIf(
                        x => x.Greater("@i", "@j")
                    )
                    .Then("SET @max=@i;")
                    .Else("SET @max=444;");
                var query = @"IF(@i < @j)
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
                Assert.AreEqual(t.Build(), query);
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            using (var t = new IfElseQueryBuilder())
            {
                t.If(
                        x => x.Less("@i", "@j")
                    )
                    .Then("SET @max = @j;")
                    .ElseIf(
                        x => x.Greater("@i", "@j")
                    )
                    .Then("SET @max = @i;")
                    .ElseIf(x => x.Exists(rr => rr.From("Users")))
                    .Else("SET @max = 444;");
                var query = @"IF(@i < @j)
BEGIN
    SET @max = @j;
END
ELSE IF(@i > @j)
BEGIN
    SET @max = @i;
END
ELSE IF ( EXISTS(SELECT * FROM Users) )ELSE
BEGIN
    SET @max = 444;
END
";
                Assert.AreEqual(t.Build(), query);
            }
        }
    }
}
