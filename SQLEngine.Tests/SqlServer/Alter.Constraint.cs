﻿
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Alter_Table_AddConstraint_Default_Value()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .AddConstraint("DF_For_ProfilePicture")
            .DefaultValue("defaultUser.jpg")
            .ForColumn("ProfilePicture");

        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT DF_For_ProfilePicture DEFAULT N'defaultUser.jpg' FOR ProfilePicture;

";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Primary_Key()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .AddConstraint("PK_Id")
            .PrimaryKey("Id")
            ;
        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT PK_Id PRIMARY KEY (Id);
";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Primary_Key_2()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .AddConstraint("PK_Name_And_Surname")
            .PrimaryKey("Name", "Surname")
            ;
        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT PK_Name_And_Surname PRIMARY KEY (Name,Surname);
";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Foreign_Key()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .AddConstraint("FK_Referral_User")
            .ForeignKey("ReferralUserId")
            .References("Users", null, "Id")
            ;


        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT FK_Referral_User FOREIGN KEY (ReferralUserId) REFERENCES Users(Id);

";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Foreign_Key_2()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .AddConstraint("FK_Referral_User")
            .ForeignKey("ReferralUserId")
            .References<UserTable>("Id")
            ;


        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT FK_Referral_User FOREIGN KEY (ReferralUserId) REFERENCES dbo.Users(Id);
";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Check()
    {
        using var q = Query.New;
        var age = q.Column("Age");
        q
            .Alter
            .Table("Users")
            .AddConstraint("CK_Age_18")
            .Check(age > 18)
            ;


        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT CK_Age_18 CHECK (Age > 18)
";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_AddConstraint_Check_2()
    {
        using var q = Query.New;
        var age = q.Column("Age");
        q
            .Alter
            .Table("Users")
            .AddConstraint("CK_Age_18_100")
            .Check(age > 18 & age < 100)
            ;


        const string query =
            @"
ALTER TABLE Users ADD CONSTRAINT CK_Age_18_100 CHECK (Age > 18 and Age < 100)
";

        SqlAssert.EqualQuery(q.Build(), query);
    }

    [Fact]
    public void Test_Alter_Table_DropConstraint()
    {
        using var q = Query.New;
        q
            .Alter
            .Table("Users")
            .DropConstraint("Constraint1")
            ;


        const string query =
            @"
ALTER TABLE Users DROP CONSTRAINT Constraint1;
";

        SqlAssert.EqualQuery(q.Build(), query);
    }
}