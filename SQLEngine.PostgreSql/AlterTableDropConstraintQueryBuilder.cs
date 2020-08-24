namespace SQLEngine.PostgreSql
{
    internal class AlterTableDropConstraintQueryBuilder : AbstractQueryBuilder,
        IAlterTableNoNameDropConstraintQueryBuilder
    {
        private string _tableName;
        private string _constraintName;

        public AlterTableDropConstraintQueryBuilder Table(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public AlterTableDropConstraintQueryBuilder Constraint(string constraintName)
        {
            _constraintName = constraintName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.ALTER);
            writer.Write(C.SPACE);
            writer.Write(C.TABLE);
            writer.Write(C.SPACE);
            writer.Write(_tableName);
            writer.Write(C.SPACE);
            writer.Write(C.DROP);
            writer.Write(C.SPACE);
            writer.Write(C.CONSTRAINT);
            writer.Write(C.SPACE);
            writer.Write(_constraintName);
        }
    }
}