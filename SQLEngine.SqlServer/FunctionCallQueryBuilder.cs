using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class FunctionCallQueryBuilder : AbstractQueryBuilder, IFunctionCallNeedNameQueryBuilder, IFunctionCallNeedArgQueryBuilder
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
        //public IFunctionCallNeedArgQueryBuilder Arg(Func<FunctionCallQueryBuilder, FunctionCallQueryBuilder> builder)
        //{
        //    using (var f = GetDefault<FunctionCallQueryBuilder>())
        //    {
        //        var expression = builder(f);
        //        _arguments.Add(expression);
        //    }
        //    return this;
        //}
        public override void Build(ISqlWriter writer)
        {
            writer.Write(_functionName);//do not use I here
            writer.Write(C.BEGIN_SCOPE);
            writer.WriteJoined(_arguments);
            writer.Write(C.END_SCOPE);
        }
    }
}