using System;

namespace SQLEngine
{
    public interface IAbstractSqlLiteral
    {
        void SetFrom(byte[] data);
        void SetFrom(int i);
        void SetFrom(long l);
        void SetFrom(bool b);
        void SetFrom(string s, bool isUnicode = true);
        void SetFrom(double d);
        void SetFrom(decimal d);
        void SetFrom(float f);
        void SetFrom(short f);
        void SetFrom(DateTime dt, bool includeTime = true);
        void SetFrom(int? i);
        void SetFrom(long? l);
        void SetFrom(bool? b);
        void SetFrom(double? d);
        void SetFrom(decimal? d);
        void SetFrom(float? f);
        void SetFrom(short? f);
        void SetFrom(DateTime? dt);
        void SetFrom(ulong l);
        void SetFrom(uint l);
        string ToSqlString();
    }

    public abstract class AbstractSqlLiteral: ISqlExpression, IAbstractSqlLiteral
    {
        protected static Func<AbstractSqlLiteral> CreateEmpty;

        public abstract void SetFrom(byte[] data);

        public abstract void SetFrom(int i);
        public abstract void SetFrom(Guid i);
        public abstract void SetFrom(Guid? i);
        public abstract void SetFrom(long l);
        public abstract void SetFrom(bool b);
        public abstract void SetFrom(string s, bool isUnicode = true);
        public abstract void SetFrom(double d);
        public abstract void SetFrom(decimal d);
        public abstract void SetFrom(float f);
        public abstract void SetFrom(short f);
        public abstract void SetFrom(DateTime dt, bool includeTime = true);

        public abstract void SetFrom(int? i);
        public abstract void SetFrom(long? l);
        public abstract void SetFrom(bool? b);
        public abstract void SetFrom(double? d);
        public abstract void SetFrom(decimal? d);
        public abstract void SetFrom(float? f);
        public abstract void SetFrom(short? f);
        public abstract void SetFrom(DateTime? dt);

        public abstract void SetFrom(ulong l);
        public abstract void SetFrom(uint l);
        public abstract string ToSqlString();








        public static implicit operator AbstractSqlLiteral(int x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(ulong x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(uint x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(char x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(char? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(double x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(decimal x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(bool x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(DateTime x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(long x)
        {
            return From(x);
        }

        public static implicit operator AbstractSqlLiteral(Guid x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(Guid? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(string x)
        {
            return From(x, true);
        }
        public static implicit operator AbstractSqlLiteral(short x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(byte x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(byte[] x)
        {
            return (AbstractSqlLiteral)From(x);
        }



        public static AbstractSqlLiteral From(int i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(short i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(long i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(ulong i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(uint i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(byte i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(int? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(short? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(double? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(double i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(decimal i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(Guid i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(Guid? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(bool i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(DateTime i,bool includeTime=true)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i, includeTime);
            return literal;
        }
        public static AbstractSqlLiteral From(bool? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(decimal? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(long? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(byte? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(byte[] i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(string str, bool isUnicode = true)
        {
            var literal = CreateEmpty();
            literal.SetFrom(str, isUnicode);
            return literal;
        }
    }
}