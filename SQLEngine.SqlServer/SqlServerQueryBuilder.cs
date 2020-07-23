using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles

namespace SQLEngine.SqlServer
{
    public class SqlServerQueryBuilder : IQueryBuilder
    {
        private readonly List<IAbstractQueryBuilder> _list = new List<IAbstractQueryBuilder>();

        public override string ToString()
        {
            return Build();
        }

        public  string Build()
        {
            var Writer = SqlWriter.New;
            foreach (var builder in _list)
            {
                builder.Build(Writer);
            }
            return Writer.Build();
        }

        
        private T _Add<T>(T item) where T : IAbstractQueryBuilder
        {
            _list.Add(item);
            return item;
        }

        public ISelectQueryBuilder Select => _Add(new SelectQueryBuilder());
        public IUpdateQueryBuilder Update => _Add(new UpdateQueryBuilder());
        public IDeleteQueryBuilder Delete => _Add(new DeleteQueryBuilder());
        public IInsertQueryBuilder Insert => _Add(new InsertQueryBuilder());
        public IAlterQueryBuilder Alter => _Add(new AlterQueryBuilder());
        
        public ICreateQueryBuilder Create => _Add(new CreateQueryBuilder());
        
        public IDropQueryBuilder Drop => _Add(new DropQueryBuilder());
        public IExecuteQueryBuilder Execute => _Add(new ExecuteQueryBuilder());


        public IConditionFilterQueryHelper Helper { get; } = new SqlServerConditionFilterQueryHelper();

        public void Union()
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.WriteLineEx(C.UNION);
            }));
        }

        public void UnionAll()
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.WriteLine(C.UNION);
                writer.WriteLine(C.SPACE);
                writer.WriteLine(C.ALL);
            }));
        }
        [Obsolete("This is a fallback for " +
                  "If You don't find any Method to use for custom query," +
                  "So If You Are Here Please create issue on github " +
                  "page of SqlEngine Repository")]
        public ISqlExpression Raw(string expression)
        {
            return new SqlServerRawExpression(expression);
        }

        public void Truncate(string tableName)
        {
            var t = new TruncateQueryBuilder();
            var expression = t.Table(tableName);
            _list.Add(expression);
        }

        public IIfQueryBuilder IfOr(params AbstractSqlCondition[] conditions)
        {
            var str = conditions.Select(x => x.ToSqlString()).ToArray();
            var xx = Helper.Or(str);
            var condition = SqlServerCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions)
        {
            var str = conditions.Select(x => x.ToSqlString()).ToArray();
            var xx = Helper.And(str);
            var condition = SqlServerCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder If(AbstractSqlCondition condition)
        {
            return _Add(new IfQueryBuilder(condition));            
        }

        public IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition)
        {
            return _Add(new ElseIfQueryBuilder(condition));
            
        }

        public void Else()
        {
            _list.Add(new RawStringQueryBuilder(w => w.Write(C.ELSE)));
        }

        public void Begin()
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.WriteLine(C.BEGIN);
                w.Indent++;
            }));
        }

        public void AddExpression(string expression)
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.WriteLine(expression);
            }));
        }

        public void End()
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.WriteLine(C.END);
                w.Indent--;
            }));
        }

        //public void Declare(Func<IDeclarationQueryBuilder, IDeclarationQueryBuilder> builder)
        //{
        //    Writer.WriteLineEx(builder.Invoke(GetDefault<DeclarationQueryBuilder>()).Build());
        //}

        public AbstractSqlVariable DeclareRandom(string variableName, string type, AbstractSqlLiteral defaultValue = null)
        {
            variableName = GenerateRandomVariableName(variableName.ToLowerInvariant());

            return Declare(variableName, type, defaultValue);
        }



        public AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue = null)
        {
            var t = new DeclarationQueryBuilder();
            {
                var expression = t.Declare(variableName).OfType(type).Default(defaultValue?.ToSqlString());
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }

        public void SetToScopeIdentity(AbstractSqlVariable variable)
        {
            Set(variable, x => x.ScopeIdentity());
        }

        public void Set(AbstractSqlVariable variable, Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right)
        {
            var t = new SetQueryBuilder();
            var functionCallExpressionBuilder=new CustomFunctionCallExpressionBuilder();
            right(functionCallExpressionBuilder);
            using (var writer=SqlWriter.New)
            {
                functionCallExpressionBuilder.Build(writer);
#pragma warning disable CS0618 // Type or member is obsolete
                var expression = t.Set(variable).To(Raw(writer.Build()));
#pragma warning restore CS0618 // Type or member is obsolete
                _list.Add(expression);
            }
        }

        public void Set(AbstractSqlVariable variable, ISqlExpression value)
        {
            var t = new SetQueryBuilder();
            {
                var expression = t.Set(variable).To(value);
                _list.Add(expression);
            }
        }
        public void Set(AbstractSqlVariable variable, AbstractSqlVariable value)
        {
            var t = new SetQueryBuilder();
            {
                var expression = t.Set(variable).To(value);
                _list.Add(expression);
            }
        }
        public void Set(AbstractSqlVariable variable, AbstractSqlLiteral value)
        {
            var t = new SetQueryBuilder();
            {
                var expression = t.Set(variable).To(value);
                _list.Add(expression);
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
        //public void Execute(Func<IExecuteQueryBuilder, IExecuteProcedureNeedArgQueryBuilder> builder)
        //{
        //    Writer.WriteLineEx(builder.Invoke(GetDefault<ExecuteQueryBuilder>()).Build());
        //}

        //public void Insert(Func<IInsertQueryBuilder, IAbstractInsertQueryBuilder> builder)
        //{
        //    Writer.WriteLineEx(builder.Invoke(GetDefault<InsertQueryBuilder>()).Build());
        //}

        //public void Update(Func<IUpdateQueryBuilder, IAbstractUpdateQueryBuilder> builder)
        //{
        //    Writer.WriteLineEx(builder.Invoke(GetDefault<UpdateQueryBuilder>()).Build());
        //}

        //public void Delete(Func<IDeleteQueryBuilder, IDeleteQueryBuilder> builder)
        //{
        //    Writer.WriteLineEx(builder.Invoke(GetDefault<DeleteQueryBuilder>()).Build());
        //}

        public void Return()
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write(C.RETURN);
                writer.WriteLine(C.SEMICOLON);
            }));
        }
        public void Return(string sql)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write(C.RETURN);
                writer.WriteWithScoped(sql);
                writer.WriteLine();
            }));
            
        }
        public void Return(ISqlExpression expression)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write(C.RETURN);
                writer.Write(expression.ToSqlString());
                writer.WriteLine();
            }));
        }

        public void Comment(string comment)
        {
            if (!string.IsNullOrEmpty(comment))
            {
                comment = comment.Replace("*/", "*\\/");
            }
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write("/*");
                writer.WriteEx(comment);
                writer.Write("*/ ");
                writer.WriteLine("");
                //writer.WriteLine("PRINT(" + comment.ToSQL() + ");");
            }));
            
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
        public void Cursor(string selection,string[] intoVariables,Action<IQueryBuilder> body)
        {
            _list.Add(new RawStringQueryBuilder(Writer =>
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
                Writer.Write(0L.ToString());

                Writer.WriteLine();

                Writer.Write(C.BEGIN);
                Writer.WriteLine();
                Writer.Indent++;
                //TODO
                //body(this);
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

                Writer.Indent--;
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
            }));
        }

        public void Print(ISqlExpression expression)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write("print");
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(expression.ToSqlString());
                writer.Write(C.END_SCOPE);
                writer.WriteLine();
            }));
            
        }

        public AbstractSqlColumn Column(string columnName)
        {
            return new SqlServerColumn(columnName);
        }
        public AbstractSqlColumn Column(string columnName,string tableAlias)
        {
            return new SqlServerColumnWithTableAlias(columnName, tableAlias);
        }

        public AbstractSqlLiteral Literal(string x, bool isUniCode = true)
        {
            return SqlServerLiteral.From(x,isUniCode);
        }

        public AbstractSqlLiteral Literal(DateTime x, bool includeTime = true)
        {
            return SqlServerLiteral.From(x, includeTime);
        }

        public AbstractSqlLiteral Literal(int x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(int? x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(byte x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(byte? x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(long x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(long? x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(short x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(short? x)
        {
            return SqlServerLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(byte[] x)
        {
            return SqlServerLiteral.From(x);
        }

        

        public void Dispose()
        {
            foreach (var builder in _list)
            {
                builder.Dispose();
            }
            _list.Clear();
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
