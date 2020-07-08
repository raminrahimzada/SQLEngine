using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    [TestClass]
    public class UnitTestCreate
    {
        [TestInitialize]
        public void Init()
        {
            QueryBuilderFactory.Setup<SqlServerQueryBuilder>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            using (var t=QueryBuilderFactory.New._create)
            {
                t.Table("Employees")
                    .Columns(c => new[]
                    {
                        c.Long("ID").Identity(),
                        c.Long("UserID")
                            .ForeignKey("USERS", "ID").NotNull(),
                        c.String("Name").MaxLength(50).Unique(),
                        c.Decimal("Weight"),
                        //custom column with all props
                        c.Column("Age").Type("INT"),
                        c.Datetime("BirthDate").DefaultValue("GETDATE()"),
                        c.Bool("HasDriverLicense"),
                      

                        // two column index
                        c.Int("Amount1").Unique("IX_Amount1_Amount2",descending:true),
                        c.Int("Amount2").Unique("IX_Amount1_Amount2"),

                        //calculated column
                        c.Column("Sum").CalculatedColumn("Amount1 + Amount2"),
                    })
                    ;
                var q = t.Build();
                const string query = @"

";
                ;
                var all = SQLKeywords.AllKeywords;

                //QueryAssert.AreEqual(t.Build(), query);
            }
        }
    }
}
