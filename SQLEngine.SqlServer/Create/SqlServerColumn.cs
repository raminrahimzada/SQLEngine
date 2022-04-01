using System;
using System.Linq;

namespace SQLEngine.SqlServer;

internal class SqlServerColumn : AbstractSqlColumn
{
    public string TableAlias { get; set; }
    public SqlServerColumn(string name)
    {
        Name = name;
    }
    public SqlServerColumn(string name,string tableAlias)
    {
        Name = name;
        TableAlias = tableAlias;
    }

    public override string ToString()
    {
        return ToSqlString();
    }

    public override string ToSqlString()
    {
        string ToSqlStringNoAlias()
        {
            if (Name == C.WILCARD + string.Empty) return Name;
            if (!Name.All(char.IsLetterOrDigit)) return "[" + Name + "]";
            if (Name.Equals("key", StringComparison.InvariantCultureIgnoreCase)) return "[" + Name + "]";
            if (Name.Equals("value", StringComparison.InvariantCultureIgnoreCase)) return "[" + Name + "]";
            if (!char.IsLetter(Name.First())) return "[" + Name + "]";
            return Name;
        }

        if (!string.IsNullOrWhiteSpace(TableAlias))
        {
            return TableAlias + C.DOT + ToSqlStringNoAlias();
        }

        return ToSqlStringNoAlias();
    }

    public override AbstractSqlCondition Like(string expression, bool isUnicode = true)
    {
        return SqlServerCondition.Raw(
            string.Concat(
                ToSqlString(),
                C.SPACE,
                C.LIKE,
                C.SPACE,
                expression.ToSQL(isUnicode)
            ));
    }

    public override AbstractSqlCondition IsNull()
    {
        return SqlServerCondition.Raw(
            string.Concat(
                ToSqlString(),
                C.SPACE,
                C.IS,
                C.SPACE,
                C.NULL
            ));
    }

    public override AbstractSqlCondition IsNotNull()
    {
        return SqlServerCondition.Raw(
            string.Concat(
                ToSqlString(),
                C.SPACE,
                C.IS,
                C.SPACE,
                C.NOT,
                C.SPACE,
                C.NULL
            ));
    }

    public override AbstractSqlCondition Between(AbstractSqlLiteral from, AbstractSqlLiteral to)
    {
        var expression = "(" + ToSqlString() + " BETWEEN " + from.ToSqlString() + " AND " +
                         to.ToSqlString() + ")";

        return SqlServerCondition.Raw(expression);
    }

    public override AbstractSqlCondition Between(ISqlExpression from, ISqlExpression to)
    {
        var expression = "(" + ToSqlString() + " BETWEEN " + from.ToSqlString() + " AND " +
                         to.ToSqlString() + ")";

        return SqlServerCondition.Raw(expression);
    }

    public override AbstractSqlCondition In(Action<ISelectQueryBuilder> builderFunc)
    {
        var writer = SqlWriter.New;
        writer.Write(ToSqlString());
        writer.Write(C.SPACE);
        writer.Write(C.IN);
        writer.Write(C.SPACE);
        writer.Write(C.BEGIN_SCOPE);
        using (var builder = new SelectQueryBuilder())
        {
            builderFunc(builder);
            builder.Build(writer);
        }

        writer.Write(C.END_SCOPE);

        return SqlServerCondition.Raw(writer.Build());
    }

    public override AbstractSqlCondition In(params AbstractSqlLiteral[] expressions)
    {
        if (expressions.Length == 0)
            throw new SqlEngineException("At least one element needed in -IN query needed");
        var expression = ToSqlString() + " IN (" +
                         string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

        return SqlServerCondition.Raw(expression);
    }

    public override AbstractSqlCondition NotIn(params AbstractSqlLiteral[] expressions)
    {
        if (expressions.Length == 0)
            throw new SqlEngineException("At least one element needed in -NOT IN query needed");
        var expression = ToSqlString() + " NOT IN (" +
                         string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

        return SqlServerCondition.Raw(expression);
    }

    public override AbstractSqlCondition NotIn(Action<ISelectQueryBuilder> builderFunc)
    {
        var writer = SqlWriter.New;
        writer.Write(ToSqlString());
        writer.Write(C.SPACE);
        writer.Write(C.NOT);
        writer.Write(C.SPACE);
        writer.Write(C.IN);
        writer.Write(C.SPACE);
        writer.Write(C.BEGIN_SCOPE);
        using (var builder = new SelectQueryBuilder())
        {
            builderFunc(builder);
            builder.Build(writer);
        }

        writer.Write(C.END_SCOPE);

        return SqlServerCondition.Raw(writer.Build());
    }

        
    #region Add
    protected override AbstractSqlExpression Add(AbstractSqlExpression right)
    {
        return new SqlServerRawExpression(ToSqlString() + "+" + right.ToSqlString());
    }
    protected override AbstractSqlExpression Add(AbstractSqlLiteral right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.ADD, right.ToSqlString());
    }
    protected override AbstractSqlExpression Add(AbstractSqlColumn right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.PLUS + string.Empty, right.ToSqlString());
    }



    #endregion

    #region Subtract
    protected override AbstractSqlExpression Subtract(AbstractSqlExpression right)
    {
        return new SqlServerRawExpression(ToSqlString() + "-" + right.ToSqlString());
    }

    protected override AbstractSqlExpression Subtract(AbstractSqlLiteral right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.MINUS + string.Empty, right.ToSqlString());
    }
    protected override AbstractSqlExpression Subtract(AbstractSqlColumn right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.MINUS + string.Empty, right.ToSqlString());
    }


    #endregion

    #region Multiple
    protected override AbstractSqlExpression Multiply(AbstractSqlExpression right)
    {
        return new SqlServerRawExpression(ToSqlString() + "*" + right.ToSqlString());
    }

    protected override AbstractSqlExpression Multiply(AbstractSqlLiteral right)
    {
        return new SqlServerRawExpression(ToSqlString() + "*" + right.ToSqlString());
    }
    protected override AbstractSqlExpression Multiply(AbstractSqlColumn right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.MULTIPLY + string.Empty, right.ToSqlString());
    }


    #endregion

    #region Divide

    protected override AbstractSqlExpression Divide(AbstractSqlColumn right)
    {
        return new SqlServerRawExpression(ToSqlString(), C.DIVIDE + string.Empty, right.ToSqlString());
    }

    protected override AbstractSqlExpression Divide(AbstractSqlLiteral right)
    {
        return new SqlServerRawExpression(ToSqlString() + "/" + right.ToSqlString());
    }

    protected override AbstractSqlExpression Divide(AbstractSqlExpression right)
    {
        return new SqlServerRawExpression(ToSqlString() + "/" + right.ToSqlString());
    }


    #endregion

    #region EqualTo

    protected override AbstractSqlCondition EqualTo(AbstractSqlExpression expression)
    {
        var rawSqlString = ToSqlString() + " = " + expression.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition EqualTo(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " = " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition EqualTo(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " = " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition EqualTo(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " = " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition EqualTo(int value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(bool value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(byte value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(byte[] value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(DateTime value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(string value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(Guid value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(long value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(decimal value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(double value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    protected override AbstractSqlCondition EqualTo(float value)
    {
        return EqualTo((AbstractSqlLiteral) value);
    }

    #endregion

    #region NotEqualTo

    protected override AbstractSqlCondition NotEqualTo(AbstractSqlExpression expression)
    {
        var rawSqlString = ToSqlString() + " <> " + expression.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " <> " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " <> " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " <> " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(int value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(byte value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(byte[] value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(DateTime value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(bool value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(string value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(Guid value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(long value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(decimal value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(double value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition NotEqualTo(float value)
    {
        var rawSqlString = ToSqlString() + " <> " + AbstractSqlLiteral.From(value).ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    #endregion

    #region Greater

    protected override AbstractSqlCondition Greater(int value)
    {
        return Greater((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Greater(long value)
    {
        return Greater((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Greater(double value)
    {
        return Greater((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Greater(DateTime value)
    {
        return Greater((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Greater(byte value)
    {
        return Greater((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Greater(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " > " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition Greater(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " > " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition Greater(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " > " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    #endregion

    #region GreaterEqual

    protected override AbstractSqlCondition GreaterEqual(int value)
    {
        return GreaterEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition GreaterEqual(long value)
    {
        return GreaterEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition GreaterEqual(double value)
    {
        return GreaterEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition GreaterEqual(DateTime value)
    {
        return GreaterEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition GreaterEqual(byte value)
    {
        return GreaterEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition GreaterEqual(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " >= " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition GreaterEqual(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " >= " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition GreaterEqual(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " >= " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    #endregion

    #region Less

    protected override AbstractSqlCondition Less(int value)
    {
        return Less((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Less(long value)
    {
        return Less((SqlServerLiteral) value);
    }


    protected override AbstractSqlCondition Less(double value)
    {
        return Less((SqlServerLiteral) value);
    }


    protected override AbstractSqlCondition Less(DateTime value)
    {
        return Less((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Less(byte value)
    {
        return Less((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition Less(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " < " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    protected override AbstractSqlCondition Less(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " < " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition Less(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " < " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }

    #endregion

    #region LessEqual

    protected override AbstractSqlCondition LessEqual(AbstractSqlColumn otherColumn)
    {
        var rawSqlString = ToSqlString() + " <= " + otherColumn.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition LessEqual(AbstractSqlLiteral value)
    {
        var rawSqlString = ToSqlString() + " <= " + value.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition LessEqual(AbstractSqlVariable variable)
    {
        var rawSqlString = ToSqlString() + " <= " + variable.ToSqlString();
        return SqlServerCondition.Raw(rawSqlString);
    }


    protected override AbstractSqlCondition LessEqual(byte value)
    {
        return LessEqual((SqlServerLiteral) value);
    }

    protected override AbstractSqlCondition LessEqual(DateTime value)
    {
        return LessEqual((SqlServerLiteral) value);
    }


    protected override AbstractSqlCondition LessEqual(double value)
    {
        return LessEqual((SqlServerLiteral) value);
    }


    protected override AbstractSqlCondition LessEqual(long value)
    {
        return LessEqual((SqlServerLiteral) value);
    }


    protected override AbstractSqlCondition LessEqual(int value)
    {
        return LessEqual((SqlServerLiteral) value);
    }

    #endregion
}