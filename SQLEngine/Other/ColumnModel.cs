namespace SQLEngine;

public class ColumnModel
{
    public string ForeignKeyConstraintName { get; set; }
    public string ForeignKeySchemaName { get; set; }
    public bool? NotNull { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public bool? IsForeignKey { get; set; }
    public string ForeignKeyColumnName { get; set; }
    public string ForeignKeyTableName { get; set; }
    public string MaxLength { get; set; }
    public string UniqueKeyName { get; set; }
    public string DefaultValue { get; set; }
    public string CalculatedColumnExpression { get; set; }
    public bool? IsPersisted { get; set; }
    public bool? IsIdentity { get; set; }
    public int? IdentityStart { get; set; }
    public int? IdentityStep { get; set; }
    public bool? IsPrimary { get; set; }
    public string PrimaryKeyName { get; set; }
    public bool? IsUniqueKey { get; set; }
    public bool IsUniqueKeyOrderDescending { get; set; }
    public string DefaultConstraintName { get; set; }
    public byte? Precision { get; set; }
    public byte? Scale { get; set; }
    public string CheckExpression { get; set; }
}