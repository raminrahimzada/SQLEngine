namespace SQLEngine;

public interface IColumnsCreateQueryBuilder : IAbstractQueryBuilder
{
    byte DefaultPrecision { get; }
    byte DefaultScale { get; }
    IColumnQueryBuilder Binary(string columnName);
    IColumnQueryBuilder Bool(string columnName);
    IColumnQueryBuilder Byte(string columnName);
    IColumnQueryBuilder Column(string columnName);
    IColumnQueryBuilder Column<T>(string columnName);
    IColumnQueryBuilder Date(string columnName);
    IColumnQueryBuilder Datetime(string columnName);

    IColumnQueryBuilder Decimal(string columnName);
    IColumnQueryBuilder Decimal(string columnName, byte precision, byte scale);
    IColumnQueryBuilder Int(string columnName);
    IColumnQueryBuilder Long(string columnName);
    IColumnQueryBuilder Short(string columnName);
    IColumnQueryBuilder String(string columnName, bool isUniCode = true, bool isVariable = true);
}