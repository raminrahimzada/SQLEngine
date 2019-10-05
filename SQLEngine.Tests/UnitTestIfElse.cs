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
                    .Then("SET @max = 888")
                    .Else("SET @max = 444;");
                const string query = @"
IF(@i < @j)
BEGIN
    SET @max = @j;
END
ELSE IF(@i > @j)
BEGIN
    SET @max = @i;
END
ELSE IF(EXISTS( SELECT  *  FROM Users))
BEGIN
    SET @max = 888
END
ELSE
BEGIN
    SET @max = 444;
END
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }

        [TestMethod]
        public void TestMethod3()
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
                    .ElseIf(x => x.Exists(y => y.From("Users")))
                    .Else("SET @max = 444;");

                const string query = @"
IF(@i < @j)
BEGIN
    SET @max = @j;
END
ELSE IF(@i > @j)
BEGIN
    SET @max = @i;
END
ELSE IF ( EXISTS( SELECT  *  FROM Users) )ELSE
BEGIN
    SET @max = 444;
END
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
        [TestMethod]
        public void TestMethod4()
        {
            using (var t = new IfElseQueryBuilder())
            {
                t.If(x => x.Less("@i", "@j"));
                t.Then("set @MAX = @i");
                t.ElseIf(x => x.Greater("@i", "@j"));
                t.Then("set @MAX = @j");
                t.Else("SET @max = -1");

                const string query = @"
IF(@i < @j)
BEGIN
    set @MAX = @i
END
ELSE IF(@i > @j)
BEGIN
    set @MAX = @j
END
ELSE
BEGIN
    SET @max = -1
END
";
                QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
