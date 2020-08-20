namespace SQLEngine
{
    public interface IAlterTableNoNameQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName);
        IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder AddColumn<T>(string columnName);
        IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
        IAlterTableNoNameDropConstraintQueryBuilder DropConstraint(string constraintName);
        IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName);
        IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName);
        IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder AlterColumn<T>(string columnName);
        IAlterTableAddConstraintQueryBuilder AddConstraint(string constraintName);
    }

    public interface IAlterTableAddConstraintQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params AbstractSqlColumn[] columns);
        IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params string[] columnNames);

        IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(AbstractSqlColumn column);
        IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(string columnName);

        IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(ISqlExpression expression);
        IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(AbstractSqlLiteral expression);
        IAlterTableAddConstraintCheckQueryBuilder Check(AbstractSqlCondition condition);
        IAlterTableAddConstraintCheckQueryBuilder Check(ISqlExpression expression);
    }

    public interface IAlterTableAddConstraintCheckQueryBuilder : IAbstractQueryBuilder
    {

    }

    public interface IAlterTableAddConstraintDefaultQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableAddConstraintDefaultForColumnQueryBuilder ForColumn(AbstractSqlColumn column);
        IAlterTableAddConstraintDefaultForColumnQueryBuilder ForColumn(string columnName);
    }

    public interface IAlterTableAddConstraintDefaultForColumnQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface IAlterTableAddConstraintPrimaryKeyQueryBuilder : IAbstractQueryBuilder
    {

    }
    public interface IAlterTableAddConstraintForeignKeyQueryBuilder : IAbstractQueryBuilder
    {
        IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References(string tableName, string columnName);

        IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References<TTable>(string columnName)
            where TTable : ITable, new();
    }

    public interface IAlterTableAddConstraintForeignKeyReferencesQueryBuilder : IAbstractQueryBuilder
    {

    }
}