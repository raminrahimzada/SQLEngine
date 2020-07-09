# SQLEngine
SQL Engine is a library that eases of generating sql queries
<br/>You can build (almost) any sql query with C# expressions 
<br/>One of the examples :
```sql
            //demonstration of create-table query
            using (var b = Query.New)
            {
                string sqlQuery = b._create.Table("Employees")
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
                    }).ToString();
                    
                    //then execute it                    
                    ExecuteRawSql(sqlQuery);
            }
```
You can see other examples(insert,update,delete,drop,if-else,truncate alter and so on.) in <a href="https://github.com/raminrahimzada/SQLEngine/tree/master/SQLEngine.Tests">SqlServer.Tests</a>
