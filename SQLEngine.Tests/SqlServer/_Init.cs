using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests.SqlServer
{
    [TestClass]
    public partial class AllTests
    {
        [TestInitialize]
        public void Init()
        {
            Query.Setup<SqlServerQueryBuilder>();
        }
    }
}