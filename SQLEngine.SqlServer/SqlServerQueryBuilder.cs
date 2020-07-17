using System;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles

namespace SQLEngine.SqlServer
{
    public class SqlServerQueryBuilder : AbstractQueryBuilder, IQueryBuilder
    {
        public ISelectQueryBuilder _select => new SelectQueryBuilder();
        public IUpdateQueryBuilder _update => new UpdateQueryBuilder();
        public IDeleteQueryBuilder _delete => new DeleteQueryBuilder();
        public IInsertQueryBuilder _insert => new InsertQueryBuilder();
        public IAlterQueryBuilder _alter => new AlterQueryBuilder();
        
        public ICreateQueryBuilder _create => new CreateQueryBuilder();
        
        public IDropQueryBuilder _drop => new DropQueryBuilder();


        public IConditionFilterQueryHelper Helper { get; } = new SqlServerConditionFilterQueryHelper();


        public void Create(Func<ICreateQueryBuilder, ICreateTableQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<CreateQueryBuilder>()).Build());
        }
        public void Create(Func<ICreateQueryBuilder, IAbstractCreateFunctionQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<CreateQueryBuilder>()).Build());
        }


        public void Select(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<SelectQueryBuilder>()).Build());
        }

        public void Union()
        {
            Writer.WriteLineEx(C.UNION);
        }

        public void UnionAll()
        {
            Writer.WriteLine(C.UNION);
            Writer.WriteLine(C.SPACE);
            Writer.WriteLine(C.ALL);
        }

        public void Truncate(string tableName)
        {
            Writer.WriteLineEx(GetDefault<TruncateQueryBuilder>().Table(tableName).Build());
        }

        public void IfOr(params string[] conditions)
        {
            If(Helper.Or(conditions));
        }
        public void IfAnd(params string[] conditions)
        {
            If(Helper.And(conditions));
        }
        public void If(string condition)
        {
            Writer.Write(C.IF);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(condition);
            Writer.WriteLine(C.END_SCOPE);
        }

        public void ElseIf(string condition)
        {
            Writer.Write(C.ELSE);
            Writer.Write(C.SPACE);
            Writer.Write(C.IF);
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(condition);
            Writer.WriteLine(C.END_SCOPE);
        }

        public void Else()
        {
            Writer.WriteLine(C.ELSE);
        }

        public void Begin()
        {
            Writer.WriteLine(C.BEGIN);
            Writer.Indent++;
        }

        public void AddExpression(string expression)
        {
            Writer.WriteLineEx(expression);
        }

        public void End()
        {
            Writer.Indent--;
            Writer.WriteLine(C.END);
        }

        public void Declare(Func<IDeclarationQueryBuilder, IDeclarationQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<DeclarationQueryBuilder>()).Build());
        }

        public AbstractSqlVariable DeclareRandom(string variableName, string type, AbstractSqlLiteral defaultValue = null)
        {
            variableName = GenerateRandomVariableName(variableName.ToLowerInvariant());

            return Declare(variableName, type, defaultValue);
        }

         

        public AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue = null)
        {
            using (var t = new DeclarationQueryBuilder())
            {
                Writer.WriteLineEx(t.Declare(variableName).OfType(type).Default(defaultValue?.ToString()).Build());
                return new SqlServerVariable(variableName);
            }
        }

        public void SetToScopeIdentity(AbstractSqlVariable variable)
        {
            Set(variable, SqlServerLiteral.Raw("SCOPE_IDENTITY()"));
        }

        //public void Set(ISqlVariable variable, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right)
        //{
        //    using (var t = new SetQueryBuilder())
        //    {
        //        Writer.WriteLineEx(t.Set(variable).To(right(GetDefault<BinaryExpressionBuilder>()).Build()).Build());
        //    }
        //}
        public void Set(AbstractSqlVariable variable, ISqlExpression value)
        {
            using (var t = new SetQueryBuilder())
            {
                Writer.WriteLineEx(t.Set(variable).To(value).Build());
            }
        } 
        public void Set(AbstractSqlVariable variable, AbstractSqlVariable value)
        {
            using (var t = new SetQueryBuilder())
            {
                Writer.WriteLineEx(t.Set(variable).To(value).Build());
            }
        }
        public void Set(AbstractSqlVariable variable, AbstractSqlLiteral value)
        {
            using (var t = new SetQueryBuilder())
            {
                Writer.WriteLineEx(t.Set(variable).To(value).Build());
            }
        }
        //public void Set(ISqlVariable variable, Func<ICastQueryBuilder, ICastQueryBuilder> q)
        //{
        //    using (var t = new SetQueryBuilder())
        //    {
        //        using (var c = new CastQueryBuilder())
        //        {
        //            ICastQueryBuilder temp = q(c);
        //            ISqlExpression x;
        //            Writer.WriteLineEx(t.Set(variable).To(x).Build());
        //        }
        //    }
        //}
        public void Execute(Func<IExecuteQueryBuilder, IExecuteProcedureNeedArgQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<ExecuteQueryBuilder>()).Build());
        }

        public void Insert(Func<IInsertQueryBuilder, IAbstractInsertQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<InsertQueryBuilder>()).Build());
        }

        public void Update(Func<IUpdateQueryBuilder, IAbstractUpdateQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<UpdateQueryBuilder>()).Build());
        }

        public void Delete(Func<IDeleteQueryBuilder, IDeleteQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<DeleteQueryBuilder>()).Build());
        }

        public void Return()
        {
            Writer.Write(C.RETURN);
            Writer.WriteLine(C.SEMICOLON);
        }
        public void Return(string sql)
        {
            Writer.Write(C.RETURN);
            Writer.WriteWithScoped(sql);
            Writer.WriteLine();
        }
        public void Return(ISqlExpression expression)
        {
            Writer.Write(C.RETURN);
            Writer.WriteWithScoped(expression.ToSqlString());
            Writer.WriteLine();
        }

        public void Comment(string comment)
        {
            if (!string.IsNullOrEmpty(comment))
            {
                comment = comment.Replace("*/", "*\\/");
            }
            Writer.Write("/*");
            Writer.WriteEx(comment);
            Writer.Write("*/ ");
            Writer.WriteLine("");
            //Writer.WriteLine("PRINT(" + comment.ToSQL() + ");");
        }

        private static readonly object Sync=new object();
        private static long _randomFeed = 1;

        public string GenerateRandomVariableName(string beginning)
        {
            lock (Sync)
            {
                _randomFeed++;
                return beginning.ToLowerInvariant() + "__" + _randomFeed;// + Guid.NewGuid().ToString().Replace("-", "_").ToLowerInvariant();
            }
        }


        public void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameNoSchemaNoDBQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<DropTableQueryBuilder>()).Build());
        }

        public void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<DropTableQueryBuilder>()).Build());
        }
        public void Drop(Func<IDropViewQueryBuilder, IDropViewQueryBuilder> builder)
        {
            Writer.WriteLineEx(builder.Invoke(GetDefault<DropViewQueryBuilder>()).Build());
        }

        public void Cursor(string selection,string[] intoVariables,Action<IQueryBuilder> body)
        {
            var variableName = "__cursor_" + Guid.NewGuid().ToString().Replace("-", "");
            Writer.Write(C.DECLARE);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            Writer.Write(C.SPACE);
            Writer.Write(C.CURSOR);
            Writer.Write(C.SPACE);
            Writer.Write(C.FOR);
            Writer.Write(C.SPACE);
            Writer.Write(selection);
            Writer.WriteLine();
            
            Writer.Write(C.OPEN);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            
            Writer.WriteLine();
            Writer.Write(C.FETCH);
            Writer.Write(C.SPACE);
            Writer.Write(C.NEXT);
            Writer.Write(C.SPACE);
            Writer.Write(C.FROM);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            Writer.Write(C.SPACE);
            Writer.Write(C.INTO);
            Writer.Write(C.SPACE);
            Writer.Write(intoVariables.JoinWith());
            
            Writer.WriteLine();
            Writer.Write(C.WHILE);
            Writer.Write(C.SPACE);
            Writer.Write(C.FETCH_STATUS);
            Writer.Write(C.EQUALS);
            Writer.Write(0L.ToSQL());
            
            Writer.WriteLine();

            Writer.Write(C.BEGIN);
            Writer.WriteLine();
            Indent++;
            body(this);
            Writer.WriteLine();
            Writer.Write(C.FETCH);
            Writer.Write(C.SPACE);
            Writer.Write(C.NEXT);
            Writer.Write(C.SPACE);
            Writer.Write(C.FROM);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            Writer.Write(C.SPACE);
            Writer.Write(C.INTO);
            Writer.Write(C.SPACE);
            Writer.Write(intoVariables.JoinWith());
            
            Writer.WriteLine();

            Indent--;
            Writer.Write(C.END);
            Writer.WriteLine();

            Writer.Write(C.CLOSE);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            Writer.WriteLine();

            Writer.Write(C.DEALLOCATE);
            Writer.Write(C.SPACE);
            Writer.Write(variableName);
            Writer.WriteLine();

        }

        public void Print(ISqlExpression expression)
        {
            Writer.Write("print");
            Writer.Write(C.BEGIN_SCOPE);
            Writer.Write(expression);
            Writer.Write(C.END_SCOPE);
            Writer.WriteLine();
        }

        public AbstractSqlColumn Column(string columnName)
        {
            return new SqlServerColumn(columnName);
        }
        public AbstractSqlColumn Column(string columnName,string tableAlias)
        {
            return new SqlServerColumnWithTableAlias(columnName, tableAlias);
        }

        //public string I(string s)
        //{
        //    //TODO
        //    return s;
        //}
    }
}
#pragma warning restore IDE1006 // Naming Styles
