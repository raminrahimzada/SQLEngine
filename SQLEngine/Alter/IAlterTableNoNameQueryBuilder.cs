﻿namespace SQLEngine;

public interface IAlterTableNoNameQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableNoNameAddColumnNoNameQueryBuilder AddColumn(string columnName);
    IAlterTableNoNameAddColumnNoNameNoTypeNameQueryBuilder AddColumn<T>(string columnName);
    IAlterTableAddConstraintQueryBuilder AddConstraint(string constraintName);
    IAlterTableNoNameAlterColumnQueryBuilder AlterColumn(string columnName);
    IAlterTableNoNameAlterColumnNoNewTypeQueryBuilder AlterColumn<T>(string columnName);
    IAlterTableNoNameDropColumnQueryBuilder DropColumn(string columnName);
    IAlterTableNoNameDropConstraintQueryBuilder DropConstraint(string constraintName);
    IAlterTableNoNameRenameColumnQueryBuilder RenameColumn(string columnName);
}

public interface IAlterTableAddConstraintQueryBuilder : IAbstractQueryBuilder
{
    IAlterTableAddConstraintCheckQueryBuilder Check(AbstractSqlCondition condition);
    IAlterTableAddConstraintCheckQueryBuilder Check(ISqlExpression expression);

    IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(ISqlExpression expression);
    IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(AbstractSqlLiteral expression);

    IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(AbstractSqlColumn column);
    IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(string columnName);
    IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params AbstractSqlColumn[] columns);
    IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params string[] columnNames);
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
    IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References(string tableName, string schema,
        string columnName);

    IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References<TTable>(string columnName)
        where TTable : ITable, new();
}

public interface IAlterTableAddConstraintForeignKeyReferencesQueryBuilder : IAbstractQueryBuilder
{
}