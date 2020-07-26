using System;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class SqlServerColumn : AbstractSqlColumn
    {
        public SqlServerColumn(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return ToSqlString();
        }

        public override string ToSqlString()
        {
            if (Name == C.WILCARD) return Name;
            if (!Name.All(char.IsLetterOrDigit)) return "[" + Name + "]";
            return Name;
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
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(byte[] value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(DateTime value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(string value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(Guid value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(long value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(decimal value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(double value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition EqualTo(float value)
        {
            return EqualTo((AbstractSqlLiteral)value);
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
            return EqualTo((AbstractSqlLiteral)(SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(int value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(byte value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(byte[] value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(DateTime value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }
        protected override AbstractSqlCondition NotEqualTo(bool value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(string value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(Guid value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(long value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(decimal value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(double value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition NotEqualTo(float value)
        {
            return EqualTo((AbstractSqlLiteral)value);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " > " + otherColumn.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " >= " + otherColumn.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " < " + otherColumn.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " <= " + otherColumn.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " > " + value.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " >= " + value.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " < " + value.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " <= " + value.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition EqualTo(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " = " + variable.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " >= " + variable.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " > " + variable.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " < " + variable.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " <= " + variable.ToSqlString();
            return SqlServerCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(byte value)
        {
            return Greater((SqlServerLiteral) value);
        }

        protected override AbstractSqlCondition GreaterEqual(byte value)
        {
            return GreaterEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Less(byte value)
        {
            return Less((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(byte value)
        {
            return LessEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Greater(DateTime value)
        {
            return Greater((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(DateTime value)
        {
            return GreaterEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Less(DateTime value)
        {
            return Less((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(DateTime value)
        {
            return LessEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Greater(double value)
        {
            return Greater((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(double value)
        {
            return GreaterEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Less(double value)
        {
            return Less((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(double value)
        {
            return LessEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Greater(long value)
        {
            return Greater((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(long value)
        {
            return GreaterEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Less(long value)
        {
            return Less((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(long value)
        {
            return LessEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Greater(int value)
        {
            return Greater((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(int value)
        {
            return GreaterEqual((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition Less(int value)
        {
            return Less((SqlServerLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(int value)
        {
            return LessEqual((SqlServerLiteral)value);
        }

        public override AbstractSqlCondition Like(string expression,bool isUnicode=true)
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

        protected override AbstractSqlExpression Add(AbstractSqlColumn right)
        {
            return new SqlServerRawExpression(ToSqlString() + "+" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(AbstractSqlColumn right)
        {
            return new SqlServerRawExpression(ToSqlString() + "-" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(AbstractSqlColumn right)
        {
            return new SqlServerRawExpression(ToSqlString() + "/" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(AbstractSqlColumn right)
        {
            return new SqlServerRawExpression(ToSqlString() + "*" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Add(AbstractSqlLiteral right)
        {
            return new SqlServerRawExpression(ToSqlString() + "+" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(AbstractSqlLiteral right)
        {
            return new SqlServerRawExpression(ToSqlString() + "-" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(AbstractSqlLiteral right)
        {
            return new SqlServerRawExpression(ToSqlString() + "/" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(AbstractSqlLiteral right)
        {
            return new SqlServerRawExpression(ToSqlString() + "*" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Add(ISqlExpression right)
        {
            return new SqlServerRawExpression(ToSqlString() + "+" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(ISqlExpression right)
        {
            return new SqlServerRawExpression(ToSqlString() + "-" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(ISqlExpression right)
        {
            return new SqlServerRawExpression(ToSqlString() + "/" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(ISqlExpression right)
        {
            return new SqlServerRawExpression(ToSqlString() + "*" + right.ToSqlString());
        }
    }
}