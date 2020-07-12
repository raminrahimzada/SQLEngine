using System;
using System.Security.Cryptography.X509Certificates;
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
        public void Test_Create_Table()
        {
            //demonstration of create-table query
            using (var b = Query.New)
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
            }
        }

        [TestMethod]
        public void Test_Create_View()
        {
            using (var q = Query.New)
            {
                var isBlocked = q.Column("IsBlocked");

                var viewSelection = q._select
                    .From("Users")
                    .Where(isBlocked == false)
                    .ToString();
                
                var view = q._create
                    .View("View_Active_Users")
                    .As(viewSelection)
                    .ToString();

                const string originalQuery = @"
CREATE VIEW View_Active_Users AS SELECT  * 
    FROM Users
    WHERE IsBlocked = 0
";
                QueryAssert.AreEqual(view, originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Function()
        {
            using (var q = Query.New)
            {
                var function=q._create
                    .Function("max")
                    .Parameter<int>("x")
                    .Parameter<int>("y")
                    .Returns<int>()
                    .Body(f =>
                    {
                        var x = f.Param("x");
                        var y = f.Param("y");
                        
                        f.If(x > y);
                        f.Return(x);
                        f.Else();
                        f.Return(y);


                        f.Comment("Sql Server needs that ;) ");
                        f.Return(0);

                    }).ToString();

                const string originalQuery = @"
CREATE FUNCTION max
(
    @x int , @y int
)
RETURNS int
BEGIN
    IF(@x > @y)
        RETURN(@x)
    ELSE
        RETURN(@y)

    /*Sql Server needs that ;) */ 
    RETURN(0)
END
";
                QueryAssert.AreEqual(function, originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Procedure()
        {
            using (var q = Query.New)
            {
                var procedureQuery = q._create
                    .Procedure("getUserInfo")
                    .Schema("dbo")
                    .Parameter<int>("userId")
                    .Body(f =>
                    {
                        var userId = f.Parameter("userId");//procedure parameter
                        var idColumn = f.Column("Id");//column of table

                        f.Select(select => select
                            .Top(1)
                            .From("Users")
                            .Where(idColumn == userId) //equality condition
                        );

                        f.Return(0);
                    })
                    .ToString();

                const string originalQuery = @"
CREATE PROCEDURE dbo.getUserInfo
(
    @userId INT
)
AS
BEGIN
    SELECT TOP(1)   * 
        FROM Users
        WHERE Id = @userId
    RETURN(0)
END



";
                QueryAssert.AreEqual(procedureQuery, originalQuery);
            }
        }
    }
}
