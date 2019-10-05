using System;
using System.Collections.Generic;
using SQLEngine.Builders;
using SQLEngine.Helpers;

namespace SQLEngine
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            {
                using (var t=new IfElseQueryBuilder())
                {
                    t.If(
                            x => x.Less("@i", "@j")
                        )
                        .Then("SET @max = @j;")
                        .ElseIf(
                            x => x.Greater("@i", "@j")
                        )
                        .Then("SET @max = @i;")
                        .ElseIf(x => x.Exists(rr => rr.From("Users")))
                        .Then("SET @max = 888")
                        .Else("SET @max = 444;");
                    Console.WriteLine(t.Build());
                    ;
                }
            }
            QueryBuilder q = new QueryBuilder();
            //deyishenler
            q.Declare("i", "INT");
            q.Declare("j", "INT", "NULL");
            q.Set("i", "1");
            //=========================================================
            //yeke join sorgusu
            //select P.* , U.Id , U.Name as Username from Users as  U LEFT JOIN Photos P on P.UserId=U.Id where P.Id=1
            q.Select(s =>
                s.From("U", "Users")
                    .LeftJoin("P", "Photos", "P.UserId", "U.Id")
                    .Selector("P.*")
                    .Selector("U.Id")
                    .Selector("U.Name", "Username")
                    .Where("P.Id=1"));
            //select P.* , U.Id , U.Name as Username from Users as  U LEFT JOIN Photos P on P.UserId=U.Id where EXISTS(select top(1) * from AdminUsers AU where AU.Id=P.Id)

            q.Select(s =>
                s.From("U", "Users")
                    .LeftJoin("P", "Photos", "P.UserIdId", "U.Id")
                    .Selector("P.*")
                    .Selector("U.Id")
                    .Selector("U.Name", "Username")
                    .Where(x => x
                        .Exists(t => t
                            .From("AU", "AdminUsers")
                            .Where(
                                q.Equal("AU.Id", "P.Id")
                            )
                            .Top(1)
                        )
                    )
            );

            //union sorgusu
            //select * from A union select * from B_table
            //q.Select(t1 =>
            //            t1.From("A_table")
            //        .Union(t2 =>
            //            t2.From("B_table")
            //        )
            //);
            //=========================================================
            //insertler
            //sutunlu insert
            // insert into Users(NAME) values(Ramin)
            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"NAME", "'Ramin'"}
            };
            q.Insert(x => x.Into("Users").Values(dict));
            //sutunsuz insert
            // insert into Users values('Ramin',17)
            List<string> list = new List<string>
            {
                "'Ramin'","17"
            };
            q.Insert(x => x.Into("Users").Values(list.ToArray()));
            //select-den insert
            //insert into Users(Name,Surname) select Name,Surname from Users_Backup
            q.Insert(x => x
                .Into("Users")
                .Columns("Name", "Surname")
                .Values(y => y
                    .From("Users_Backup")
                    .Selector("Name")
                    .Selector("Surname")
                )
            );
            //condition
            string condition = q.Equal("ID", "1");
            //=========================================================
            //update Users set NAME=N'Ramin' where ID=1
            Dictionary<string, string> updateDict = new Dictionary<string, string>
            {
                {"NAME", "'Ramin'"}
            };
            q.Update(i => i.Table("Users").Values(updateDict).Where(condition));

            //=========================================================
            //ozumuzun condition
            //bir defeye
            q.IfElse(s => s.If(condition).Then(x => x.FromRawQuery("zrt prt")).Else("bla bla"));
            //hisse hisse
            q.If(condition);
            q.Begin();
            //bla bla bla
            q.End();

            q.ElseIf(condition);
            q.Begin();
            //bla bla bla
            q.End();

            //murekkeb if-ler

            // @i>1
            q.If(i => i.Firstly("@i>1"));

            // @i>1 and @i<10
            q.If(i => i.Firstly("@i>1").And("@i<10"));

            // @i>1 or @i<10
            q.If(i => i.Firstly("@i>1").Or("@i<10"));

            // i>10 and (@i<15 or @i>12)
            q.If(i => i.Firstly("@i>10").And(j => j.Firstly("@i<15").Or("@i>12")));

            //if (@id<>0 AND EXISTS(SELECT TOP(1) 1 FROM TABLE_NAME WHERE ID=@id))
            q.If(i => i.Firstly("@id<>0")
                .And(j => ((AbstractConditionBuilder) j).Exists(s => s
                        .From("TABLE_NAME")
                        .Where(t => t
                            .Equal("ID", "@id")
                        )
                        .Top(1)
                    )
                )
            );

            //=========================================================
            //execute sp with parameter order
            q.Execute(p => p
                .Procedure("SP_SAVE")
                .Arg0("ramin")
                .Arg1("test")
                .Arg2("123")
            );

            //execute sp with parameter named
            q.Execute(p => p
                .Procedure("SP_SAVE")
                .Arg("NAME", "Ramin")
                .Arg("SUR_NAME", "Rahimzada")
                .Arg("AGE", "25")
            );


            //hisse hisse
            //Main2(args);
            //return;
            //var builder = new SqlBuilder();

            //var permissionTable = new SqlTable("PERMISSIONS");
            //var permissionValueTable = new SqlTable("USER_PERMISSIONS");

            //var filterConditionPermissionId = SqlCondition.Equal(permissionTable.Column("CODE"), SqlLiteral.String("ANBAR_IDARE"));
            //var variablePermissionId = SqlVariable.New("INT");
            //builder.Add(variablePermissionId);

            //var selectionPermissionId = SqlSelection.IntoVariable(permissionTable.Column("ID"), variablePermissionId);
            //var permissionIdSelection = new SqlSelectQuery(permissionTable, filterConditionPermissionId, selectionPermissionId);

            //builder.Add(permissionIdSelection);

            //var variablePermissionValueId = SqlVariable.New("INT");
            //builder.Add(variablePermissionValueId);

            //var selectionPermissionValueId = SqlSelection.IntoVariable(permissionValueTable.Column("ID"), variablePermissionValueId);

            //var condition1 = SqlCondition.Equal(permissionValueTable.Column("PERMISSION_ID"), variablePermissionId);
            //var condition2 = SqlCondition.Equal(permissionValueTable.Column("USER_ID"), SqlLiteral.Integer(1));
            //var condition3 = SqlCondition.Equal(permissionValueTable.Column("USER_GROUP_ID"), SqlLiteral.Integer(null));
            //var condition =  SqlCondition.And(condition1, condition2, condition3);

            //var filterConditionPermissionValueId = condition;

            //var permissionSelection = new SqlSelectQuery(permissionValueTable, filterConditionPermissionValueId, selectionPermissionValueId);

            //builder.Add(permissionSelection);

            //var ifCondition = SqlCondition.IsNull(variablePermissionValueId);
            ////var insertStatement=new SqlInsertStatement();
            ////var updateStatement=new SqlUpdateStatement();
            ////var ifStatement = SqlIf.If(ifCondition).Then(insertStatement).Else(updateStatement);
            ////builder.Add(ifStatement);


            // Clipboard.SetText(builder.Build());
        }
    }
}