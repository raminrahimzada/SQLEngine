using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public class ColumnQueryBuilder : AbstractQueryBuilder
    {
        private readonly ColumnModel _model;

        public ColumnQueryBuilder()
        {
            _model=new ColumnModel();
        }

        public ColumnQueryBuilder Null()
        {
            _model.Nullable = true;
            return this;
        }

        public ColumnQueryBuilder NotNull()
        {
            _model.Nullable = false;
            return this;
        }
        public ColumnQueryBuilder Name(string name)
        {
            _model.Name = name;
            return this;
        }

        public ColumnQueryBuilder Precision(byte precision)
        {
            _model.Precision = precision;
            return this;
        }
        public ColumnQueryBuilder Scale(byte scale)
        {
            _model.Scale = scale;
            return this;
        }
        public ColumnQueryBuilder Type(string type)
        {
            _model.Type = type;
            return this;
        }

        public ColumnQueryBuilder ForeignKey(string tableName,string columnName,string fkName=null)
        {
            _model.IsForeignKey = true;
            _model.ForeignKeyConstraintName = fkName;
            _model.ForeignKeyTableName = tableName;
            _model.ForeignKeyColumnName = columnName;
            return this;
        }

        public ColumnQueryBuilder MaxLength(int? maxLen)
        {
            _model.MaxLength = maxLen == null ? MAX : maxLen.ToString();
            return this;
        }

        public ColumnQueryBuilder Unique(string keyName=null,bool descending=false)
        {
            if (keyName == null) keyName = string.Empty;
            _model.IsUniqueKey = true;
            _model.IsUniqueKeyOrderDescending = descending;
            _model.UniqueKeyName = keyName;
            return this;
        }

        public ColumnQueryBuilder DefaultValue(string defaultValue,string defaultConstraintName=null)
        {
            _model.DefaultValue = defaultValue;
            _model.DefaultConstraintName = defaultConstraintName;
            return this;
        }

        public ColumnQueryBuilder CalculatedColumn(string expression,bool? isPersisted=true)
        {
            _model.CalculatedColumnExpression = expression;
            _model.IsPersisted = isPersisted;
            return this;
        }

        public ColumnQueryBuilder Identity(int start=1,int step=1)
        {
            _model.IsIdentity = true;
            _model.IdentityStart = start;
            _model.IdentityStep = step;
            return Primary().NotNull();
        }

        public ColumnQueryBuilder Primary(string keyName=null)
        {
            _model.IsPrimary = true;
            _model.PrimaryKeyName = keyName;
            return this;
        }

        internal ColumnModel BuildModel()
        {
            return _model;
        }

        internal class ColumnModel
        {
            public string ForeignKeyConstraintName { get; set; }
            public bool? Nullable { get; set; }
            public string Name { get; set; }
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
            public bool IsUniqueKeyClustered { get; set; }
            public string DefaultConstraintName { get; set; }
            public byte? Precision { get; set; }
            public byte? Scale { get; set; }
        }

        public override string Build()
        {
            Writer.Write(I(_model.Name));
            if (!string.IsNullOrEmpty(_model.CalculatedColumnExpression))
            {
                Writer.Write2(AS);
                Writer.WriteScoped(_model.CalculatedColumnExpression);
                if (_model.IsPersisted ?? false) Writer.Write2(PERSISTED);
                return base.Build();
            }
            Writer.Write2(_model.Type);

            if (_model.MaxLength != null)
            {
                Writer.WriteScoped(_model.MaxLength);
                Writer.Write(SPACE);
            }
            if (_model.Type == DECIMAL)
            {
                Writer.Write(BEGIN_SCOPE);
                Writer.Write(_model.Precision);
                Writer.Write(COMMA);
                Writer.Write(_model.Scale);
                Writer.Write(END_SCOPE);
                Writer.Write(SPACE);
            }
            if (_model.IsIdentity??false)
            {
                Writer.Write(IDENTITY);
                Writer.Write(BEGIN_SCOPE);
                Writer.Write(_model.IdentityStart);
                Writer.Write(COMMA);
                Writer.Write(_model.IdentityStep);
                Writer.Write(END_SCOPE);
            }

            if (!(_model.Nullable ?? true))
            {
                Writer.Write2(NOT);
            }
            Writer.Write(NULL);

            return base.Build();
        }
    }
}