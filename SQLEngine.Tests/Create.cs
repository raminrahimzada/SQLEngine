using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    [TestClass]
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestInitialize]
        public void Init()
        {
            Query.Setup<SqlServerQueryBuilder>();
        }

        [TestMethod]
        public void TestMethodCreateTable()
        {
            //just for example
            using (var b=Query.New)
            {
                string queryFromBuilder = b._create.Table("Employees")
                    .Columns(c => new[]
                    {
                        c.Long("ID").Identity(),
                        c.Long("UserID")
                            .ForeignKey("USERS", "ID").NotNull(),

                        c.String("Name").MaxLength(50).Unique(),
                        c.Decimal("Weight"),
                        
                        //custom column with all props
                        c.Column("Age").Type("INT").Check("Age>1 && Age<100"),
                        c.Datetime("BirthDate").DefaultValue("GETDATE()"),
                        c.Bool("HasDriverLicense"),
                      

                        // two column index
                        c.Int("Amount1").Unique("IX_Amount1_Amount2",descending:true),
                        c.Int("Amount2").Unique("IX_Amount1_Amount2"),

                        //calculated column
                        c.Column("Sum").CalculatedColumn("Amount1 + Amount2"),
                    }).ToString()
                    ;
                ;
            }
        }
    }
}
