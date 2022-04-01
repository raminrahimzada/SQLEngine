namespace SQLEngine;

public interface IColumnsCreateQueryBuilder : IAbstractQueryBuilder
{
    byte DefaultPrecision { get; }
    byte DefaultScale { get; }
    IColumnQueryBuilder Column(string columnName);
    IColumnQueryBuilder Column<T>(string columnName);
    IColumnQueryBuilder Datetime(string columnName);
    IColumnQueryBuilder Long(string columnName);
    IColumnQueryBuilder Int(string columnName);
    IColumnQueryBuilder Date(string columnName);
    IColumnQueryBuilder Binary(string columnName);
    IColumnQueryBuilder Byte(string columnName);
    IColumnQueryBuilder String(string columnName, bool isUniCode = true, bool isVariable = true);

    IColumnQueryBuilder Decimal(string columnName);
    IColumnQueryBuilder Decimal(string columnName, byte precision, byte scale);
    IColumnQueryBuilder Bool(string columnName);
    IColumnQueryBuilder Short(string columnName);
}