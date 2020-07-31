namespace SQLEngine.SqlServer
{
    internal class AlterTableQueryBuilder :AbstractQueryBuilder,
             IAlterTableQueryBuilder
            , IAlterTableNoNameQueryBuilder
            , IAlterTableNoNameDropColumnQueryBuilder
    {
        private IAbstractQueryBuilder _internalBuilder;
        private string _tableName;
        public IAlterTableNoNameQueryBuilder TableName(string tableName)
        {
            _tableName = tableName;
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
            _internalBuilder= b.Table(_tableName).Column(columnName);
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

        public override void Dispose()
        {
            _internalBuilder.Dispose();
        }

        public override void Build(ISqlWriter writer)
        {
            _internalBuilder.Build(writer);
        }
    }
}