﻿using System;

namespace SQLEngine
{
    public interface ISqlWriter:IDisposable
    {
        int Indent { get; set; }
        void Write(string expression);
        void Write(params string[] expressions);
        void Write(byte? b);
        void Write(int? b);
        void Write(char b);
        void WriteLine(string expression);
        void WriteLine(char? expression = null);
        string Build();
    }
}