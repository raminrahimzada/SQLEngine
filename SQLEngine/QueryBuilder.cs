using System;
using SQLEngine.Builders;

namespace SQLEngine
{
    public  class QueryBuilder:AbstractQueryBuilder
    {
        public void Select(Func<SelectQueryBuilder,SelectQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<SelectQueryBuilder>()));
        }

        public void IfElse(Func<IfElseQueryBuilder, IfElseQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<IfElseQueryBuilder>()));
        }

        public void If(string condition)
        {
            using (var t= new IfElseQueryBuilder())
            {
                Writer.WriteLine(t.If(condition));
            }
        }

        public void If(Func<IfConditionBuilder, IfConditionBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<IfConditionBuilder>()));
        }

        public void ElseIf(string condition)
        {
            using (var t = new IfElseQueryBuilder())
            {
                Writer.WriteLine(t.ElseIf(condition));
            }
        }

        public void Else(string expression)
        {
            using (var t = new IfElseQueryBuilder())
            {
                Writer.WriteLine(t.Else(expression));
            }
        }

        public void Begin()
        {
            Writer.WriteLine(SQLKeywords.BEGIN);
            Writer.Indent++;
        }

        public void End()
        {
            Writer.Indent--;
            Writer.WriteLine(SQLKeywords.END);
        }

        public void Declare(Func<DeclarationQueryBuilder, DeclarationQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<DeclarationQueryBuilder>()));
        }

        public void Declare(string variableName, string type,string defaultValue=null)
        {
            using (var t = new DeclarationQueryBuilder())
            {
                Writer.WriteLine(t.Declare(variableName).OfType(type).Default(defaultValue));
            }
        }

        public void Set(Func<SetQueryBuilder, SetQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<SetQueryBuilder>()));
        }

        public void Set(string variable, string value)
        {
            using (var t = new SetQueryBuilder())
            {
                Writer.WriteLine(t.Set(variable).To(value));
            }
        }

        public void Execute(Func<ExecuteQueryBuilder, ExecuteQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<ExecuteQueryBuilder>()));
        }

        public void Insert(Func<InsertQueryBuilder, InsertQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<InsertQueryBuilder>()));
        }

        public void Update(Func<UpdateQueryBuilder, UpdateQueryBuilder> builder)
        {
            Writer.WriteLine(builder.Invoke(GetDefault<UpdateQueryBuilder>()));
        }
    }
}