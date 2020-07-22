namespace SQLEngine.Tests
{
    public class PhotosTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Photos";
        public string PrimaryColumnName => "Id";
    }
}