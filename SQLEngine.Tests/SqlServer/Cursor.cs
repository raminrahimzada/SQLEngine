
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    [Fact]
    public void Test_Cursor_1()
    {
        using (var q = Query.New)
        {
            var id = q.Declare<int>("id");
            var name = q.Declare<int>("name");

            var variables = new[] {id,name};

            q.Cursor(
                //name of the cursor
                "cursor1",
                //selection of the cursor
                select => select.From("Users"),
                //variables 
                variables,
                //and the body
                b =>
                {
                    b.Print(id);
                    b.Print(name);
                });


            const string query =
                @"
DECLARE  @id INT ;
DECLARE  @name INT ;

DECLARE cursor1 CURSOR FOR SELECT  * 
    FROM Users

OPEN cursor1

FETCH NEXT FROM cursor1 INTO @id,@name
WHILE @@FETCH_STATUS=0
BEGIN
    print(@id)
    print(@name)

    FETCH NEXT FROM cursor1 INTO @id,@name
END
CLOSE cursor1
DEALLOCATE cursor1

                ";

            SqlAssert.EqualQuery(q.ToString(), query);
        }
    }
}