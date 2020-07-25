using System;

namespace SQLEngine
{
    
    public interface IQueryBuilder: IDisposable
    {
        ISqlExpression Null { get; }
        ISelectQueryBuilder _select { get; }
        IUpdateQueryBuilder _update { get; }
        IDeleteQueryBuilder _delete { get; }
        IInsertQueryBuilder _insert { get; }
        IAlterQueryBuilder _alter { get; }
        ICreateQueryBuilder _create { get; }
        IDropQueryBuilder _drop { get; }
        IExecuteQueryBuilder _execute { get; }

        ISelectQueryBuilder Select { get; }
        IUpdateQueryBuilder Update { get; }
        IDeleteQueryBuilder Delete { get; }
        IInsertQueryBuilder Insert { get; }
        ICreateQueryBuilder Create { get; }
        IAlterQueryBuilder Alter { get; }
        IDropQueryBuilder Drop { get; }
        IExecuteQueryBuilder Execute { get; }
        IConditionFilterQueryHelper Helper { get; }
        
        void Union();
        void UnionAll();
        void Truncate(string tableName);
        IIfQueryBuilder IfOr(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder If(AbstractSqlCondition condition);

        IIfQueryBuilder IfExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selection);
        IIfQueryBuilder IfExists(IAbstractSelectQueryBuilder selection);

        IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition);
        void Else();
        void Begin();
        void AddExpression(string expression);
        void End();
        //void Declare(Func<IDeclarationQueryBuilder, IDeclarationQueryBuilder> builder);
        AbstractSqlVariable DeclareRandom(string variableName, string type, AbstractSqlLiteral defaultValue = null);
        AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue = null);
        void SetToScopeIdentity(AbstractSqlVariable variable);
        void Set(AbstractSqlVariable variable, Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right);
        void Set(AbstractSqlVariable variable, ISqlExpression value);
        void Set(AbstractSqlVariable variable, AbstractSqlVariable value);
        void Set(AbstractSqlVariable variable, AbstractSqlLiteral value);
        //void Set(ISqlVariable variable, Func<ICastQueryBuilder, ICastQueryBuilder> q);
        //void Execute(Func<IExecuteQueryBuilder, IExecuteProcedureNeedArgQueryBuilder> builder);
        //void Insert(Func<IInsertQueryBuilder, IAbstractInsertQueryBuilder> builder);
        //void Update(Func<IUpdateQueryBuilder, IAbstractUpdateQueryBuilder> builder);
        //void Delete(Func<IDeleteQueryBuilder, IDeleteQueryBuilder> builder);
        void Return();
        //void Return(string sql);
        void Return(ISqlExpression expression);
        void Return(AbstractSqlLiteral literal);
        void Comment(string comment);
        string GenerateRandomVariableName(string beginning);
        //void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameNoSchemaNoDBQueryBuilder> builder);
        //void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameQueryBuilder> builder);
        //void Drop(Func<IDropViewQueryBuilder, IDropViewQueryBuilder> builder);
        void Cursor(
            string cursorName,
            Action<ISelectQueryBuilder> selection,
            AbstractSqlVariable[] intoVariables,
            Action<IQueryBuilder> body);
        void Print(ISqlExpression expression);
        void Print(AbstractSqlLiteral literal);
        //void Join(AbstractQueryBuilder other);
        string Build();
        AbstractSqlColumn Column(string columnName);
        AbstractSqlColumn Column(string columnName,string tableAlias);




        AbstractSqlLiteral Literal(string x, bool isUniCode = true);
        AbstractSqlLiteral Literal(DateTime x, bool includeTime = true);
        AbstractSqlLiteral Literal(int x);
        AbstractSqlLiteral Literal(int? x);
        AbstractSqlLiteral Literal(byte x);
        AbstractSqlLiteral Literal(byte? x);

        AbstractSqlLiteral Literal(long x);
        AbstractSqlLiteral Literal(long? x);

        AbstractSqlLiteral Literal(short x);
        AbstractSqlLiteral Literal(short? x);

        AbstractSqlLiteral Literal(byte[] x);

        ISqlExpression Raw(string expression);
    }

}