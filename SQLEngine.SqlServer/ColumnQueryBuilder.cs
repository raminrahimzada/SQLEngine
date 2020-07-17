using System.Collections;

namespace SQLEngine.SqlServer
{
    internal class ColumnQueryBuilder : AbstractQueryBuilder, IColumnQueryBuilder
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
            Model.MaxLength = maxLen == null ? C.MAX : maxLen.ToString();
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
                Writer.Write2(C.AS);
                Writer.WriteScoped(Model.CalculatedColumnExpression);
                if (Model.IsPersisted ?? false) Writer.Write2(C.PERSISTED);
                return base.Build();
            }
            Writer.Write2(Model.Type);

            if (Model.MaxLength != null)
            {
                Writer.WriteScoped(Model.MaxLength);
                Writer.Write(C.SPACE);
            }
            else
            {
                if (((IList)new[] { C.NVARCHAR, C.VARCHAR, C.NCHAR, C.CHAR }).Contains(Model.Type))
                {
                    Writer.Write(C.BEGIN_SCOPE);
                    Writer.Write(C.MAX);
                    Writer.Write(C.END_SCOPE);
                    Writer.Write(C.SPACE);
                }
            }
            if (Model.Type == C.DECIMAL)
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(Model.Precision);
                Writer.Write(C.COMMA);
                Writer.Write(Model.Scale);
                Writer.Write(C.END_SCOPE);
                Writer.Write(C.SPACE);
            }
            if (Model.IsIdentity ?? false)
            {
                Writer.Write(C.IDENTITY);
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(Model.IdentityStart);
                Writer.Write(C.COMMA);
                Writer.Write(Model.IdentityStep);
                Writer.Write(C.END_SCOPE);
            }

            if (Model.NotNull ?? false)
            {
                Writer.Write2(C.NOT);
            }
            Writer.Write(C.NULL);

            if (!string.IsNullOrEmpty(Model.CheckExpression))
            {
                Writer.Write2(C.CHECK);
                Writer.Write2(C.BEGIN_SCOPE);
                Writer.Write(Model.CheckExpression);
                Writer.Write2(C.END_SCOPE);
                Writer.Write(C.SPACE);
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