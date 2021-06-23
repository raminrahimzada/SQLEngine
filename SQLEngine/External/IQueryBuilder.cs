using System;

namespace SQLEngine
{
    public interface IQueryBuilder: IDisposable
    {
        IConditionFilterQueryHelper Helper { get; }
        void Clear();

        ISelectQueryBuilder Select { get; }
        IUpdateQueryBuilder Update { get; }
        IDeleteQueryBuilder Delete { get; }
        IInsertQueryBuilder Insert { get; }
        ICreateQueryBuilder Create { get; }
        IAlterQueryBuilder Alter { get; }
        IDropQueryBuilder Drop { get; }
        IExecuteQueryBuilder Execute { get; }
        
        /*
        not sure how to use these
        void Union();
        void UnionAll();
        */
        void Truncate(string tableName);
        void Truncate<TTable>() where TTable : ITable, new();
        
        IIfQueryBuilder IfOr(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder If(AbstractSqlCondition condition);
        
        IDisposable If2(AbstractSqlCondition condition);
        IDisposable Else2();

        IIfQueryBuilder IfNot(AbstractSqlCondition condition);
        IIfQueryBuilder IfExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selector);
        IIfQueryBuilder IfExists(IAbstractSelectQueryBuilder selection);
        IIfQueryBuilder IfNotExists(Func<IAbstractSelectQueryBuilder, IAbstractSelectQueryBuilder> selector);
        IIfQueryBuilder IfNotExists(IAbstractSelectQueryBuilder selection);


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
        AbstractSqlVariable Declare<T>(string variableName,  AbstractSqlLiteral defaultValue);
        
        

        //default values usually become literals
        //AbstractSqlVariable Declare(string variableName, string type, ISqlExpression defaultValue);
        //AbstractSqlVariable DeclareRandom(string variableName, string type, ISqlExpression defaultValue);
        AbstractSqlVariable Declare(string variableName, string type, Action<IQueryBuilder> builder);
        AbstractSqlVariable Declare<T>(string variableName, Action<IQueryBuilder> builder);

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
        
        void Cursor(
            string cursorName,
            Action<ISelectQueryBuilder> selection,
            AbstractSqlVariable[] intoVariables,
            Action<IQueryBuilder> body);


        void Print(ISqlExpression expression);
        void Print(AbstractSqlLiteral literal);
        
        AbstractSqlColumn Column(string columnName);
        AbstractSqlColumn Column(string columnName,string tableAlias);


        AbstractSqlLiteral Literal(AbstractSqlLiteral literal);

        AbstractSqlLiteral Literal(string x);
        AbstractSqlLiteral Literal(string x, bool isUniCode );
        AbstractSqlLiteral Literal(DateTime x);
        AbstractSqlLiteral Literal(DateTime x, bool includeTime );


        ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder);
        

        void BeginTransaction(string transactionName=null);
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


}