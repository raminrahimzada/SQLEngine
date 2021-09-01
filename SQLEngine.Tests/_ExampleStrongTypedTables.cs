using System;

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
    }
    public class UserTable : ITable
    {
        public byte IdByte { get; set; }

        public void Dispose()
        {
        }

        public string Name => "Users";
        public string PrimaryColumnName => "Id";
        public int IdInteger { get; set; }
        public long IdLong { get; set; }
        public Guid IdGuid{ get; set; }
        public double Weight{ get; set; }
        public float Height{ get; set; }
        public byte Age { get; set; }
        public bool IsFemale { get; set; }
        public short IdShort { get; set; }
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