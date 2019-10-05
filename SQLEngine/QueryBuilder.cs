using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using SQLEngine.Builders;

namespace SQLEngine
{
    public  class QueryBuilder:AbstractQueryBuilder
    {
        private readonly IndentedTextWriter _writer;

        public QueryBuilder()
        {
            var sb = new StringBuilder();
            var sw=new StringWriter(sb);
            _writer = new IndentedTextWriter(sw);
            
        }
       
        public void Select(Func<SelectQueryBuilder,SelectQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }
        public void IfElse(Func<IfElseQueryBuilder, IfElseQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }
        public void If(string condition)
        {
            throw new NotImplementedException();
        }
        public void If(Func<IfConditionBuilder, IfConditionBuilder> builder)
        {
            throw new NotImplementedException();
        }
        public void ElseIf(string condition)
        {
            throw new NotImplementedException();
        }
        public void Else( )
        {
            throw new NotImplementedException();
        }
        public void Begin()
        {
            throw new NotImplementedException();
        }
        public void End( )
        {
            throw new NotImplementedException();
        }
        public void Declare(string variableName, string type)
        {
            throw new NotImplementedException();
        }
        public void Declare(string variableName, string type,string defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Set(string variable, string value)
        {
            throw new NotImplementedException();
        }

        public void Execute(Func<ExecuteQueryBuilder, ExecuteQueryBuilder> builder)
        {
        }

        public void Insert(Func<InsertQueryBuilder, InsertQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }
        public void Update(Func<UpdateQueryBuilder, UpdateQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }

        
    }
}