using System;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
#pragma warning disable IDE1006 // Naming Styles

namespace SQLEngine
{
    public interface IQueryBuilder: IDisposable
    {
        AbstractSqlExpression Null { get; }
        AbstractSqlExpression Now { get; }
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
        
        IIfQueryBuilder IfNotExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selection);
        IIfQueryBuilder IfNotExists(IAbstractSelectQueryBuilder selection);

        IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition);
        void Else();
        void Begin();
        void AddExpression(string expression);
        void End();
        
        AbstractSqlVariable DeclareRandom(string variableName, string type, AbstractSqlLiteral defaultValue);
        AbstractSqlVariable DeclareRandom(string variableName, string type);
        AbstractSqlVariable Declare(string variableName, string type);
        AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue);
        AbstractSqlVariable Declare<T>(string variableName,  AbstractSqlLiteral defaultValue);

        //default values usually become literals
        //AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue);
        //AbstractSqlVariable DeclareRandom(string variableName, string type, ISqlExpression defaultValue);

        AbstractSqlVariable Declare<T>(string variableName,  ISqlExpression defaultValue);

        void SetToScopeIdentity(AbstractSqlVariable variable);
        void Set(AbstractSqlVariable variable, Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right);
        void Set(AbstractSqlVariable variable, AbstractSqlExpression value);
        void Set(AbstractSqlVariable variable, AbstractSqlVariable value);
        void Set(AbstractSqlVariable variable, AbstractSqlLiteral value);
        
        void Return();
        
        void Return(ISqlExpression expression);
        void Return(AbstractSqlLiteral literal);
        void Comment(string comment);
        string GenerateUniqueVariableName(string beginning);
        
        void Cursor(
            string cursorName,
            Action<ISelectQueryBuilder> selection,
            AbstractSqlVariable[] intoVariables,
            Action<IQueryBuilder> body);
        void Print(ISqlExpression expression);
        void Print(AbstractSqlLiteral literal);
        string Build();
        AbstractSqlColumn Column(string columnName);
        AbstractSqlColumn Column(string columnName,string tableAlias);




        AbstractSqlLiteral Literal(string x, bool isUniCode = true);
        AbstractSqlLiteral Literal(DateTime x, bool includeTime = true);
        AbstractSqlLiteral Literal(int x);
        AbstractSqlLiteral Literal(Enum x);
        AbstractSqlLiteral Literal(int? x);
        AbstractSqlLiteral Literal(byte x);
        AbstractSqlLiteral Literal(byte? x);
        AbstractSqlLiteral Literal(long x);
        AbstractSqlLiteral Literal(long? x);
        AbstractSqlLiteral Literal(decimal? x);

        AbstractSqlLiteral Literal(short x);
        AbstractSqlLiteral Literal(short? x);

        AbstractSqlLiteral Literal(params byte[] x);

        [Obsolete("This is a fallback for " +
                  "If You don't find any Method to use for custom query," +
                  "So If You Are Here Please create issue on github " +
                  "page of SqlEngine Repository")]
        AbstractSqlExpression Raw(string rawSqlExpression);
        [Obsolete("This is a fallback for " +
                  "If You don't find any Method to use for custom query," +
                  "So If You Are Here Please create issue on github " +
                  "page of SqlEngine Repository")]
        AbstractSqlCondition RawCondition(string rawConditionQuery);

        ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder);
        
        void BeginTransaction(string transactionName=null);
        void CommitTransaction(string transactionName = null);
        void RollbackTransaction(string transactionName = null);
    }

}