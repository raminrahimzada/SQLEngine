
using SQLEngine.SqlServer;
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Create_Table()
    {
        //demonstration of create-table query
        using var b = Query.New;
        var now = b.Helper.Now;
        var age = b.Column("Age");

        var amount1 = b.Column("Amount1");
        var amount2 = b.Column("Amount2");

        b.Create.Table("Employees")
            .Columns(c => new[]
            {
                c.Long("ID").Identity(),
                c.Long("UserID")
                    .ForeignKey("USERS","dbo", "ID").NotNull(),

                c.String("Name").MaxLength(50).Unique(),
                c.Decimal("Weight"),

                //custom column with all props
                c.Column<byte>("Age").Check(age > 18 & age < 100),
                c.Datetime("BirthDate").DefaultValue(now),
                c.Bool("HasDriverLicense"),


                // two column index
                c.Column<decimal>("Amount1").Unique("IX_Amount1_Amount2", @descending: true),
                c.Decimal("Amount2").Unique("IX_Amount1_Amount2"),

                //calculated column
                c.Column("SumAmount").CalculatedColumn(amount1 + amount2),
            });

        var queryForSqlServer = @"
CREATE TABLE Employees  ( 
    ID BIGINT IDENTITY(1,1) NOT NULL,
    UserID BIGINT  NOT NULL,
    Name NVARCHAR (50) NULL,
    Weight DECIMAL (18,4) NULL,
    Age TINYINT NULL CHECK  ( (Age > 18) AND (Age < 100) )  ,
    BirthDate DATETIME NULL,
    HasDriverLicense BIT NULL,
    Amount1 DECIMAL (18,4) NULL,
    Amount2 DECIMAL (18,4) NULL,
    SumAmount AS (Amount1+Amount2) PERSISTED ,
 ) ;

ALTER TABLE Employees ADD CONSTRAINT PK_Employees_ID PRIMARY KEY CLUSTERED ( ID);
ALTER TABLE Employees ADD CONSTRAINT IX_Employees_Name UNIQUE(Name ASC);
ALTER TABLE Employees ADD CONSTRAINT IX_Amount1_Amount2 UNIQUE(Amount1 DESC , Amount2 ASC);

ALTER TABLE Employees WITH CHECK ADD CONSTRAINT FK_Employees_UserID__USERS_ID FOREIGN KEY (UserID) REFERENCES dbo.USERS( ID );
ALTER TABLE Employees CHECK CONSTRAINT FK_Employees_UserID__USERS_ID ;

ALTER TABLE Employees ADD CONSTRAINT DF_EmployeesBirthDate DEFAULT(GETDATE()) FOR BirthDate;

";
        ;
        Query.Setup<SqlServerQueryBuilder>();
        var actual = b.Build();
        SqlAssert.EqualQuery(actual, queryForSqlServer);
    }

    [Fact]
    public void Test_Create_View()
    {
        using var q = Query.New;
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
        SqlAssert.EqualQuery(q.ToString(), originalQuery);
    }

    [Fact]
    public void Test_Create_View_2()
    {
        using var q = Query.New;
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
    FROM dbo.Users
    WHERE IsBlocked = 0


";
        SqlAssert.EqualQuery(q.ToString(), originalQuery);
    }

    [Fact]
    public void Test_Create_Function()
    {
        using var q = Query.New;
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

                using(f.If(x > y))
                {
                    f.Return(x);
                }
                f.Else();
                f.Return(y);

                f.Comment("Sql Server needs that ;) ");
                f.Return(0);

            });

        const string originalQuery = @"
CREATE FUNCTION max
(
    @x INT , @y INT
)
RETURNS INT
BEGIN
    IF(@x > @y)
    BEGIN
        RETURN @x 
    END
    ELSE RETURN @y 
    /*Sql Server needs that ;) */ 
    RETURN 0 
END

";
        var queryActual = q.Build();
        SqlAssert.EqualQuery(queryActual, originalQuery);
    }

    [Fact]
    public void Test_Create_Function_Sum()
    {
        using var q = Query.New;
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
        SqlAssert.EqualQuery(q.ToString(), originalQuery);
    }

    [Fact]
    public void Test_Create_Procedure()
    {
        using var q = Query.New;
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
        SqlAssert.EqualQuery(q.ToString(), originalQuery);
    }

    [Fact]
    public void Test_Create_Index()
    {
        using var q = Query.New;
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

        SqlAssert.EqualQuery(q.ToString(), query);
    }

    [Fact]
    public void Test_Create_Database()
    {
        using var q = Query.New;
        q
            .Create
            .Database("FacebookDB")
            ;
        const string query =
            @"
                CREATE DATABASE FacebookDB
                ";

        SqlAssert.EqualQuery(q.ToString(), query);
    }

    [Fact]
    public void Test_Create_Trigger()
    {
        using var q = Query.New;
        q.Create
            .Trigger("Trigger_Test")
            .ForDelete()
            .On("Users", "dbo")
            .Body(x =>
            {
                x.Print("Trigger_Test Executed!");
            })
            ;
        const string query =
            @"
                CREATE TRIGGER Trigger_Test ON dbo.Users FOR DELETE         
                    AS 
                print(N'Trigger_Test Executed!')
                ";
        ;
        SqlAssert.EqualQuery(q.Build(), query);
    }
}