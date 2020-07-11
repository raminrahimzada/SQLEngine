using System;

namespace SQLEngine
{
    public abstract class AbstractSqlLiteral: ISqlExpression
    {
        //protected abstract void SetFrom(byte[] data);

        //protected abstract void SetFrom(int i);
        //protected abstract void SetFrom(long l);
        //protected abstract void SetFrom(bool b);
        //protected abstract void SetFrom(string s, bool isUnicode = true);
        //protected abstract void SetFrom(double d);
        //protected abstract void SetFrom(decimal d);
        //protected abstract void SetFrom(float f);
        //protected abstract void SetFrom(short f);
        //protected abstract void SetFrom(DateTime dt,bool onlyDate=false);
        
        //protected abstract void SetFrom(int? i);
        //protected abstract void SetFrom(long? l);
        //protected abstract void SetFrom(bool? b);
        //protected abstract void SetFrom(double? d);
        //protected abstract void SetFrom(decimal? d);
        //protected abstract void SetFrom(float? f);
        //protected abstract void SetFrom(short? f);
        //protected abstract void SetFrom(DateTime? dt);
        public abstract string ToSqlString();

    }
}