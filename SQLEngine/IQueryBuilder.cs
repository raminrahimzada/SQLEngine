using System;

namespace SQLEngine
{
    public interface IQueryBuilder:IDisposable
    {
        ISelectQueryBuilder Select { get; }
        IUpdateQueryBuilder Update { get; }
        IDeleteQueryBuilder Delete { get; }
        IInsertQueryBuilder Insert { get; }
        ICreateQueryBuilder Create { get; }
        IAlterQueryBuilder Alter { get; }
        IDropQueryBuilder Drop { get; }
        IConditionFilterQueryHelper Helper { get; }
        //void Create(Func<ICreateQueryBuilder, ICreateTableQueryBuilder> builder);
        //void Create(Func<ICreateQueryBuilder, IAbstractCreateFunctionQueryBuilder> builder);
        //void Select(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> builder);
        //void Union();
        //void UnionAll();
        void Truncate(string tableName);
        IIfQueryBuilder IfOr(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder IfAnd(params AbstractSqlCondition[] conditions);
        IIfQueryBuilder If(AbstractSqlCondition condition);
        IElseIfQueryBuilder ElseIf(AbstractSqlCondition condition);
        void Else();
        void Begin();
        void AddExpression(string expression);
        void End();
        //void Declare(Func<IDeclarationQueryBuilder, IDeclarationQueryBuilder> builder);
        AbstractSqlVariable DeclareRandom(string variableName, string type, AbstractSqlLiteral defaultValue = null);
        AbstractSqlVariable Declare(string variableName, string type, AbstractSqlLiteral defaultValue = null);
        void SetToScopeIdentity(AbstractSqlVariable variable);
        //void Set(ISqlVariable variable, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right);
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
        void Comment(string comment);
        string GenerateRandomVariableName(string beginning);
        //void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameNoSchemaNoDBQueryBuilder> builder);
        //void Drop(Func<IDropTableQueryBuilder, IDropTableNoNameQueryBuilder> builder);
        //void Drop(Func<IDropViewQueryBuilder, IDropViewQueryBuilder> builder);
        void Cursor(string selection,string[] intoVariables,Action<IQueryBuilder> body);
        void Print(ISqlExpression expression);
        //void Join(AbstractQueryBuilder other);
        string Build();
        AbstractSqlColumn Column(string columnName);
        AbstractSqlColumn Column(string columnName,string tableAlias);
    }
}