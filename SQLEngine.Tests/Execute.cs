using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {
        [TestMethod]
        public void Test_Execute_Procedure_1()
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
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Execute_Function_1()
        {
            using (var q = Query.New)
            {
                q
                    .Execute
                    .Function("UserExists")
                    .Schema("dbo")
                    .Arg("Nikola") //Name
                    .Arg("Tesla") //Surname
                    ;

                const string query = @"
dbo.UserExists (N'Nikola', N'Tesla')
";
                QueryAssert.AreEqual(q.ToString(), query);
            }
        }

    }
}