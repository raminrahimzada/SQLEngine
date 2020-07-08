using System.Collections;

namespace SQLEngine.SqlServer
{
    internal class ColumnQueryBuilder : SqlServerQueryBuilder, IColumnQueryBuilder
    {
        public ColumnModel Model { get; }

        public ColumnQueryBuilder()
        {
            Model = new ColumnModel();
        }
        public IColumnQueryBuilder NotNull()
        {
            Model.NotNull = true;
            return this;
        }
        public IColumnQueryBuilder Name(string name)
        {
            Model.Name = name;
            return this;
        }
        public IColumnQueryBuilder Description(string description)
        {
            Model.Description = description;
            return this;
        }
        public IColumnQueryBuilder Precision(byte precision)
        {
            Model.Precision = precision;
            return this;
        }
        public IColumnQueryBuilder Scale(byte scale)
        {
            Model.Scale = scale;
            return this;
        }
        public IColumnQueryBuilder Type(string type)
        {
            Model.Type = type;
            return this;
        }
        public IColumnQueryBuilder ForeignKey(string tableName, string columnName, string fkName = null)
        {
            Model.IsForeignKey = true;
            Model.ForeignKeyConstraintName = fkName;
            Model.ForeignKeyTableName = tableName;
            Model.ForeignKeyColumnName = columnName;
            return Check(Model.Name + ">0");
        }

        public IColumnQueryBuilder MaxLength(int? maxLen)
        {
            Model.MaxLength = maxLen == null ? SQLKeywords.MAX : maxLen.ToString();
            return this;
        }

        public IColumnQueryBuilder Unique(string keyName = null, bool descending = false)
        {
            if (keyName == null) keyName = string.Empty;
            Model.IsUniqueKey = true;
            Model.IsUniqueKeyOrderDescending = descending;
            Model.UniqueKeyName = keyName;
            return this;
        }

        public IColumnQueryBuilder DefaultValue(string defaultValue, string defaultConstraintName = null)
        {
            Model.DefaultValue = defaultValue;
            Model.DefaultConstraintName = defaultConstraintName;
            return this;
        }

        public IColumnQueryBuilder CalculatedColumn(string expression, bool? isPersisted = true)
        {
            Model.CalculatedColumnExpression = expression;
            Model.IsPersisted = isPersisted;
            return this;
        }
        public IColumnQueryBuilder Identity(int start = 1, int step = 1)
        {
            Model.IsIdentity = true;
            Model.IdentityStart = start;
            Model.IdentityStep = step;
            return Primary().NotNull();
        }

        public IColumnQueryBuilder Primary(string keyName = null)
        {
            Model.IsPrimary = true;
            Model.PrimaryKeyName = keyName;
            return this;
        }



        public override string Build()
        {
            Writer.Write(I(Model.Name));
            if (!string.IsNullOrEmpty(Model.CalculatedColumnExpression))
            {
                Writer.Write2(SQLKeywords.AS);
                Writer.WriteScoped(Model.CalculatedColumnExpression);
                if (Model.IsPersisted ?? false) Writer.Write2(SQLKeywords.PERSISTED);
                return base.Build();
            }
            Writer.Write2(Model.Type);

            if (Model.MaxLength != null)
            {
                Writer.WriteScoped(Model.MaxLength);
                Writer.Write(SQLKeywords.SPACE);
            }
            else
            {
                if (((IList)new[] { SQLKeywords.NVARCHAR, SQLKeywords.VARCHAR, SQLKeywords.NCHAR, SQLKeywords.CHAR }).Contains(Model.Type))
                {
                    Writer.Write(SQLKeywords.BEGIN_SCOPE);
                    Writer.Write(SQLKeywords.MAX);
                    Writer.Write(SQLKeywords.END_SCOPE);
                    Writer.Write(SQLKeywords.SPACE);
                }
            }
            if (Model.Type == SQLKeywords.DECIMAL)
            {
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(Model.Precision);
                Writer.Write(SQLKeywords.COMMA);
                Writer.Write(Model.Scale);
                Writer.Write(SQLKeywords.END_SCOPE);
                Writer.Write(SQLKeywords.SPACE);
            }
            if (Model.IsIdentity ?? false)
            {
                Writer.Write(SQLKeywords.IDENTITY);
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(Model.IdentityStart);
                Writer.Write(SQLKeywords.COMMA);
                Writer.Write(Model.IdentityStep);
                Writer.Write(SQLKeywords.END_SCOPE);
            }

            if (Model.NotNull ?? false)
            {
                Writer.Write2(SQLKeywords.NOT);
            }
            Writer.Write(SQLKeywords.NULL);

            if (!string.IsNullOrEmpty(Model.CheckExpression))
            {
                Writer.Write2(SQLKeywords.CHECK);
                Writer.Write2(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(Model.CheckExpression);
                Writer.Write2(SQLKeywords.END_SCOPE);
                Writer.Write(SQLKeywords.SPACE);
            }
            return base.Build();
        }

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            if (string.IsNullOrEmpty(Model.Name))
            {
                Bomb();
            }
            if (string.IsNullOrEmpty(Model.Type) && string.IsNullOrEmpty(Model.CalculatedColumnExpression))
            {
                Bomb();
            }
        }

        public IColumnQueryBuilder Check(string checkExpression)
        {
            if (!string.IsNullOrEmpty(Model.CheckExpression))
            {
                Model.CheckExpression = "(" + Model.CheckExpression + ")AND(" + checkExpression + ")";
            }
            else
            {
                Model.CheckExpression = checkExpression;
            }
            return this;
        }
    }
}