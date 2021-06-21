using System;

namespace SQLEngine
{
   
    public abstract class AbstractSqlLiteral:ISqlExpression
    {
        public abstract string ToSqlString();

        protected static Func<AbstractSqlLiteral> CreateEmpty;

        public abstract void SetFrom(byte[] data);

        public abstract void SetFrom(int i);
        public abstract void SetFrom(DateTimeOffset dto);
        public abstract void SetFrom(DateTimeOffset? dto);
        public abstract void SetFrom(Enum e);
        public abstract void SetFrom(byte b);
        public abstract void SetFrom(byte? b);
        public abstract void SetFrom(sbyte? sb);
        public abstract void SetFrom(Guid g);
        public abstract void SetFrom(Guid? g);
        public abstract void SetFrom(long l);
        public abstract void SetFrom(bool b);
        public abstract void SetFrom(string s, bool isUnicode = true);
        public abstract void SetFrom(double d);
        public abstract void SetFrom(decimal d);
        public abstract void SetFrom(float f);
        public abstract void SetFrom(short s);
        public abstract void SetFrom(sbyte sb);
        public abstract void SetFrom(ushort us);
        public abstract void SetFrom(DateTime dt, bool includeTime = true);
        public abstract void SetFrom(char ch);

        public abstract void SetFrom(int? i);
        public abstract void SetFrom(ushort? us);
        public abstract void SetFrom(long? l);
        public abstract void SetFrom(bool? b);
        public abstract void SetFrom(double? d);
        public abstract void SetFrom(ulong? ul);
        public abstract void SetFrom(uint? ui);
        public abstract void SetFrom(decimal? d);
        public abstract void SetFrom(float? f);
        public abstract void SetFrom(short? sh);
        public abstract void SetFrom(char? ch);
        public abstract void SetFrom(DateTime? dt);

        public abstract void SetFrom(ulong ul);
        public abstract void SetFrom(uint ui);



        public static implicit operator AbstractSqlLiteral(int? x)
        {
            return From(x);
        }

        public static implicit operator AbstractSqlLiteral(byte? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(short? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(sbyte? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(uint? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(ulong? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(long? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(DateTime? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(DateTimeOffset? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(DateTimeOffset x)
        {
            return From(x);
        }




        public static implicit operator AbstractSqlLiteral(int x)
        {
            return From(x);
        }

        public static implicit operator AbstractSqlLiteral(Enum x)
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
        public static implicit operator AbstractSqlLiteral(bool? x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(decimal? x)
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
        public static implicit operator AbstractSqlLiteral(sbyte x)
        {
            return From(x);
        }

        public static implicit operator AbstractSqlLiteral(ushort? x)
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

        public static implicit operator AbstractSqlLiteral(ushort x)
        {
            return From(x);
        }
        
        public static implicit operator AbstractSqlLiteral(byte x)
        {
            return From(x);
        }
        public static implicit operator AbstractSqlLiteral(byte[] x)
        {
            return From(x);
        }



        public static AbstractSqlLiteral From(int i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(Enum i)
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
        public static AbstractSqlLiteral From(sbyte i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(ushort i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(char i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(char? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(DateTimeOffset? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(DateTimeOffset i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(DateTime? i)
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
        public static AbstractSqlLiteral From(ushort? i)
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
        public static AbstractSqlLiteral From(ulong? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(uint? i)
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
        public static AbstractSqlLiteral From(sbyte? i)
        {
            var literal = CreateEmpty();
            literal.SetFrom(i);
            return literal;
        }
        public static AbstractSqlLiteral From(params byte[] i)
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