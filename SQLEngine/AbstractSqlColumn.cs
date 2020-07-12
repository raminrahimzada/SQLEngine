using System;

namespace SQLEngine
{
#pragma warning disable 660,661
    public abstract class AbstractSqlColumn : ISqlExpression
#pragma warning restore 660,661
    {
        //TODO Refactor this, too much bottleneck codes here 
        public string Name { get; set; }
        public abstract string ToSqlString();

        protected abstract AbstractSqlCondition EqualTo(AbstractSqlColumn otherColumn);
        protected abstract AbstractSqlCondition EqualTo(AbstractSqlLiteral value);
        protected abstract AbstractSqlCondition EqualTo(int value);
        protected abstract AbstractSqlCondition EqualTo(bool value);
        protected abstract AbstractSqlCondition EqualTo(byte value);
        protected abstract AbstractSqlCondition EqualTo(byte[] value);
        protected abstract AbstractSqlCondition EqualTo(DateTime value);
        protected abstract AbstractSqlCondition EqualTo(string value);
        protected abstract AbstractSqlCondition EqualTo(Guid value);
        protected abstract AbstractSqlCondition EqualTo(long value);
        protected abstract AbstractSqlCondition EqualTo(decimal value);
        protected abstract AbstractSqlCondition EqualTo(double value);
        protected abstract AbstractSqlCondition EqualTo(float value);

        protected abstract AbstractSqlCondition NotEqualTo(AbstractSqlColumn otherColumn);
        protected abstract AbstractSqlCondition NotEqualTo(AbstractSqlVariable otherColumn);
        protected abstract AbstractSqlCondition NotEqualTo(AbstractSqlLiteral value);
        protected abstract AbstractSqlCondition NotEqualTo(int value);
        protected abstract AbstractSqlCondition NotEqualTo(byte value);
        protected abstract AbstractSqlCondition NotEqualTo(byte[] value);
        protected abstract AbstractSqlCondition NotEqualTo(DateTime value);
        protected abstract AbstractSqlCondition NotEqualTo(bool value);
        protected abstract AbstractSqlCondition NotEqualTo(string value);
        protected abstract AbstractSqlCondition NotEqualTo(Guid value);
        protected abstract AbstractSqlCondition NotEqualTo(long value);
        protected abstract AbstractSqlCondition NotEqualTo(decimal value);
        protected abstract AbstractSqlCondition NotEqualTo(double value);
        protected abstract AbstractSqlCondition NotEqualTo(float value);

        protected abstract AbstractSqlCondition Greater(AbstractSqlColumn otherColumn);
        protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlColumn otherColumn);
        protected abstract AbstractSqlCondition Less(AbstractSqlColumn otherColumn);

        protected abstract AbstractSqlCondition LessEqual(AbstractSqlColumn otherColumn);

        protected abstract AbstractSqlCondition Greater(AbstractSqlLiteral value);
        protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlLiteral value);
        protected abstract AbstractSqlCondition Less(AbstractSqlLiteral value);
        protected abstract AbstractSqlCondition LessEqual(AbstractSqlLiteral value);


        protected abstract AbstractSqlCondition EqualTo(AbstractSqlVariable variable);
        protected abstract AbstractSqlCondition GreaterEqual(AbstractSqlVariable variable);
        protected abstract AbstractSqlCondition Greater(AbstractSqlVariable variable);
        protected abstract AbstractSqlCondition Less(AbstractSqlVariable variable);
        protected abstract AbstractSqlCondition LessEqual(AbstractSqlVariable variable);

        protected abstract AbstractSqlCondition Greater(byte value);
        protected abstract AbstractSqlCondition GreaterEqual(byte value);
        protected abstract AbstractSqlCondition Less(byte value);
        protected abstract AbstractSqlCondition LessEqual(byte value);



        protected abstract AbstractSqlCondition Greater(DateTime value);
        protected abstract AbstractSqlCondition GreaterEqual(DateTime value);
        protected abstract AbstractSqlCondition Less(DateTime value);
        protected abstract AbstractSqlCondition LessEqual(DateTime value);


        protected abstract AbstractSqlCondition Greater(double value);
        protected abstract AbstractSqlCondition GreaterEqual(double value);
        protected abstract AbstractSqlCondition Less(double value);
        protected abstract AbstractSqlCondition LessEqual(double value);


        protected abstract AbstractSqlCondition Greater(long value);
        protected abstract AbstractSqlCondition GreaterEqual(long value);
        protected abstract AbstractSqlCondition Less(long value);
        protected abstract AbstractSqlCondition LessEqual(long value);

        protected abstract AbstractSqlCondition Greater(int value);
        protected abstract AbstractSqlCondition GreaterEqual(int value);
        protected abstract AbstractSqlCondition Less(int value);
        protected abstract AbstractSqlCondition LessEqual(int value);





        public static AbstractSqlCondition operator <(AbstractSqlColumn x, AbstractSqlColumn y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, AbstractSqlColumn y)
        {
            return x.LessEqual(y);
        }

        public static AbstractSqlCondition operator >(AbstractSqlColumn x, AbstractSqlColumn y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, AbstractSqlColumn y)
        {
            return x.GreaterEqual(y);
        }


        public static AbstractSqlCondition operator <(AbstractSqlColumn x, AbstractSqlLiteral y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, AbstractSqlLiteral y)
        {
            return x.LessEqual(y);
        }

        public static AbstractSqlCondition operator >(AbstractSqlColumn x, AbstractSqlLiteral y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, AbstractSqlLiteral y)
        {
            return x.GreaterEqual(y);
        }

        public static AbstractSqlCondition operator <(AbstractSqlColumn x, AbstractSqlVariable y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, AbstractSqlVariable y)
        {
            return x.LessEqual(y);
        }

        public static AbstractSqlCondition operator >(AbstractSqlColumn x, AbstractSqlVariable y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, AbstractSqlVariable y)
        {
            return x.GreaterEqual(y);
        }


        public static AbstractSqlCondition operator >(AbstractSqlColumn x, int y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, int y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, int y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, int y)
        {
            return x.LessEqual(y);
        }



        public static AbstractSqlCondition operator >(AbstractSqlColumn x, long y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, long y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, long y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, long y)
        {
            return x.LessEqual(y);
        }


        public static AbstractSqlCondition operator >(AbstractSqlColumn x, byte y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, byte y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, byte y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, byte y)
        {
            return x.LessEqual(y);
        }


        public static AbstractSqlCondition operator >(AbstractSqlColumn x, short y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, short y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, short y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, short y)
        {
            return x.LessEqual(y);
        }


        public static AbstractSqlCondition operator >(AbstractSqlColumn x, DateTime y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, DateTime y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, DateTime y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, DateTime y)
        {
            return x.LessEqual(y);
        }


        public static AbstractSqlCondition operator >(AbstractSqlColumn x, double y)
        {
            return x.Greater(y);
        }
        public static AbstractSqlCondition operator <(AbstractSqlColumn x, double y)
        {
            return x.Less(y);
        }
        public static AbstractSqlCondition operator >=(AbstractSqlColumn x, double y)
        {
            return x.GreaterEqual(y);
        }
        public static AbstractSqlCondition operator <=(AbstractSqlColumn x, double y)
        {
            return x.LessEqual(y);
        }




        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, AbstractSqlColumn otherColumn)
        {
            return column?.EqualTo(otherColumn);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, AbstractSqlColumn otherColumn)
        {
            return column?.NotEqualTo(otherColumn);
        }
        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, AbstractSqlVariable otherColumn)
        {
            return column?.NotEqualTo(otherColumn);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, AbstractSqlVariable otherColumn)
        {
            return column?.EqualTo(otherColumn);
        }


        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, AbstractSqlLiteral literal)
        {
            return column?.NotEqualTo(literal);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, AbstractSqlLiteral literal)
        {
            return column?.EqualTo(literal);
        }


        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, DateTime value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, DateTime value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, int value)
        {
            return column?.EqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, bool value)
        {
            return column?.EqualTo(value);
        }
        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, bool value)
        {
            return column?.NotEqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, int value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, long value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, long value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, decimal value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, double value)
        {
            return column?.NotEqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, float value)
        {
            return column?.NotEqualTo(value);
        }


        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, decimal value)
        {
            return column?.EqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, double value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, float value)
        {
            return column?.EqualTo(value);
        }





        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, short value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, short value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, byte value)
        {
            return column?.EqualTo(value);
        }
        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, byte[] value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, byte[] value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, byte value)
        {
            return column?.NotEqualTo(value);
        }
        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, Guid value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, Guid value)
        {
            return column?.NotEqualTo(value);
        }

        public static AbstractSqlCondition operator ==(AbstractSqlColumn column, string value)
        {
            return column?.EqualTo(value);
        }

        public static AbstractSqlCondition operator !=(AbstractSqlColumn column, string value)
        {
            return column?.NotEqualTo(value);
        }
    }
}