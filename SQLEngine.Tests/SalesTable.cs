namespace SQLEngine.Tests
{
    public class SalesTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Sales";
        public string PrimaryColumnName => "Id";
    }
}