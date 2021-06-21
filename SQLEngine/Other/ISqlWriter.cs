using System;

namespace SQLEngine
{
    public interface ISqlWriter:IDisposable
    {
        int Indent { get; set; }
        void Write(params string[] expressions);
        void Write(byte? b);
        void Write(int? b);
        void Write(char b);
        void WriteLine(string expression = null);
        string Build();
    }
}