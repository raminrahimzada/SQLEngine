using System;
using System.Linq;
using System.Text;
    
namespace SQLEngine.SqlServer
{
    public class SqlServerConditionFilterQueryHelper : IConditionFilterQueryHelper
    {
        public string IsNull(ISqlExpression key)
        {
            return $"{key} IS NULL";
        }
        public string IsNotNull(ISqlExpression key)
        {
            return $"{key} IS NOT NULL";
        }

        public string ColumnEquals(string columnName, ISqlExpression value)
        {
            var col=new SqlServerColumn(columnName);
            if (String.Equals(value.ToString(), SQLKeywords.NULL, StringComparison.InvariantCultureIgnoreCase))
            {
                return IsNull(col);
            }
            return $"{col} = {value}";
        }

        public string Equal(ISqlExpression key, ISqlExpression value)
        {
            if (String.Equals(value.ToString(), SQLKeywords.NULL, StringComparison.InvariantCultureIgnoreCase))
            {
                return IsNull(key);
            }
            return $"{key} = {value}";
        }
        public string NotEqual(ISqlExpression key, ISqlExpression value)
        {
            if (String.Equals(value.ToString(), SQLKeywords.NULL, StringComparison.InvariantCultureIgnoreCase))
            {
                return IsNotNull(key);
            }
            return $"{key} <> {value}";
        }
        public string GreaterThan(ISqlExpression key, ISqlExpression value)
        {
            return $"{key} > {value}";
        }
        public string ColumnGreaterThan(string columnName, ISqlExpression value)
        {
            var col=new SqlServerColumn(columnName);
            return $"{col} > {value}";
        }
        public string ColumnLessThan(string columnName, ISqlExpression value)
        {
            var col=new SqlServerColumn(columnName);
            return $"{col} < {value}";
        }
        public string GreaterEqualThan(ISqlExpression key, ISqlExpression value)
        {
            return $"{key} >= {value}";
        }
        public string LessThan(ISqlExpression key, ISqlExpression value)
        {
            return $"{key} < {value}";
        }
        //public string Equal(string key, ISqlString value, string alias)
        //{
        //    return $"{alias}.{key} = {value}";
        //}

        public string BetWeen(ISqlExpression expression, ISqlExpression starting, ISqlExpression ending)
        {
            return $"{expression} BETWEEN {starting} AND {ending}";
        }

        public string And(params string[] filters)
        {
            return string.Join("AND", filters.Select(delegate(string f)
            {
                if (string.IsNullOrEmpty(f)) return "(-1=0)"; //
                return $"({f})";
            }));
        }
        public string Or(params string[] filters)
        {
            return string.Join("OR", filters.Select(delegate(string f)
            {
                if (string.IsNullOrEmpty(f)) return "(-1=0)"; //
                return $"({f})";
            }));
        }
        public string As(string tableName, string columnName, string alias)
        {
            return $"{tableName}.{columnName}] AS {alias}";
        }

      
        public string Map(string columnName, string alias)
        {
            return $"{columnName} AS {alias}";
        }

        public string Top(int count, string selection = "*")
        {
            return $"TOP({count}) {selection}";
        }

        public string True => "(1=1)";
        public string False => "(1=0)";

        public string In(string columnName, params string[] values)
        {
            if (values == null || values.Length == 0) return False;

            var sb = new StringBuilder();
            sb.Append("" + columnName + " IN (");
            sb.Append(string.Join(",", values));
            sb.Append(")");
            return sb.ToString();
        }
        public string NotIn(string columnName, params string[] values)
        {
            if (values == null || values.Length == 0) return True;
            var sb = new StringBuilder();
            sb.Append("" + columnName + " NOT IN (");
            sb.Append(string.Join(",", values));
            sb.Append(SQLKeywords.END_SCOPE);
            return sb.ToString();
        }

        public string Exists(string selection)
        {
            return string.Concat(SQLKeywords.EXISTS, SQLKeywords.BEGIN_SCOPE, selection,
                SQLKeywords.END_SCOPE);
        }

        public string NotExists(string selection)
        {
            return string.Concat(SQLKeywords.NOT, SQLKeywords.SPACE, SQLKeywords.EXISTS, SQLKeywords.BEGIN_SCOPE, selection,
                SQLKeywords.END_SCOPE);
        }

        public string LessThanOrEqual(string left, string right)
        {
            return $"{left}<={right}";
        }

        public string NotLike(string expression, string regex, string escape)
        {
            var sb = new StringBuilder();
            sb.Append(expression);
            sb.Append(SQLKeywords.SPACE);
            sb.Append(SQLKeywords.NOT);
            sb.Append(SQLKeywords.SPACE);
            sb.Append(SQLKeywords.LIKE);
            sb.Append(SQLKeywords.SPACE);
            sb.Append(regex.ToSQL());
            if (!string.IsNullOrEmpty(escape))
            {
                sb.Append(SQLKeywords.ESCAPE);
                sb.Append(SQLKeywords.SPACE);
                sb.Append(escape.ToSQL());
                sb.Append(SQLKeywords.SPACE);
            }

            return sb.ToString();
        }
        public string Like(string expression, string regex, string escape)
        {
            var sb = new StringBuilder();
            sb.Append(expression);
            sb.Append(SQLKeywords.SPACE);
            sb.Append(SQLKeywords.LIKE);
            sb.Append(SQLKeywords.SPACE);
            sb.Append(regex.ToSQL());
            if (!string.IsNullOrEmpty(escape))
            {
                sb.Append(SQLKeywords.ESCAPE);
                sb.Append(SQLKeywords.SPACE);
                sb.Append(escape.ToSQL());
                sb.Append(SQLKeywords.SPACE);
            }

            return sb.ToString();
        }

        public string Call(string functionName, params string[] parameters)
        {
            return $"{functionName}({string.Join(",", parameters)})";
        }

        public string Cast(ISqlExpression expression, string asType)
        {
            return string.Concat(SQLKeywords.CAST, SQLKeywords.BEGIN_SCOPE, expression, SQLKeywords.SPACE,
                SQLKeywords.AS, SQLKeywords.SPACE, asType, SQLKeywords.END_SCOPE);
        }
    }
}