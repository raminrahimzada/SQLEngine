﻿using System;
using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class FunctionCallQueryBuilder : SqlServerQueryBuilder, IFunctionCallNeedNameQueryBuilder, IFunctionCallNeedArgQueryBuilder
    {
        private string _functionName;
        private readonly List<string> _arguments;

        public FunctionCallQueryBuilder()
        {
            _arguments = new List<string>();
        }
        public IFunctionCallNeedArgQueryBuilder Call(string functionName)
        {
            _functionName = functionName;
            return this;
        }
        public IFunctionCallNeedArgQueryBuilder Arg(string argument)
        {
            _arguments.Add(argument);
            return this;
        }
        public IFunctionCallNeedArgQueryBuilder Arg(Func<FunctionCallQueryBuilder, FunctionCallQueryBuilder> builder)
        {
            using (var f = GetDefault<FunctionCallQueryBuilder>())
            {
                var expression = builder(f).Build();
                _arguments.Add(expression);
            }
            return this;
        }
        public override string Build()
        {
            Writer.Write(_functionName);//do not use I here
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.WriteJoined(_arguments);
            Writer.Write(SQLKeywords.END_SCOPE);
            return base.Build();
        }
    }
}