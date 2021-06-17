﻿using System;
using System.Linq;

namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlColumn : AbstractSqlColumn
    {
        public PostgreSqlColumn(string name)
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

        protected override AbstractSqlCondition EqualTo(AbstractSqlExpression expression)
        {
            var rawSqlString = ToSqlString() + " = " + expression.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition EqualTo(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " = " + otherColumn.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition EqualTo(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " = " + value.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
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

        protected override AbstractSqlCondition NotEqualTo(AbstractSqlExpression expression)
        {
            var rawSqlString = ToSqlString() + " <> " + expression.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition NotEqualTo(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " <> " + otherColumn.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }
        protected override AbstractSqlCondition NotEqualTo(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " <> " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition NotEqualTo(AbstractSqlLiteral value)
        {
            return EqualTo((AbstractSqlLiteral)(PostgreSqlLiteral)value);
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
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " >= " + otherColumn.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " < " + otherColumn.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlColumn otherColumn)
        {
            var rawSqlString = ToSqlString() + " <= " + otherColumn.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " > " + value.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " >= " + value.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " < " + value.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlLiteral value)
        {
            var rawSqlString = ToSqlString() + " <= " + value.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition EqualTo(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " = " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition GreaterEqual(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " >= " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " > " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Less(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " < " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition LessEqual(AbstractSqlVariable variable)
        {
            var rawSqlString = ToSqlString() + " <= " + variable.ToSqlString();
            return PostgreSqlCondition.Raw(rawSqlString);
        }

        protected override AbstractSqlCondition Greater(byte value)
        {
            return Greater((PostgreSqlLiteral) value);
        }

        protected override AbstractSqlCondition GreaterEqual(byte value)
        {
            return GreaterEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Less(byte value)
        {
            return Less((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(byte value)
        {
            return LessEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Greater(DateTime value)
        {
            return Greater((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(DateTime value)
        {
            return GreaterEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Less(DateTime value)
        {
            return Less((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(DateTime value)
        {
            return LessEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Greater(double value)
        {
            return Greater((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(double value)
        {
            return GreaterEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Less(double value)
        {
            return Less((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(double value)
        {
            return LessEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Greater(long value)
        {
            return Greater((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(long value)
        {
            return GreaterEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Less(long value)
        {
            return Less((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(long value)
        {
            return LessEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Greater(int value)
        {
            return Greater((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition GreaterEqual(int value)
        {
            return GreaterEqual((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition Less(int value)
        {
            return Less((PostgreSqlLiteral)value);
        }

        protected override AbstractSqlCondition LessEqual(int value)
        {
            return LessEqual((PostgreSqlLiteral)value);
        }

        public override AbstractSqlCondition Like(string expression,bool isUnicode=true)
        {
            return PostgreSqlCondition.Raw(
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
            return PostgreSqlCondition.Raw(
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
            return PostgreSqlCondition.Raw(
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
            var expression = "(" + this.ToSqlString() + " BETWEEN " + from.ToSqlString() + " AND " +
                             to.ToSqlString() + ")";

            return PostgreSqlCondition.Raw(expression);
        }
        public override AbstractSqlCondition Between(ISqlExpression from, ISqlExpression to)
        {
            var expression = "(" + this.ToSqlString() + " BETWEEN " + from.ToSqlString() + " AND " +
                             to.ToSqlString() + ")";

            return PostgreSqlCondition.Raw(expression);
        }
        public override AbstractSqlCondition In(Action<ISelectQueryBuilder> builderFunc)
        {
            var writer = SqlWriter.New;
            writer.Write(this.ToSqlString());
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

            return PostgreSqlCondition.Raw(writer.Build());
        }
        public override AbstractSqlCondition In(params AbstractSqlLiteral[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -IN query needed");
            }
            var expression = this.ToSqlString() + " IN (" +
                             string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

            return PostgreSqlCondition.Raw(expression);
        }
        public override AbstractSqlCondition NotIn(params AbstractSqlLiteral[] expressions)
        {
            if (expressions.Length == 0)
            {
                throw new Exception("At least one element needed in -NOT IN query needed");
            }
            var expression = this.ToSqlString() + " NOT IN (" +
                             string.Join(",", expressions.Select(x => x.ToSqlString())) + ")";

            return PostgreSqlCondition.Raw(expression);
        }
        public override AbstractSqlCondition NotIn(Action<ISelectQueryBuilder> builderFunc)
        {
            var writer = SqlWriter.New;
            writer.Write(this.ToSqlString());
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

            return PostgreSqlCondition.Raw(writer.Build());
        }
        protected override AbstractSqlExpression Add(AbstractSqlColumn right)
        {
            return new PostgreSqlRawExpression(ToSqlString() ,C.PLUS, right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(AbstractSqlColumn right)
        {
            return new PostgreSqlRawExpression(ToSqlString() ,C.MINUS, right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(AbstractSqlColumn right)
        {
            return new PostgreSqlRawExpression(ToSqlString() ,C.DIVIDE, right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(AbstractSqlColumn right)
        {
            return new PostgreSqlRawExpression(ToSqlString() ,C.MULTIPLY, right.ToSqlString());
        }

        protected override AbstractSqlExpression Add(AbstractSqlLiteral right)
        {
            return new PostgreSqlRawExpression(ToSqlString(),C.ADD,right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(AbstractSqlLiteral right)
        {
            return new PostgreSqlRawExpression(ToSqlString(), C.MINUS, right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(AbstractSqlLiteral right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "/" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(AbstractSqlLiteral right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "*" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Add(AbstractSqlExpression right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "+" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Subtract(AbstractSqlExpression right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "-" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Divide(AbstractSqlExpression right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "/" + right.ToSqlString());
        }

        protected override AbstractSqlExpression Multiply(AbstractSqlExpression right)
        {
            return new PostgreSqlRawExpression(ToSqlString() + "*" + right.ToSqlString());
        }
    }
}