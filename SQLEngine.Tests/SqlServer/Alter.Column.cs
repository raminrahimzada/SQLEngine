
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Alter_Table_AddColumn()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .AddColumn("Age")
                .NotNull()
                .OfType("DECIMAL")
                .Size(18, 4)
                .DefaultValue(18);

            const string query =
                @"
ALTER TABLE Users ADD  Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

            SqlAssert.EqualQuery(q.Build(), query);
        }
    }

    [Fact]
    public void Test_Alter_Table_AddColumn_2()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>()
                .AddColumn("Age")
                .NotNull()
                .OfType("DECIMAL")
                .Size(18, 4)
                .DefaultValue(18);

            const string query =
                @"
ALTER TABLE Users ADD  Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

            SqlAssert.EqualQuery(q.Build(), query);
        }
    }

    [Fact]
    public void Test_Alter_Table_AddColumn_OfType_Strong_Typed()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .AddColumn("Age")
                .NotNull()
                .OfType<decimal>()
                .Size(18, 4)
                .DefaultValue(18)
                ;
            const string query =
                @"
ALTER TABLE Users ADD  Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }

    [Fact]
    public void Test_Alter_Table_AddColumn_OfType_Strong_Typed_2()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>()
                .AddColumn("Age")
                .NotNull()
                .OfType<decimal>()
                .Size(18, 4)
                .DefaultValue(18)
                ;
            const string query =
                @"
ALTER TABLE Users ADD Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_AddColumn_OfType_Strong_Typed_3()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>()
                .AddColumn<decimal>("Age")
                .NotNull()
                .Size(18, 4)
                .DefaultValue(18)
                ;
            const string query =
                @"
ALTER TABLE Users ADD Age  DECIMAL (18,4) NOT  NULL  DEFAULT 18
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_DropColumn()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .DropColumn("Age")
                ;
            const string query =
                @"
ALTER TABLE  Users  DROP  COLUMN  Age 
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_RenameColumn()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .RenameColumn("Age")
                .To("Age_Of_User")
                ;
            const string query =
                @"
EXECUTE sys.sp_rename  @objtype=N'COLUMN'
	,@objname=N'Users.Age'
	,@newname=N'Age_Of_User';
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_AlterColumn()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .AlterColumn("Name")
                .Type("VARCHAR")
                .NotNull()
                .Size(15)
                .DefaultValue("Anonymous")
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Name VARCHAR(15) NOT  NULL  
ALTER TABLE Users ADD CONSTRAINT DF_Users_Name  DEFAULT N'Anonymous' FOR Name 

";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_AlterColumn_3()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .AlterColumn("Name")
                .Type("VARCHAR")
                .NotNull()
                .Size(15)
                .DefaultValue("Anonymous","DF_my_awesome_constraint")
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Name VARCHAR(15) NOT  NULL  
ALTER TABLE Users ADD CONSTRAINT DF_my_awesome_constraint  DEFAULT N'Anonymous' FOR Name 

";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
    [Fact]
    public void Test_Alter_Table_AlterColumn_2()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table("Users")
                .AlterColumn("Name")
                .Type<string>()
                .NotNull()
                .Size(15)
                .DefaultValue("Anonymous")
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Name NVARCHAR(15) NOT  NULL  
ALTER TABLE Users ADD CONSTRAINT DF_Users_Name DEFAULT N'Anonymous' FOR Name 

";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
        
    [Fact]
    public void Test_Alter_Table_AlterColumn_Decimal()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>() //another form you can use
                .AlterColumn("Weight")
                .Type("decimal")
                .NotNull()
                .Size(18, 4)
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NOT NULL 
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }

    [Fact]
    public void Test_Alter_Table_AlterColumn_Decimal_2()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>()
                .AlterColumn("Weight")
                .Type<decimal>()
                .NotNull()
                .Size(18, 4)
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NOT NULL 
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }

    [Fact]
    public void Test_Alter_Table_AlterColumn_Decimal_3()
    {
        using (var q = Query.New)
        {
            q
                .Alter
                .Table<UserTable>()
                .AlterColumn<decimal>("Weight")
                .NotNull()
                .Size(18, 4)
                ;
            const string query =
                @"
ALTER TABLE Users ALTER COLUMN Weight decimal(18,4) NOT NULL 
";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }

}