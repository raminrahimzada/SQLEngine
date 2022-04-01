using System;

namespace SQLEngine.Tests;

public abstract class BaseTable : ITable
{
    public abstract string Name { get; }
    public string Schema => "dbo";

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
public class OrderTable : BaseTable
{
    public override string Name => "Orders";
}
public class CustomerTable : BaseTable
{
    public override string Name => "Customers";
}
public class UserTable : BaseTable
{
    public override string Name => "Users";

    public byte IdByte { get; set; }
    public int IdInteger { get; set; }
    public long IdLong { get; set; }
    public Guid IdGuid { get; set; }
    public double Weight { get; set; }
    public float Height { get; set; }
    public byte Age { get; set; }
    public bool IsFemale { get; set; }
    public short IdShort { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Surname { get; set; }
}
public class AttachmentsTable : BaseTable
{
    public override string Name => "Attachments";
}
public class AnotherUsersTable : BaseTable
{
    public override string Name => "AnotherUsers";
}
public class SalesTable : BaseTable
{
    public override string Name => "Sales";
}

public class PhotosTable : BaseTable
{
    public override string Name => "Photos";
}