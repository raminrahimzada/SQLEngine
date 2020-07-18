using System;
using System.CodeDom.Compiler;

namespace SQLEngine.SqlServer
{
    
    internal class IfQueryBuilder : AbstractQueryBuilder, IIfQueryBuilder
    {
        private readonly AbstractSqlCondition _condition;

        public IfQueryBuilder(AbstractSqlCondition condition)
        {
            _condition = condition;
        }

        public override string Build()
        {
            Writer.Write(C.IF);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(_condition.ToSqlString());
            Writer.WriteLine(C.END_SCOPE);
            return base.Build();
        }
    }

    public class RawStringQueryBuilder : AbstractQueryBuilder, IElseIfQueryBuilder
    {
        private readonly Action<IndentedTextWriter> _func;

        public RawStringQueryBuilder(Action<IndentedTextWriter> func)
        {
            _func = func;
        }

        public override string Build()
        {
            _func(Writer);
            return base.Build();
        }
    }
    internal class ElseIfQueryBuilder : AbstractQueryBuilder, IElseIfQueryBuilder
    {
        private readonly AbstractSqlCondition _condition;

        public ElseIfQueryBuilder(AbstractSqlCondition condition)
        {
            _condition = condition;
        }

        public override string Build()
        {
            Writer.Write(C.ELSE);
            Writer.Write(C.SPACE);
            Writer.Write(C.IF);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(_condition.ToSqlString());
            Writer.WriteLine(C.END_SCOPE);
            return base.Build();
        }
    }
    internal class SetQueryBuilder : AbstractQueryBuilder, ISetNeedSetQueryBuilder, ISetNeedToQueryBuilder, 
        ISetNoSetNoToQueryBuilder
    {
        private AbstractSqlVariable _variable;
        private ISqlExpression _value;
        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //TODO
        }
        public ISetNeedToQueryBuilder Set(AbstractSqlVariable variable)
        {
            _variable = variable;
            return this;
        }

        public ISetNoSetNoToQueryBuilder To(ISqlExpression value)
        {
            _value = value;
            return this;
        }

        public override string Build()
        {
            Writer.Write(C.SET);
            Writer.Write2();
            Writer.Write(_variable.ToSqlString());
            Writer.Write(C.SPACE);
            Writer.Write2(C.EQUALS);
            Writer.Write(_value.ToSqlString());
            Writer.Write(C.SEMICOLON);
            return base.Build();
        }
    }
}