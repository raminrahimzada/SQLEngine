using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace SQLEngine;

public interface IQueryBuilder : IDisposable
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IAlterQueryBuilder Alter { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    ICreateQueryBuilder Create { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IDeleteQueryBuilder Delete { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IDropQueryBuilder Drop { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IExecuteQueryBuilder Execute { get; }

    IConditionFilterQueryHelper Helper { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IInsertQueryBuilder Insert { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    ISelectQueryBuilder Select { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IUpdateQueryBuilder Update { get; }

    [Obsolete("This is a fallback for " +
              "If You don't find any Method to use for custom query," +
              "So If You Are Here Please create issue on github " +
              "page of SqlEngine Repository")]
    void AddExpression(string rawExpression);


    void Begin();


    void BeginTransaction(string transactionName = null);


    string Build();

    AbstractSqlExpression CallFunc(string functionName, string schema = null);
    AbstractSqlExpression CallFunc(string functionName, string schema = null, params AbstractSqlLiteral[] expressions);
    AbstractSqlExpression CallFunc(string functionName, string schema = null, params ISqlExpression[] expressions);

    AbstractSqlExpression CallFunc(string functionName, string schema = null,
        params AbstractSqlExpression[] expressions);

    [Pure]
    ISqlExpression Cast(ISqlExpression expression, string asType);

    void Clear();


    AbstractSqlColumn Column(string columnName);
    AbstractSqlColumn Column(string columnName, string tableAlias);
    void Comment(string comment);
    void CommitTransaction(string transactionName = null);

    void Cursor(
        string cursorName,
        Action<ISelectQueryBuilder> selection,
        AbstractSqlVariable[] intoVariables,
        Action<IQueryBuilder> body);

    AbstractSqlVariable Declare(string variableName, string type);
    AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue);
    AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue);
    AbstractSqlVariable Declare<T>(string variableName);
    AbstractSqlVariable Declare<T>(string variableName, AbstractSqlLiteral defaultValue);


    //default values usually become literals
    AbstractSqlVariable Declare(string variableName, string type, Action<IQueryBuilder> builder);
    AbstractSqlVariable Declare<T>(string variableName, Action<IQueryBuilder> builder);

    AbstractSqlVariable Declare<T>(string variableName, ISqlExpression defaultValue);

    AbstractSqlVariable DeclareNew(string type);
    AbstractSqlVariable DeclareNew(string type, AbstractSqlLiteral defaultValue);
    AbstractSqlVariable DeclareNew<T>(AbstractSqlLiteral defaultValue);

    /// <summary>
    ///     Declares a new variable with unique name and returns it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    AbstractSqlVariable DeclareNew<T>();

    void Else();
    IDisposable Else2();


    IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition);
    void End();

    void Goto(string labelName);


    IDisposable If(AbstractSqlCondition condition);

    IDisposable IfExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> selector);
    IDisposable IfNotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> selector);
    IDisposable Label(string labelName);

    [Pure]
    ISqlExpression Len(ISqlExpression expression);

    [Pure]
    AbstractSqlLiteral Literal(AbstractSqlLiteral literal);

    AbstractSqlLiteral Literal(string x, bool isUniCode);
    AbstractSqlLiteral Literal(DateTime x, bool includeTime);

    [Pure]
    AbstractSqlExpression Now();


    void Print(ISqlExpression expression);
    void Print(AbstractSqlLiteral literal);

    [Pure]
    AbstractSqlExpression Rand();

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

    AbstractSqlExpression RawInternal(string rawSqlExpression);


    void Return();
    void Return(ISqlExpression expression);
    void Return(AbstractSqlLiteral literal);
    void RollbackTransaction(string transactionName = null);

    void Set(AbstractSqlVariable variable,
        Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right);

    void Set(AbstractSqlVariable variable, AbstractSqlExpression value);
    void Set(AbstractSqlVariable variable, ISqlExpression value);
    void Set(AbstractSqlVariable variable, AbstractSqlVariable value);
    void Set(AbstractSqlVariable variable, AbstractSqlLiteral value);


    void SetToScopeIdentity(AbstractSqlVariable variable);


    [Pure]
    AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start, ISqlExpression length);

    AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, AbstractSqlLiteral length);
    AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, ISqlExpression length);
    AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start, AbstractSqlLiteral length);


    AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, ISqlExpression length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, AbstractSqlLiteral length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, ISqlExpression length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, AbstractSqlLiteral length);

    void Truncate(string tableName);
    void Truncate<TTable>() where TTable : ITable, new();


    ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder);
    IDisposable While(AbstractSqlCondition condition);
}