namespace SQLEngine.Tests
{
    public class OrderTable : ITable
    {
        public void Dispose()
        {
            
        }

        public string Name => "Orders";
    }
    public class CustomerTable : ITable
    {
        public void Dispose()
        {
            
        }

        public string Name => "Customers";
        public string PrimaryColumnName => Query.Settings.DefaultIdColumnName;
    }
    public class UserTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Users";
        public string PrimaryColumnName => "Id";
    }
    public class AttachmentsTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Attachments";
        public string PrimaryColumnName => "Id";
    }
    public class AnotherUsersTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "AnotherUsers";
        public string PrimaryColumnName => "Id";
    }
    public class SalesTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Sales";
        public string PrimaryColumnName => "Id";
    }

    public class PhotosTable : ITable
    {
        public void Dispose()
        {
        }

        public string Name => "Photos";
        public string PrimaryColumnName => "Id";
    }
}