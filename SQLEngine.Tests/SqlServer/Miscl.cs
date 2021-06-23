
using Xunit;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [Fact]
        public void Test_Clear()
        {
            using (var b = Query.New)
            {
                b.Insert.Into("Users");
                b.Clear();
                SqlAssert.EqualQuery(b.Build(), string.Empty);
            }
        }

        [Fact]
        public void Test_Truncate_1()
        {
            using (var b = Query.New)
            {
                b.Truncate("Users");
                SqlAssert.EqualQuery(b.Build(), "truncate table Users");
            }
        }
        
        [Fact]
        public void Test_Truncate_2()
        {
            using (var b = Query.New)
            {
                b.Truncate<UserTable>();
                SqlAssert.EqualQuery(b.Build(), "truncate table Users");
            }
        }
        
        [Fact]
        public void Test_Declare_Unique()
        {
            using (var b = Query.New)
            {
                int counter = 0;
                for (int i = 0; i < 1000; i++)
                {
                    {
                        var id = b.DeclareNew<int>();
                        counter++;
                        Assert.Equal(id.ToSqlString(), $"@v{counter}");
                        SqlAssert.EqualQuery(b.Build(), $"declare {id} int;");
                        b.Clear();
                    }
                    {
                        var id = b.DeclareNew<int>(i);
                        counter++;
                        Assert.Equal(id.ToSqlString(), $"@v{counter}");
                        SqlAssert.EqualQuery(b.Build(), $"declare {id} int={i};");
                        b.Clear();
                    }
                    
                }
            }
        }
    }
}