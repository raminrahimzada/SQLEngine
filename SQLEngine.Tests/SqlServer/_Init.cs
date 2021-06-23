
using SQLEngine.SqlServer;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        public AllTests()
        {
            Query.Setup<SqlServerQueryBuilder>();
        }
    }
}