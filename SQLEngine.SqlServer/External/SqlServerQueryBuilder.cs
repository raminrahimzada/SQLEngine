using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable IDE1006 // Naming Styles

namespace SQLEngine.SqlServer
{
    public class SqlServerQueryBuilder : IQueryBuilder
    {
        private static void SetupDefaults()
        {
            //default settings
            Query.Settings.EnumSqlStringConvertor = new IntegerEnumSqlStringConvertor();
            Query.Settings.TypeConvertor = new DefaultTypeConvertor();
            Query.Settings.UniqueVariableNameGenerator = new DefaultUniqueVariableNameGenerator();
            Query.Settings.DefaultIdColumnName = "Id";
            Query.Settings.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            Query.Settings.DateFormat = "yyyy-MM-dd";
            Query.Settings.DefaultPrecision = 18;
            Query.Settings.DefaultScale = 4;
            Query.Settings.SQLErrorState = 47;
        }
        public static void Setup() 
        {
            SetupDefaults();

            SqlServerLiteral.Setup();
            SqlServerRawExpression.Setup();
            SqlServerCondition.Setup();
        }

        static SqlServerQueryBuilder()
        {
            Setup();
        }

        private readonly List<IAbstractQueryBuilder> _list = new();

        public override string ToString()
        {
            return Build();
        }

        public void Clear()
        {
            _list.Clear();
        }
        public  string Build()
        {
            using (var writer = SqlWriter.New)
            {
                foreach (var builder in _list)
                {
                    builder.Build(writer);
                }

                return writer.Build();
            }
        }
        public  void Build(ISqlWriter writer)
        {
            foreach (var builder in _list)
            {
                builder.Build(writer);
            }
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
        
        public AbstractSqlExpression Raw(string rawSqlExpression)
        {
            return new SqlServerRawExpression(rawSqlExpression);
        }
        
        public AbstractSqlExpression RawInternal(string rawSqlExpression)
        {
            return new SqlServerRawExpression(rawSqlExpression);
        }

        public AbstractSqlCondition RawCondition(string rawConditionQuery)
        {
            return new SqlServerCondition(rawConditionQuery);
        }

        public AbstractSqlLiteral Literal(AbstractSqlLiteral literal)
        {
            return literal;
        }

        public ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder)
        {
            var t = new TryCatchQueryBuilder();
            var expression = t.Try(builder);
            _list.Add(expression);
            return expression;
        }

        public void BeginTransaction(string transactionName = null)
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.Write(C.BEGIN);
                w.Write(C.SPACE);
                w.Write(C.TRANSACTION);
                if (!string.IsNullOrWhiteSpace(transactionName))
                {
                    w.Write(C.SPACE);
                    w.Write(transactionName);
                }
                w.WriteLine();
            }));
        }

        public void CommitTransaction(string transactionName = null)
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.Write(C.COMMIT);
                w.Write(C.SPACE);
                w.Write(C.TRANSACTION);
                if (!string.IsNullOrWhiteSpace(transactionName))
                {
                    w.Write(C.SPACE);
                    w.Write(transactionName);
                }
                w.WriteLine();
            }));
        }

        public void RollbackTransaction(string transactionName = null)
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.Write(C.ROLLBACK);
                w.Write(C.SPACE);
                w.Write(C.TRANSACTION);
                if (!string.IsNullOrWhiteSpace(transactionName))
                {
                    w.Write(C.SPACE);
                    w.Write(transactionName);
                }
            }));
        }

        public void Truncate(string tableName)
        {
            var t = new TruncateQueryBuilder();
            var expression = t.Table(tableName);
            _list.Add(expression);
        }
        public void Truncate<TTable>() where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                Truncate(table.Name);
            }
        }

        public IIfQueryBuilder IfOr(params AbstractSqlCondition[] conditions)
        {
            var str = conditions.Select(x => x.ToSqlString()).ToArray();
            var xx = string.Join(C.OR, str);
            var condition = SqlServerCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions)
        {
            var str = conditions.Select(x => x.ToSqlString()).ToArray();
            var xx = string.Join(C.AND, str);
            var condition = SqlServerCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder If(AbstractSqlCondition condition)
        {
            return _Add(new IfQueryBuilder(condition));            
        }

        public IDisposable If2(AbstractSqlCondition condition)
        {
            return new IfDisposable(this,condition);
        }
        public IDisposable Else2()
        {
            return new ElseDisposable(this);
        }

        class ElseDisposable : IDisposable
        {
            private readonly SqlServerQueryBuilder _sqlServerQueryBuilder;

            public ElseDisposable(SqlServerQueryBuilder sqlServerQueryBuilder)
            {
                _sqlServerQueryBuilder = sqlServerQueryBuilder;
                _sqlServerQueryBuilder.Else();
                _sqlServerQueryBuilder.AddExpression(Environment.NewLine);
                _sqlServerQueryBuilder.Begin();
            }

            public void Dispose()
            {
                _sqlServerQueryBuilder.End();
            }
        }
        class IfDisposable : IDisposable
        {
            private readonly SqlServerQueryBuilder _sqlServerQueryBuilder;

            public IfDisposable(SqlServerQueryBuilder sqlServerQueryBuilder, AbstractSqlCondition abstractSqlCondition)
            {
                _sqlServerQueryBuilder = sqlServerQueryBuilder;
                _sqlServerQueryBuilder.If(abstractSqlCondition);
                _sqlServerQueryBuilder.Begin();
            }

            public void Dispose()
            {
                _sqlServerQueryBuilder.End();
            }
        }
        public IIfQueryBuilder IfNot(AbstractSqlCondition condition)
        {
            return _Add(new IfNotQueryBuilder(condition));            
        }
        public IIfQueryBuilder IfExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selector)
        {
            using (var s=new SelectQueryBuilder())
            {
                selector(s);
                return If(new SqlServerCondition(C.EXISTS, C.BEGIN_SCOPE+string.Empty, s.Build(), C.END_SCOPE + string.Empty));
            }
        }
        public IIfQueryBuilder IfExists(IAbstractSelectQueryBuilder selection)
        {
            return If(new SqlServerCondition(C.SPACE+string.Empty, C.EXISTS, C.BEGIN_SCOPE + string.Empty, selection.Build(), C.END_SCOPE + string.Empty));
        }

        public IIfQueryBuilder IfNotExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selector)
        {
            using (var s = new SelectQueryBuilder())
            {
                selector(s);
                return If(new SqlServerCondition(C.NOT , C.SPACE +string.Empty, C.EXISTS , C.BEGIN_SCOPE + string.Empty, s.Build() , C.END_SCOPE + string.Empty));
            }
        }

        public IIfQueryBuilder IfNotExists(IAbstractSelectQueryBuilder selection)
        {
            return If(new SqlServerCondition(C.NOT , C.SPACE + string.Empty, C.EXISTS , C.BEGIN_SCOPE + string.Empty, selection.Build() , C.END_SCOPE + string.Empty));
        }

        public IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition)
        {
            return _Add(new ElseIfQueryBuilder(condition));
            
        }

        public void Else()
        {
            _list.Add(new RawStringQueryBuilder(w => w.Write(C.ELSE, C.SPACE + string.Empty)));
        }

        public void Begin()
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.WriteLine(C.BEGIN);
                w.Indent++;
            }));
        }

        public void AddExpression(string rawExpression)
        {
            _list.Add(new RawStringQueryBuilder(w =>
            {
                w.WriteLine(rawExpression);
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


        public AbstractSqlVariable DeclareNew<T>(AbstractSqlLiteral defaultValue)
        {
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            return DeclareNew(type, defaultValue);
        }

        public AbstractSqlVariable DeclareNew<T>()
        {
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            return DeclareNew(type);
        }

        public AbstractSqlVariable Declare(string variableName, string type, Action<IQueryBuilder> builder)
        {
            using (var q = Query.New)
            {
                builder(q);
                using (var t = new DeclarationQueryBuilder())
                {
                    var defaultValue = q.Build();
                    var expression = t.Declare(variableName).OfType(type).Default(q.RawInternal(defaultValue));
                    _list.Add(expression);
                    return new SqlServerVariable(variableName);
                }
            }
        }

        public AbstractSqlVariable Declare<T>(string variableName, Action<IQueryBuilder> builder)
        {
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            return Declare(variableName, type, builder);
        }

      
        public AbstractSqlVariable DeclareNew(string type)
        {
            var variableName = Query.Settings.UniqueVariableNameGenerator.New();
            return Declare(variableName, type);
        }
        public AbstractSqlVariable DeclareNew(string type,AbstractSqlLiteral defaultValue)
        {
            var variableName = Query.Settings.UniqueVariableNameGenerator.New();
            return Declare(variableName, type, defaultValue);
        }

        public AbstractSqlVariable Declare(string variableName, string type)
        {
            var t = new DeclarationQueryBuilder();
            {
                var expression = t.Declare(variableName).OfType(type);
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }


        public AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            {
                var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }
        public AbstractSqlVariable Declare<T>(string variableName, AbstractSqlLiteral defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            {
                var type = Query.Settings.TypeConvertor.ToSqlType<T>();
                var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }
        public AbstractSqlVariable Declare<T>(string variableName)
        {
            var t = new DeclarationQueryBuilder();
            {
                var type = Query.Settings.TypeConvertor.ToSqlType<T>();
                var expression = t.Declare(variableName).OfType(type);
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }

        public AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            {
                var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
                _list.Add(expression);
                return new SqlServerVariable(variableName);
            }
        }
        public AbstractSqlVariable Declare<T>(string variableName, ISqlExpression defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            {
                var type = Query.Settings.TypeConvertor.ToSqlType<T>();
                var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
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
                var expression = t.Set(variable).To(RawInternal(writer.Build()));
                _list.Add(expression);
            }
        }

        public void Set(AbstractSqlVariable variable, AbstractSqlExpression value)
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
                writer.Write(C.SPACE);
                writer.Write(expression.ToSqlString());
                writer.WriteLine(C.SPACE);
            }));
        }
        public void Return(AbstractSqlLiteral literal)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write(C.RETURN);
                writer.Write(C.SPACE);
                writer.Write(literal.ToSqlString());
                writer.WriteLine(C.SPACE);
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
            }));
            
        }

        public void Cursor(
            string cursorName,
            Action<ISelectQueryBuilder> selection,
            AbstractSqlVariable[] intoVariables,
            Action<IQueryBuilder> body)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                var variableName = cursorName;
                writer.Write(C.DECLARE);
                writer.Write(C.SPACE);
                writer.Write(cursorName);
                writer.Write(C.SPACE);
                writer.Write(C.CURSOR);
                writer.Write(C.SPACE);
                writer.Write(C.FOR);
                writer.Write(C.SPACE);
                using (var s=new SelectQueryBuilder())
                {
                    selection(s);
                    s.Build(writer);
                }
                writer.WriteLine();

                writer.Write(C.OPEN);
                writer.Write(C.SPACE);
                writer.Write(variableName);

                writer.WriteLine();
                writer.Write(C.FETCH);
                writer.Write(C.SPACE);
                writer.Write(C.NEXT);
                writer.Write(C.SPACE);
                writer.Write(C.FROM);
                writer.Write(C.SPACE);
                writer.Write(variableName);
                writer.Write(C.SPACE);
                writer.Write(C.INTO);
                writer.Write(C.SPACE);
                writer.Write(intoVariables.JoinWith());

                writer.WriteLine();
                writer.Write(C.WHILE);
                writer.Write(C.SPACE);
                writer.Write(C.FETCH_STATUS);
                writer.Write(C.EQUALS);
                writer.Write(0L.ToString());

                writer.WriteLine();

                writer.Write(C.BEGIN);
                writer.WriteLine();
                writer.Indent++;

                using (var s = new FunctionBodyQueryBuilder())
                {
                    body(s);
                    writer.Write(s.Build());
                }

                writer.WriteLine();
                writer.Write(C.FETCH);
                writer.Write(C.SPACE);
                writer.Write(C.NEXT);
                writer.Write(C.SPACE);
                writer.Write(C.FROM);
                writer.Write(C.SPACE);
                writer.Write(variableName);
                writer.Write(C.SPACE);
                writer.Write(C.INTO);
                writer.Write(C.SPACE);
                writer.Write(intoVariables.JoinWith());

                writer.WriteLine();

                writer.Indent--;
                writer.Write(C.END);
                writer.WriteLine();

                writer.Write(C.CLOSE);
                writer.Write(C.SPACE);
                writer.Write(variableName);
                writer.WriteLine();

                writer.Write(C.DEALLOCATE);
                writer.Write(C.SPACE);
                writer.Write(variableName);
                writer.WriteLine();
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
        public void Print(AbstractSqlLiteral literal)
        {
            _list.Add(new RawStringQueryBuilder(writer =>
            {
                writer.Write("print");
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(literal.ToSqlString());
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

        public AbstractSqlLiteral Literal(string x, bool isUniCode )
        {
            return AbstractSqlLiteral.From(x,isUniCode);
        }
        public AbstractSqlLiteral Literal(string x)
        {
            return AbstractSqlLiteral.From(x,true);
        }

        public AbstractSqlLiteral Literal(DateTime x, bool includeTime)
        {
            return SqlServerLiteral.From(x, includeTime);
        }
        public AbstractSqlLiteral Literal(DateTime x)
        {
            return SqlServerLiteral.From(x, true);
        }
        public AbstractSqlLiteral Literal(DateTime? x, bool includeTime = true)
        {
            return SqlServerLiteral.From(x, includeTime);
        }
        public AbstractSqlLiteral Literal(DateTimeOffset x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(DateTimeOffset? x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(int x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(int? x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(Enum x)
        {
            return AbstractSqlLiteral.From(x);
        }

       

        public AbstractSqlLiteral Literal(byte x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(sbyte x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(byte? x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(long x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(long? x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(decimal? x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(decimal x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(short x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(ushort x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(short? x)
        {
            return AbstractSqlLiteral.From(x);
        }
        public AbstractSqlLiteral Literal(double? x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(byte[] x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public virtual void Dispose(bool b)
        {
            foreach (var builder in _list)
            {
                builder.Dispose();
            }
            _list.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
