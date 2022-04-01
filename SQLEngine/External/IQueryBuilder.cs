using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace SQLEngine;

public interface IQueryBuilder : IDisposable
{
    IConditionFilterQueryHelper Helper { get; }
    void Clear();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    ISelectQueryBuilder Select { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IUpdateQueryBuilder Update { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IDeleteQueryBuilder Delete { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IInsertQueryBuilder Insert { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    ICreateQueryBuilder Create { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IAlterQueryBuilder Alter { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IDropQueryBuilder Drop { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IExecuteQueryBuilder Execute { get; }

    void Truncate(string tableName);
    void Truncate<TTable>() where TTable : ITable, new();


    IDisposable If(AbstractSqlCondition condition);
    IDisposable Else2();

    IDisposable IfExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> selector);
    IDisposable IfNotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> selector);


    IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition);
    void Else();



    void Begin();
    void End();

    AbstractSqlVariable DeclareNew(string type);
    AbstractSqlVariable DeclareNew(string type, AbstractSqlLiteral defaultValue);
    AbstractSqlVariable DeclareNew<T>(AbstractSqlLiteral defaultValue);

    /// <summary>
    /// Declares a new variable with unique name and returns it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    AbstractSqlVariable DeclareNew<T>();

    AbstractSqlVariable Declare(string variableName, string type);
    AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue);
    AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue);
    AbstractSqlVariable Declare<T>(string variableName);
    AbstractSqlVariable Declare<T>(string variableName, AbstractSqlLiteral defaultValue);



    //default values usually become literals
    AbstractSqlVariable Declare(string variableName, string type, Action<IQueryBuilder> builder);
    AbstractSqlVariable Declare<T>(string variableName, Action<IQueryBuilder> builder);

    AbstractSqlVariable Declare<T>(string variableName, ISqlExpression defaultValue);



    void SetToScopeIdentity(AbstractSqlVariable variable);
    void Set(AbstractSqlVariable variable, Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> right);
    void Set(AbstractSqlVariable variable, AbstractSqlExpression value);
    void Set(AbstractSqlVariable variable, ISqlExpression value);
    void Set(AbstractSqlVariable variable, AbstractSqlVariable value);
    void Set(AbstractSqlVariable variable, AbstractSqlLiteral value);



    void Return();
    void Return(ISqlExpression expression);
    void Return(AbstractSqlLiteral literal);
    void Comment(string comment);

    void Cursor(
        string cursorName,
        Action<ISelectQueryBuilder> selection,
        AbstractSqlVariable[] intoVariables,
        Action<IQueryBuilder> body);


    void Print(ISqlExpression expression);
    void Print(AbstractSqlLiteral literal);

    [Pure]
    AbstractSqlExpression Now();

    [Pure]
    AbstractSqlExpression Rand();

    [Pure]
    ISqlExpression Len(ISqlExpression expression);
    [Pure]
    ISqlExpression Cast(ISqlExpression expression, string asType);

    AbstractSqlExpression CallFunc(string functionName, string schema = null);
    AbstractSqlExpression CallFunc(string functionName, string schema = null, params AbstractSqlLiteral[] expressions);
    AbstractSqlExpression CallFunc(string functionName, string schema = null, params ISqlExpression[] expressions);
    AbstractSqlExpression CallFunc(string functionName, string schema = null, params AbstractSqlExpression[] expressions);

    void Goto(string labelName);
    IDisposable Label(string labelName);
    IDisposable While(AbstractSqlCondition condition);


    [Pure]
    AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start, ISqlExpression length);
    AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, AbstractSqlLiteral length);
    AbstractSqlExpression SubString(ISqlExpression expression, AbstractSqlLiteral start, ISqlExpression length);
    AbstractSqlExpression SubString(ISqlExpression expression, ISqlExpression start, AbstractSqlLiteral length);


    AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, ISqlExpression length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, AbstractSqlLiteral length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, AbstractSqlLiteral start, ISqlExpression length);
    AbstractSqlExpression SubString(AbstractSqlLiteral expression, ISqlExpression start, AbstractSqlLiteral length);


    AbstractSqlColumn Column(string columnName);
    AbstractSqlColumn Column(string columnName, string tableAlias);

    [Pure]
    AbstractSqlLiteral Literal(AbstractSqlLiteral literal);

    AbstractSqlLiteral Literal(string x, bool isUniCode);
    AbstractSqlLiteral Literal(DateTime x, bool includeTime);


    ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder);


    void BeginTransaction(string transactionName = null);
    void CommitTransaction(string transactionName = null);
    void RollbackTransaction(string transactionName = null);


    string Build();

    [Obsolete("This is a fallback for " +
              "If You don't find any Method to use for custom query," +
              "So If You Are Here Please create issue on github " +
              "page of SqlEngine Repository")]
    AbstractSqlExpression Raw(string rawSqlExpression);
    AbstractSqlExpression RawInternal(string rawSqlExpression);

    [Obsolete("This is a fallback for " +
              "If You don't find any Method to use for custom query," +
              "So If You Are Here Please create issue on github " +
              "page of SqlEngine Repository")]
    AbstractSqlCondition RawCondition(string rawConditionQuery);

    [Obsolete("This is a fallback for " +
              "If You don't find any Method to use for custom query," +
              "So If You Are Here Please create issue on github " +
              "page of SqlEngine Repository")]
    void AddExpression(string rawExpression);
}