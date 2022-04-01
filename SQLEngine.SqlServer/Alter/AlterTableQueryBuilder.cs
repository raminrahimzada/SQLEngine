namespace SQLEngine.SqlServer;

internal sealed class AlterTableQueryBuilder : AbstractQueryBuilder,
    IAlterTableQueryBuilder
    , IAlterTableNoNameQueryBuilder
    , IAlterTableNoNameDropColumnQueryBuilder
{
    private IAbstractQueryBuilder _internalBuilder;
    private string _tableName;
    private string _schema;
    public IAlterTableNoNameQueryBuilder TableName(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
        return this;
    }

    public IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName)
    {
        var b = new AlterTableAddColumnQueryBuilder();
        _internalBuilder = b.Table(_tableName).Column(columnName);
        return b;
    }

    public IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder AddColumn<T>(string columnName)
    {
        return AddColumn(columnName).OfType<T>();
    }

    public IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName)
    {
        var b = new AlterTableDropColumnQueryBuilder();
        _internalBuilder = b.Table(_tableName).Column(columnName);
        return b;
    }

    public IAlterTableNoNameDropConstraintQueryBuilder DropConstraint(string constraintName)
    {
        var b = new AlterTableDropConstraintQueryBuilder();
        _internalBuilder = b.Table(_tableName).Constraint(constraintName);
        return b;
    }

    public IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName)
    {
        var b = new AlterTableRenameColumnQueryBuilder();
        _internalBuilder = b.Table(_tableName).Column(columnName);
        return b;
    }

    public IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName)
    {
        var b = new AlterTableAlterColumnQueryBuilder();
        _internalBuilder = b.Table(_tableName).Column(columnName);
        return b;
    }

    public IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder AlterColumn<T>(string columnName)
    {
        return AlterColumn(columnName).Type<T>();
    }

    public IAlterTableAddConstraintQueryBuilder AddConstraint(string constraintName)
    {
        var b = new AlterTableAddConstraintQueryBuilder();
        _internalBuilder = b.Table(_tableName, _schema).ConstraintName(constraintName);
        return b;
    }

    protected override void Dispose(bool disposing)
    {
        if(disposing)
        {
            _internalBuilder?.Dispose();
        }
    }

    public override void Build(ISqlWriter writer)
    {
        _internalBuilder.Build(writer);
    }
}