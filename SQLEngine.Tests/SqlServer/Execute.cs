﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Execute_Procedure_1()
        {
            using (var q = Query.New)
            {
                q
                    .Execute
                    .Procedure("addUser")
                    .Schema("dbo")
                    .Arg("Name", "Nikola")
                    .Arg("Surname", "Tesla")
                    .Arg("Surname", 87)
                    .Arg("IsReallyFamousInventor", true)
                    ;
                    

                const string query = @"
EXECUTE dbo.addUser  @Name=N'Nikola'
	,@Surname=N'Tesla'
	,@Surname=87
	,@IsReallyFamousInventor=1;

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Execute_Procedure_2()
        {
            using (var q = Query.New)
            {
                q
                    .Execute
                    .Procedure("addUser")
                    .Arg("Name", "Nikola")
                    .Arg("Surname", "Tesla")
                    .Arg("Surname", 87)
                    .Arg("IsReallyFamousInventor", true)
                    ;


                const string query = @"
EXECUTE addUser  @Name=N'Nikola'
	,@Surname=N'Tesla'
	,@Surname=87
	,@IsReallyFamousInventor=1;

";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Execute_Function_1()
        {
            using (var q = Query.New)
            {
                var execution =
                        Query.New
                            
                            .Build() //Surname

                    ;
                q.Declare<bool>("b", x => x.Execute
                    .Function("UserExists")
                    .Schema("dbo")
                    .Arg("Nikola") //Name
                    .Arg("Tesla")
                );
                
                const string query = @"

DECLARE @b BIT = dbo.UserExists (N'Nikola', N'Tesla')
";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

    }
}