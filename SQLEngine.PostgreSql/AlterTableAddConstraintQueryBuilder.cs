using System.Linq;

namespace SQLEngine.PostgreSql
{
    internal class AlterTableAddConstraintQueryBuilder : AbstractQueryBuilder,
        IAlterTableAddConstraintQueryBuilder,
        IAlterTableAddConstraintPrimaryKeyQueryBuilder,
        IAlterTableAddConstraintForeignKeyQueryBuilder, 
        IAlterTableAddConstraintForeignKeyReferencesQueryBuilder,
        IAlterTableAddConstraintDefaultQueryBuilder,
        IAlterTableAddConstraintDefaultForColumnQueryBuilder,
        IAlterTableAddConstraintCheckQueryBuilder
    {
        private string _tableName;
        private string _constraintName;
        
        private string[] _primaryKeyColumns;

        private string _foreignKeyColumn;
        private string _foreignKeyReferenceTableName;
        private string _foreignKeyReferenceColumnName;


        private string _defaultValue;
        private string _defaultValueColumn;

        private bool _isPkQuery;
        private bool _isFkQuery;
        private bool _isDfQuery;
        private bool _isCkQuery;

        private string _checkCondition;

        public override void Build(ISqlWriter writer)
        {

            writer.Write(C.ALTER);
            writer.Write(C.SPACE);
            writer.Write(C.TABLE);
            writer.Write(C.SPACE);
            writer.Write(_tableName);
            writer.Write(C.SPACE);
            writer.Write(C.ADD);
            writer.Write(C.SPACE);
            writer.Write(C.CONSTRAINT);
            writer.Write(C.SPACE);
            writer.Write(_constraintName);
            writer.Write(C.SPACE);


            if (_isPkQuery)
            {
                writer.Write(C.PRIMARY);
                writer.Write(C.SPACE);
                writer.Write(C.KEY);
                writer.Write(C.SPACE);
                writer.Write(C.BEGIN_SCOPE);
                for (var i = 0; i < _primaryKeyColumns.Length; i++)
                {
                    if (i != 0)
                    {
                        writer.Write(C.COMMA);
                    }

                    writer.Write(_primaryKeyColumns[i]);
                }
                writer.Write(C.END_SCOPE);
                writer.Write(C.SEMICOLON);
            }
            else if (_isFkQuery)
            {
                writer.Write(C.FOREIGN);
                writer.Write(C.SPACE);
                writer.Write(C.KEY);
                writer.Write(C.SPACE);
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(_foreignKeyColumn);
                writer.Write(C.END_SCOPE);

                writer.Write(C.REFERENCES);
                writer.Write(C.SPACE);
                writer.Write(_foreignKeyReferenceTableName);
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(_foreignKeyReferenceColumnName);
                writer.Write(C.END_SCOPE);
                writer.Write(C.SEMICOLON);
            }
            else if (_isDfQuery)
            {
                writer.Write(C.DEFAULT);
                writer.Write(C.SPACE);
                writer.Write(_defaultValue);
                writer.Write(C.SPACE);
                writer.Write(C.FOR);
                writer.Write(C.SPACE);
                writer.Write(_defaultValueColumn);
                writer.Write(C.SEMICOLON);
            }
            else if(_isCkQuery)
            {
                writer.Write(C.CHECK);
                writer.Write(C.SPACE);
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(_checkCondition);
                writer.Write(C.END_SCOPE);
            }
            else
            {
                throw Bomb("Invalid Usage bcs none of PK,FK,DF");
            }
        }

        public AlterTableAddConstraintQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IAlterTableAddConstraintQueryBuilder ConstraintName(string constraintName)
        {
            _constraintName = constraintName;
            return this;
        }

        public IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params AbstractSqlColumn[] columns)
        {
            return PrimaryKey(columns.Select(c => c.Name).ToArray());
        }

        public IAlterTableAddConstraintPrimaryKeyQueryBuilder PrimaryKey(params string[] columnNames)
        {
            _primaryKeyColumns = columnNames;
            _isPkQuery = true;
            return this;             
        }

        public IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(AbstractSqlColumn column)
        {
            _foreignKeyColumn = column.Name;
            _isFkQuery = true;
            return this;
        }

        public IAlterTableAddConstraintForeignKeyQueryBuilder ForeignKey(string columnName)
        {
            _foreignKeyColumn = columnName;
            _isFkQuery = true;
            return this;
        }


        public IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(ISqlExpression expression)
        {
            _defaultValue = expression.ToSqlString();
            _isDfQuery = true;
            return this;
        }

        public IAlterTableAddConstraintDefaultQueryBuilder DefaultValue(AbstractSqlLiteral expression)
        {
            _defaultValue = expression.ToSqlString();
            _isDfQuery = true;
            return this;
        }

        public IAlterTableAddConstraintCheckQueryBuilder Check(AbstractSqlCondition condition)
        {
            _checkCondition = condition.ToSqlString();
            _isCkQuery = true;
            return this;
        }

        public IAlterTableAddConstraintCheckQueryBuilder Check(ISqlExpression expression)
        {
            _checkCondition = expression.ToSqlString();
            _isCkQuery = true;
            return this;
        }

        public IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References(string tableName, string columnName)
        {
            _foreignKeyReferenceTableName = tableName;
            _foreignKeyReferenceColumnName = columnName;
            return this;
        }

        public IAlterTableAddConstraintForeignKeyReferencesQueryBuilder References<TTable>(string columnName) where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                _foreignKeyReferenceTableName = table.Name;
                _foreignKeyReferenceColumnName = columnName;
                return this;
            }
        }

        public IAlterTableAddConstraintDefaultForColumnQueryBuilder ForColumn(AbstractSqlColumn column)
        {
            _defaultValueColumn = column.Name;
            return this;
        }

        public IAlterTableAddConstraintDefaultForColumnQueryBuilder ForColumn(string columnName)
        {
            _defaultValueColumn = columnName;
            return this;
        }
    }
}