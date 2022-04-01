using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace SQLEngine.SqlServer;

public partial class SqlServerQueryBuilder : IQueryBuilder
{
    [Pure]
    public ISqlExpression Cast(ISqlExpression expression, string asType)
    {
        return RawInternal($"CAST({expression.ToSqlString()} AS {asType})");
    }

    #region SubString

    [Pure]
    public AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start,
        ISqlExpression length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    [Pure]
    public AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, AbstractSqlLiteral length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    [Pure]
    public AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, ISqlExpression length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    [Pure]
    public AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start, AbstractSqlLiteral length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    [Pure]
    public AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, ISqlExpression length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    [Pure]
    public AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, AbstractSqlLiteral length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }
    [Pure]
    public AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, ISqlExpression length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }
    [Pure]
    public AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, AbstractSqlLiteral length)
    {
        return RawInternal($"SUBSTRING({expression.ToSqlString()},{start.ToSqlString()},{length.ToSqlString()})");
    }

    #endregion

    #region Column

    [Pure]
    public AbstractSqlColumn Column(string columnName)
    {
        return new SqlServerColumn(columnName);
    }
        
    [Pure]
    public AbstractSqlColumn Column(string columnName, string tableAlias)
    {
        return new SqlServerColumnWithTableAlias(columnName, tableAlias);
    }

    #endregion

    #region Literal
    [Pure]
    public AbstractSqlLiteral Literal(DateTime? x, bool includeTime = true)
    {
        return SqlServerLiteral.From(x, includeTime);
    }

    [Pure]
    public AbstractSqlLiteral Literal(AbstractSqlLiteral literal)
    {
        return literal;
    }

    [Pure]
    public AbstractSqlLiteral Literal(string x, bool isUniCode)
    {
        return AbstractSqlLiteral.From(x, isUniCode);
    }

    [Pure]
    public AbstractSqlLiteral Literal(DateTime x, bool includeTime)
    {
        return SqlServerLiteral.From(x, includeTime);
    }


    #endregion

    #region Buildin Functions

    [Pure]
    public AbstractSqlExpression Now()
    {
        return RawInternal("GETDATE()");
    }

    [Pure]
    public AbstractSqlExpression Rand()
    {
        return RawInternal("RAND()");
    }

    [Pure]
    public ISqlExpression Len(ISqlExpression expression)
    {
        return RawInternal($"LEN({expression.ToSqlString()})");
    }


    #endregion

    #region Raw

    [Pure]
    public AbstractSqlExpression CallFunc(string functionName, string schema = null)
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(schema))
        {
            sb.Append(schema);
            sb.Append(C.DOT);
        }

        sb.Append(functionName);
        sb.Append(C.BEGIN_SCOPE);
        sb.Append(C.END_SCOPE);
        return RawInternal(sb.ToString());
    }

    [Pure]
    public AbstractSqlExpression CallFunc(string functionName,string schema=null,params ISqlExpression[] expressions)
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(schema))
        {
            sb.Append(schema);
            sb.Append(C.DOT);
        }

        sb.Append(functionName);
        sb.Append(C.BEGIN_SCOPE);
        sb.AppendJoin(C.COMMA, expressions.Select(x => x.ToSqlString()).ToArray());
        sb.Append(C.END_SCOPE);
        return RawInternal(sb.ToString());
    }

    [Pure]
    public AbstractSqlExpression CallFunc(string functionName,string schema=null,params AbstractSqlLiteral[] expressions)
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(schema))
        {
            sb.Append(schema);
            sb.Append(C.DOT);
        }

        sb.Append(functionName);
        sb.Append(C.BEGIN_SCOPE);
        sb.AppendJoin(C.COMMA, expressions.Select(x => x.ToSqlString()).ToArray());
        sb.Append(C.END_SCOPE);
        return RawInternal(sb.ToString());
    }

    [Pure]
    public AbstractSqlExpression CallFunc(string functionName,string schema=null,params AbstractSqlExpression[] expressions)
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(schema))
        {
            sb.Append(schema);
            sb.Append(C.DOT);
        }

        sb.Append(functionName);
        sb.Append(C.BEGIN_SCOPE);
        sb.AppendJoin(C.COMMA, expressions.Select(x => x.ToSqlString()).ToArray());
        sb.Append(C.END_SCOPE);
        return RawInternal(sb.ToString());
    }

    [Pure]
    public AbstractSqlExpression Raw(string rawSqlExpression)
    {
        return new SqlServerRawExpression(rawSqlExpression);
    }
    [Pure]
    public AbstractSqlCondition RawCondition(string rawConditionQuery)
    {
        return new SqlServerCondition(rawConditionQuery);
    }
    [Pure]
    public AbstractSqlExpression RawInternal(string rawSqlExpression)
    {
        return new SqlServerRawExpression(rawSqlExpression);
    }

    #endregion
}