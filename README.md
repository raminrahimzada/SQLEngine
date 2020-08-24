<img src="https://github.com/raminrahimzada/SQLEngine/blob/master/logo.png?raw=true"/> 

# SQL Engine is a query abstraction layer on relational sql databases - MS-SQL, Oracle, MySQL etc..
<br/>Library eases of generating cross-dialect sql queries without using any dialect specific syntax
<br/>You can generate any complex sql statements using Engine expressions then configure it to build  queries in a specific dialect


| | |
| --- | --- |
| **Build** | [![Build status](https://ci.appveyor.com/api/projects/status/r75p0yn5uo6colgk?svg=true&branch=master)](https://ci.appveyor.com/project/raminrahimzada/SQLEngine) |
| **Quality** | [![SonarCloud](https://sonarcloud.io/api/project_badges/measure?project=raminrahimzada_SQLEngine&metric=alert_status)](https://sonarcloud.io/dashboard?id=SQLEngine) [![GitHub issues](https://img.shields.io/github/issues-raw/raminrahimzada/SQLEngine.svg)](https://github.com/raminrahimzada/SQLEngine/issues) | 
| **Nuget** | [![Nuget](https://buildstats.info/nuget/SQLEngine)](http://nuget.org/packages/SQLEngine) |

# Database Providers:
| | |
| --- | --- |
|SqlEngine.SqlServer| [![Nuget](https://buildstats.info/nuget/SQLEngine.SQLServer)](http://nuget.org/packages/SQLEngine.SQLServer)
|SqlEngine.Oracle| ... |
|SqlEngine.MySql| ... | 
|SqlEngine.PostgreSql| ... |
|SqlEngine.Sqlite| ... | 
 

## Configuration 
```cs
Query.Setup<SqlServerQueryBuilder>();

//in development
//Query.Setup<PostgreSqlQueryBuilder>(); 

//future plans
//Query.Setup<MySqlQueryBuilder>(); 
//Query.Setup<OracleQueryBuilder>(); 
//Query.Setup<SqliteQueryBuilder>(); 
```
<br/>Usage :

## Demonstration of simple select-where query
```sql            
using (var q = Query.New)
{
    var id = q.Column("Id");

    q
        .Select
        .Top(1)
        .From("Users")
        .Where(id==11)
        .OrderBy(id);
 
     var query = q.ToString();
}


//or using strong typed tables
using (var q = Query.New)
{
    var id = q.Column("Id");

    q
        .Select
        .Top(1)
        .From<UserTable>()
        .Where(id==11)
	.OrderBy(id);

     var query = q.ToString();
}
```
Above 2 queries will be like that:
```sql
SELECT TOP(1)  *
    FROM Users
    WHERE Id = 11
    ORDER BY Id
```

## Demonstration of another select-where query
```sql            
using (var q = Query.New)
{
    var age = q.Column("Age");
    var height = q.Column("Height");

    q
        .Select
        .Top(1)
        .Selector("Name")
        .Selector("Surname")
        .From("Users")
        .Where(age > 18 & height <= 1.7)
        ;
    
    var query = q.ToString();
}
```
Above query will be like that:
```sql
SELECT TOP(1)  Name , Surname
    FROM Users
    WHERE (Age > 18) and (Height <= 1.7)
```
## Demostration of left-right-inner joins
```cs
var age = t.Column("Age");
    t
	    .Select
            .Top(1)
            .From("Users", "U")
            .Selector("Name")
            .Selector("Surname")
            .InnerJoin("P", "Photos", "UserId")
            .LeftJoin("A", "Attachments", "UserId")
            .RightJoin("S", "Sales", "UserId")
            .Where(age > 18)            
        ;
	
  var query = q.ToString();
```        
And the result will be
```sql
SELECT TOP(1)  Name , Surname
    FROM Users AS U
	INNER JOIN	Photos AS P ON P.UserId = U.Id
	LEFT JOIN	Attachments AS A ON A.UserId = U.Id
	RIGHT JOIN	Sales AS S ON S.UserId = U.Id
    WHERE Age > 18
```    


## Demonstration of create-table query
```sql            
using (var b = Query.New)
{
    b
        .Create
        .Table("Employees")
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
        });
        
        var query = q.ToString();	
}
```

## You can see other examples(insert,update,delete,drop,if-else,truncate alter and so on.) in <a href="https://github.com/raminrahimzada/SQLEngine/tree/master/SQLEngine.Tests">SqlServer.Tests</a>




