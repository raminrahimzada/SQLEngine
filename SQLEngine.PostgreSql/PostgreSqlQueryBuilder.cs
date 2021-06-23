using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles

namespace SQLEngine.PostgreSql
{
    [Obsolete("Do not Use,Not Completed Development")]
    public class PostgreSqlQueryBuilder : IQueryBuilder
    {
        private static void SetupDefaults()
        {
            //default settings
            Query.Settings.EnumSqlStringConvertor = new IntegerEnumSqlStringConvertor();
            Query.Settings.TypeConvertor = new TypeConvertor();
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

            PostgreSqlLiteral.Setup();
            PostgreSqlRawExpression.Setup();
            PostgreSqlCondition.Setup();
        }

        static PostgreSqlQueryBuilder()
        {
            Setup();
        }

        private readonly List<IAbstractQueryBuilder> _list = new List<IAbstractQueryBuilder>();

        public override string ToString()
        {
            return Build();
        }

        public  string Build()
        {
            using (var Writer = SqlWriter.New)
            {
                foreach (var builder in _list)
                {
                    builder.Build(Writer);
                    Writer.WriteLine();
                }

                return Writer.Build();
            }
        }
        public  void Build(ISqlWriter Writer)
        {
            foreach (var builder in _list)
            {
                builder.Build(Writer);
                Writer.WriteLine();
            }
        }

        
        private T _Add<T>(T item) where T : IAbstractQueryBuilder
        {
            _list.Add(item);
            return item;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public ISelectQueryBuilder Select => _Add(new SelectQueryBuilder());
        public IUpdateQueryBuilder Update => _Add(new UpdateQueryBuilder());
        public IDeleteQueryBuilder Delete => _Add(new DeleteQueryBuilder());
        public IInsertQueryBuilder Insert => _Add(new InsertQueryBuilder());
        public IAlterQueryBuilder Alter => _Add(new AlterQueryBuilder());
        
        public ICreateQueryBuilder Create => _Add(new CreateQueryBuilder());
        
        public IDropQueryBuilder Drop => _Add(new DropQueryBuilder());
        public IExecuteQueryBuilder Execute => _Add(new ExecuteQueryBuilder());   

        public IConditionFilterQueryHelper Helper { get; } = new PostgreSqlConditionFilterQueryHelper();

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
            return new PostgreSqlRawExpression(rawSqlExpression);
        }

        public AbstractSqlExpression RawInternal(string rawSqlExpression)
        {
            return new PostgreSqlRawExpression(rawSqlExpression);
        }

        public AbstractSqlCondition RawCondition(string rawConditionQuery)
        {
            return new PostgreSqlCondition(rawConditionQuery);
        }

        public AbstractSqlLiteral Literal(AbstractSqlLiteral literal)
        {
            return literal;
        }

        public AbstractSqlLiteral Literal(string x)
        {
            throw new NotImplementedException();
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
            var condition = PostgreSqlCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions)
        {
            var str = conditions.Select(x => x.ToSqlString()).ToArray();
            var xx = string.Join(C.AND, str);
            var condition = PostgreSqlCondition.Raw(xx);
            return If(condition);
        }
        public IIfQueryBuilder If(AbstractSqlCondition condition)
        {
            return _Add(new IfQueryBuilder(condition));            
        }

        public IDisposable If2(AbstractSqlCondition condition)
        {
            throw new NotImplementedException();
        }

        public IDisposable Else2()
        {
            throw new NotImplementedException();
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
                return If(new PostgreSqlCondition(C.EXISTS, C.BEGIN_SCOPE, s.Build(), C.END_SCOPE));
            }
        }
        public IIfQueryBuilder IfExists(IAbstractSelectQueryBuilder selection)
        {
            return If(new PostgreSqlCondition(C.NOT,C.SPACE,C.EXISTS , C.BEGIN_SCOPE , selection.Build() , C.END_SCOPE));
        }

        public IIfQueryBuilder IfNotExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selector)
        {
            using (var s = new SelectQueryBuilder())
            {
                selector(s);
                return If(new PostgreSqlCondition(C.NOT , C.SPACE , C.EXISTS , C.BEGIN_SCOPE , s.Build() , C.END_SCOPE));
            }
        }

        public IIfQueryBuilder IfNotExists(IAbstractSelectQueryBuilder selection)
        {
            return If(new PostgreSqlCondition(C.NOT , C.SPACE , C.EXISTS , C.BEGIN_SCOPE , selection.Build() , C.END_SCOPE));
        }

        public IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition)
        {
            return _Add(new ElseIfQueryBuilder(condition));
        }

        public void Else()
        {
            _list.Add(new RawStringQueryBuilder(w => w.Write(C.ELSE, C.SPACE)));
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

        public AbstractSqlVariable DeclareNew(string type)
        {
            throw new NotImplementedException();
        }

        public AbstractSqlVariable DeclareNew(string type, AbstractSqlLiteral defaultValue)
        {
            throw new NotImplementedException();
        }

        public AbstractSqlVariable DeclareNew<T>(AbstractSqlLiteral defaultValue)
        {
            throw new NotImplementedException();
        }

        public AbstractSqlVariable DeclareNew<T>()
        {
            throw new NotImplementedException();
        }


        

        public AbstractSqlVariable Declare(string variableName, string type, Action<IQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }

        public AbstractSqlVariable Declare<T>(string variableName, Action<IQueryBuilder> builder)
        {
            throw new NotImplementedException();
        }

        

       

        

        public AbstractSqlVariable Declare(string variableName, string type)
        {
            var t = new DeclarationQueryBuilder();
            {
                var expression = t.Declare(variableName).OfType(type);
                _list.Add(expression);
                return new PostgreSqlVariable(variableName);
            }
        }


        public AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
            _list.Add(expression);
            return new PostgreSqlVariable(variableName);
        }
        public AbstractSqlVariable Declare<T>(string variableName, AbstractSqlLiteral defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
            _list.Add(expression);
            return new PostgreSqlVariable(variableName);
        }
        public AbstractSqlVariable Declare<T>(string variableName)
        {
            var t = new DeclarationQueryBuilder();
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            var expression = t.Declare(variableName).OfType(type);
            _list.Add(expression);
            return new PostgreSqlVariable(variableName);
        }

        public AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
            _list.Add(expression);
            return new PostgreSqlVariable(variableName);
        }
        public AbstractSqlVariable Declare<T>(string variableName, ISqlExpression defaultValue)
        {
            var t = new DeclarationQueryBuilder();
            var type = Query.Settings.TypeConvertor.ToSqlType<T>();
            var expression = t.Declare(variableName).OfType(type).Default(defaultValue);
            _list.Add(expression);
            return new PostgreSqlVariable(variableName);
        }

        public void SetToScopeIdentity(AbstractSqlVariable variable)
        {
            Set(variable, x => x.ScopeIdentity());
        }

        public void Set(AbstractSqlVariable variable, Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right)
        {
            var t = new SetQueryBuilder();
            var functionCallExpressionBuilder=new CustomFunctionCallExpressionBuilder();
            ICustomFunctionCallNopBuilder tt = right(functionCallExpressionBuilder);
            using (var writer=SqlWriter.New)
            {
                tt.Build(writer);
                var expression = t.Set(variable).To(Raw(writer.Build()));
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
            var expression = t.Set(variable).To(value);
            _list.Add(expression);
        }
        public void Set(AbstractSqlVariable variable, AbstractSqlLiteral value)
        {
            var t = new SetQueryBuilder();
            var expression = t.Set(variable).To(value);
            _list.Add(expression);
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
                //writer.WriteLine("PRINT(" + comment.ToSQL() + ");");
            }));
            
        }

        
        

        public void Cursor(
            string cursorName,
            Action<ISelectQueryBuilder> selection,
            AbstractSqlVariable[] intoVariables,
            Action<IQueryBuilder> body)
        {
            _list.Add(new RawStringQueryBuilder(Writer =>
            {
                var variableName = cursorName;//"__cursor_" + Guid.NewGuid().ToString().Replace("-", "");
                Writer.Write(C.DECLARE);
                Writer.Write(C.SPACE);
                Writer.Write(cursorName);
                Writer.Write(C.SPACE);
                Writer.Write(C.CURSOR);
                Writer.Write(C.SPACE);
                Writer.Write(C.FOR);
                Writer.Write(C.SPACE);
                using (var s=new SelectQueryBuilder())
                {
                    selection(s);
                    s.Build(Writer);
                }
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

                using (var s = new FunctionBodyQueryBuilder())
                {
                    body(s);
                    Writer.Write(s.Build());
                }

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
                writer.Write(C.RAISE);
                writer.Write(C.SPACE);
                writer.Write(C.INFO);
                writer.Write(C.SPACE);
                writer.Write("'%'");
                writer.Write(C.COMMA);
                writer.Write(expression.ToSqlString());
                writer.WriteLine(C.SEMICOLON);
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
            return new PostgreSqlColumn(columnName);
        }
        public AbstractSqlColumn Column(string columnName,string tableAlias)
        {
            return new PostgreSqlColumnWithTableAlias(columnName, tableAlias);
        }

        public AbstractSqlLiteral Literal(string x, bool isUniCode = true)
        {
            return AbstractSqlLiteral.From(x,isUniCode);
        }

        public AbstractSqlLiteral Literal(DateTime x)
        {
            throw new NotImplementedException();
        }

        public AbstractSqlLiteral Literal(DateTime x, bool includeTime = true)
        {
            return PostgreSqlLiteral.From(x, includeTime);
        }

        public AbstractSqlLiteral Literal(DateTime? x, bool includeTime = true)
        {
            return PostgreSqlLiteral.From(x, includeTime);
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

        public AbstractSqlLiteral Literal(Enum x)
        {
            return AbstractSqlLiteral.From(x);
        }

        public AbstractSqlLiteral Literal(int? x)
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

        

        public void Dispose()
        {
            _list.Clear();
            foreach (var builder in _list)
            {
                builder.Dispose();
            }
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
