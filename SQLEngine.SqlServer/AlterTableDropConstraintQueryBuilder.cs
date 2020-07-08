namespace SQLEngine.SqlServer
{
    internal class AlterTableDropConstraintQueryBuilder : AlteredTableQueryBuilder
    {
        internal string ConstraintName;
        public override string Build()
        {
            Writer.Write(SQLKeywords.ALTER);
            Writer.Write2(SQLKeywords.TABLE);
            if (!string.IsNullOrEmpty(SchemaName))
            {
                Writer.Write(I(SchemaName));
                Writer.Write(SQLKeywords.DOT);
            }

            Writer.Write(I(TableName));
            Writer.Write(SQLKeywords.SPACE);
            Writer.Write(SQLKeywords.DROP);
            Writer.Write2(SQLKeywords.CONSTRAINT);
            Writer.Write(I(ConstraintName));
            Writer.Write(SQLKeywords.SEMICOLON);

            return base.Build();
        }
    }
}