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
                b.Create.Table("Employees")
                    .Columns(c => new[]
                    {
                        c.Long("ID").Identity(),
                        c.Long("UserID")
                            .ForeignKey("USERS", "ID").NotNull(),

                        c.String("Name").MaxLength(50).Unique(),
                        c.Decimal("Weight"),

                        //custom column with all props
                        c.Column("Age").Type("INT").Check("Age>1 && Age<100"),
                        c.Datetime("BirthDate").DefaultValueRaw("GETDATE()"),
                        c.Bool("HasDriverLicense"),


                        // two column index
                        c.Column<decimal>("Amount1").Unique("IX_Amount1_Amount2", descending: true),
                        c.Decimal("Amount2").Unique("IX_Amount1_Amount2"),

                        //calculated column
                        c.Column("Sum").CalculatedColumn("Amount1 + Amount2"),
                    });

                var query = b.ToString();
            }
        }

        [TestMethod]
        public void Test_Create_View()
        {
            using (var q = Query.New)
            {
                var isBlocked = q.Column("IsBlocked");
                q
                    .Create
                    .View("View_Active_Users")
                    .As(
                        s => s
                            .From("Users")
                            .Where(isBlocked == false)
                    );

                const string originalQuery = @"
CREATE VIEW View_Active_Users AS SELECT  * 
    FROM Users
    WHERE IsBlocked = 0
";
                SqlAssert.AreEqualQuery(q.ToString(), originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_View_2()
        {
            using (var q = Query.New)
            {
                var isBlocked = q.Column("IsBlocked");
                q
                    .Create
                    .View("View_Active_Users")
                    .As(
                        s => s
                            .From<UserTable>()
                            .Where(isBlocked == false)
                    );

                const string originalQuery = @"
CREATE VIEW View_Active_Users AS SELECT  * 
    FROM Users
    WHERE IsBlocked = 0
";
                SqlAssert.AreEqualQuery(q.ToString(), originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Function()
        {
            using (var q = Query.New)
            {
                q
                    .Create
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

                    });

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
                SqlAssert.AreEqualQuery(q.ToString(), originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Function_Sum()
        {
            using (var q = Query.New)
            {
                q
                    .Create
                    .Function("sum")
                    .Parameter<int>("x")
                    .Parameter<int>("y")
                    .Returns<int>()
                    .Body(f =>
                    {
                        var x = f.Param("x");
                        var y = f.Param("y");
                        
                        f.Comment("Adding numbers here");

                        f.Return(x + y);
                    });

                const string originalQuery = @"
CREATE FUNCTION sum
(
@x INT , @y INT
)
RETURNS INT
BEGIN
    /*Adding numbers here*/ 
    RETURN (@x + @y)
END
";
                SqlAssert.AreEqualQuery(q.ToString(), originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Procedure()
        {
            using (var q = Query.New)
            {
                q.Create
                    .Procedure("getUserInfo")
                    .Schema("dbo")
                    .Parameter<int>("userId")
                    .Body(f =>
                    {
                        var userId = f.Parameter("userId"); //procedure parameter
                        var idColumn = f.Column("Id"); //column of table

                        f.Select
                            .Top(1)
                            .From("Users")
                            .Where(idColumn == userId); //equality condition

                        f.Return(0);
                    });

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
                SqlAssert.AreEqualQuery(q.ToString(), originalQuery);
            }
        }

        [TestMethod]
        public void Test_Create_Index()
        {
            using (var q = Query.New)
            {
                q
                        .Create
                        .Index("IX_Unique_Email")
                        .OnTable("Users")
                        .Columns("Email")
                        .Unique()
                    ;
                const string query =
                    @"
                CREATE UNIQUE  INDEX IX_Unique_Email ON Users ( Email ) 
                ";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        
        [TestMethod]
        public void Test_Create_Database()
        {
            using (var q = Query.New)
            {
                q
                        .Create
                        .Database("FacebookDB")
                    ;
                const string query =
                    @"
                CREATE DATABASE FacebookDB
                ";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
    }
}
