using System.Collections;

namespace SQLEngine.SqlServer;

internal class ColumnQueryBuilder : AbstractQueryBuilder, IColumnQueryBuilder
{
    public ColumnModel Model { get; }

    public ColumnQueryBuilder()
    {
        Model = new ColumnModel();
    }
    public IColumnQueryBuilder NotNull(bool notNull = true)
    {
        Model.NotNull = notNull;
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
    public IColumnQueryBuilder ForeignKey(string tableName, string schemaName, string columnName, string fkName = null)
    {
        Model.IsForeignKey = true;
        Model.ForeignKeyConstraintName = fkName;
        Model.ForeignKeyTableName = tableName;
        Model.ForeignKeyColumnName = columnName;
        Model.ForeignKeySchemaName = schemaName;
        return this;
        //return Check(Model.Name + ">0");
    }

    public IColumnQueryBuilder MaxLength(int? maxLen)
    {
        Model.MaxLength = maxLen == null ? C.MAX : maxLen.ToString();
        return this;
    }

    public IColumnQueryBuilder Unique(string keyName = null, bool descending = false)
    {
        if(keyName == null)
        {
            keyName = string.Empty;
        }

        Model.IsUniqueKey = true;
        Model.IsUniqueKeyOrderDescending = descending;
        Model.UniqueKeyName = keyName;
        return this;
    }

    public IColumnQueryBuilder DefaultValue(ISqlExpression defaultValue, string defaultConstraintName = null)
    {
        Model.DefaultValue = defaultValue.ToSqlString();
        Model.DefaultConstraintName = defaultConstraintName;
        return this;
    }
    public IColumnQueryBuilder DefaultValue(AbstractSqlLiteral literal, string defaultConstraintName = null)
    {
        Model.DefaultValue = literal.ToSqlString();
        Model.DefaultConstraintName = defaultConstraintName;
        return this;
    }

    public IColumnQueryBuilder CalculatedColumn(AbstractSqlExpression expression, bool? isPersisted = true)
    {
        Model.CalculatedColumnExpression = expression.ToSqlString();
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



    public override void Build(ISqlWriter writer)
    {
        writer.Write(I(Model.Name));
        if(!string.IsNullOrEmpty(Model.CalculatedColumnExpression))
        {
            writer.Write2(C.AS);
            writer.WriteScoped(Model.CalculatedColumnExpression);
            if(Model.IsPersisted ?? false)
            {
                writer.Write2(C.PERSISTED);
            }

            return;
        }
        writer.Write2(Model.Type);

        if(Model.MaxLength != null)
        {
            writer.WriteScoped(Model.MaxLength);
            writer.Write(C.SPACE);
        }
        else
        {
            if(((IList)new[] { C.NVARCHAR, C.VARCHAR, C.NCHAR, C.CHAR }).Contains(Model.Type))
            {
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(C.MAX);
                writer.Write(C.END_SCOPE);
                writer.Write(C.SPACE);
            }
        }
        if(Model.Type == C.DECIMAL)
        {
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(Model.Precision ?? Query.Settings.DefaultPrecision);
            writer.Write(C.COMMA);
            writer.Write(Model.Scale ?? Query.Settings.DefaultScale);
            writer.Write(C.END_SCOPE);
            writer.Write(C.SPACE);
        }
        if(Model.IsIdentity ?? false)
        {
            writer.Write(C.IDENTITY);
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(Model.IdentityStart);
            writer.Write(C.COMMA);
            writer.Write(Model.IdentityStep);
            writer.Write(C.END_SCOPE);
        }

        if(Model.NotNull ?? false)
        {
            writer.Write2(C.NOT);
        }
        writer.Write(C.NULL);

        if(!string.IsNullOrEmpty(Model.CheckExpression))
        {
            writer.Write2(C.CHECK);
            writer.Write2(C.BEGIN_SCOPE);
            writer.Write(Model.CheckExpression);
            writer.Write2(C.END_SCOPE);
            writer.Write(C.SPACE);
        }
    }

    protected override void ValidateAndThrow()
    {
        base.ValidateAndThrow();
        if(string.IsNullOrEmpty(Model.Name))
        {
            Bomb();
        }
        if(string.IsNullOrEmpty(Model.Type) && string.IsNullOrEmpty(Model.CalculatedColumnExpression))
        {
            Bomb();
        }
    }

    public IColumnQueryBuilder Check(AbstractSqlCondition condition)
    {
        return Check(condition.ToSqlString());
    }
    public IColumnQueryBuilder Check(ISqlExpression expression)
    {
        return Check(expression.ToSqlString());
    }
    public IColumnQueryBuilder Check(string checkExpression)
    {
        if(!string.IsNullOrEmpty(Model.CheckExpression))
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