using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SQLEngine.SqlServer
{
    internal class SelectQueryBuilder<TTable> : SelectQueryBuilder,
        ISelectWithoutFromQueryBuilder<TTable>
    {
        public SelectQueryBuilder(SelectQueryBuilder builder)
        {
            _mainTableName = builder._mainTableName;
            _mainTableAlias = builder._mainTableAlias;
            _mainTableSchema = builder._mainTableSchema;
            _selectors = builder._selectors;
            _whereClause = builder._whereClause;
            _topClause = builder._topClause;
            _hasDistinct = builder._hasDistinct;
            _having = builder._having;

            _currentJoinModel = builder._currentJoinModel;
            _joinsList = builder._joinsList;
            _groupByClauses = builder._groupByClauses;
        }

        public ISelectWithoutWhereQueryBuilder Where(Expression<Func<TTable, bool>> expression)
        {
            _whereClause = Query.Settings.ExpressionCompiler.Compile(expression);
            return this;
        }
    }
    internal class SelectQueryBuilder : AbstractQueryBuilder, 
        ISelectQueryBuilder, 
        ISelectNoTopQueryBuilder,
        ISelectWithoutFromQueryBuilder,
        ISelectWithoutWhereQueryBuilder,
        ISelectWithoutFromAndGroupQueryBuilder,
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder,
        ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder,
        IJoinedNeedsOnQueryBuilder,
        IJoinedNeedsOnEqualsToQueryBuilder
    {
        internal sealed class OrderByQueryModel
        {
            private readonly IAbstractQueryBuilder _internalBuilder;
            private readonly bool? _isDesc;
            public OrderByQueryModel(AggregateFunctionBuilder aggregateFunction,bool? isDesc)
            {
                _internalBuilder = aggregateFunction;
                _isDesc = isDesc;
            }
            public OrderByQueryModel(CustomFunctionCallExpressionBuilder customFunctionCallExpressionBuilder,bool? isDesc=false)
            {
                _internalBuilder = customFunctionCallExpressionBuilder;
                _isDesc = isDesc;
            }

            public OrderByQueryModel(ISqlExpression expression, bool? isDesc = false)
            {
                string expressionSql = expression.ToSqlString();
                _internalBuilder = new RawStringQueryBuilder(w => w.Write(expressionSql));
                _isDesc = isDesc;
            }

            public void Build(ISqlWriter writer)
            {
                _internalBuilder.Build(writer);
                if (_isDesc??false)
                {
                    writer.Write(C.SPACE);
                    writer.Write(C.DESC);
                }
            }
        }
        
        internal class SelectorCollection
        {
            private readonly List<string> _rawSqlQueryList = new();

            public void Add(IAbstractQueryBuilder abstractQueryBuilder)
            {                
                var writer = SqlWriter.New;
                abstractQueryBuilder.Build(writer);
                _rawSqlQueryList.Add(writer.Build());
                writer.Dispose();
            }

            public void Add(AbstractSqlExpression expression)
            {
                _rawSqlQueryList.Add(expression.ToSqlString());
            }

            public void Add(OrderByQueryModel orderByQueryModel)
            {
                var writer = SqlWriter.New;
                orderByQueryModel.Build(writer);
                _rawSqlQueryList.Add(writer.Build());
                writer.Dispose();
            }

            public void Add(string rawExpression)
            {
                _rawSqlQueryList.Add(rawExpression);
            }

            
            public void Add(AbstractSqlColumn column)
            {
                _rawSqlQueryList.Add(column.ToSqlString());
            }

            public void Add(ISqlExpression expression)
            {
                _rawSqlQueryList.Add(expression.ToSqlString());
            }
            public int Count => _rawSqlQueryList.Count;

            public string[] ToArray()
            {
                return _rawSqlQueryList.ToArray();
            }

        }
        internal string _mainTableName;
        internal string _mainTableSchema;
        internal string _mainTableAlias;
        internal SelectorCollection _selectors = new();
        internal string _whereClause;
        internal int? _topClause;
        internal bool? _hasDistinct;
        internal string _having;

        internal JoinModel _currentJoinModel=new();

        internal List<JoinModel> _joinsList = new();
        internal List<OrderByQueryModel> _orderByClauses = new();
        internal List<string> _groupByClauses=new();
        private SelectQueryBuilder _actualBuilder;

        private static void MutateAliasName(ref string alias)
        {
            if (string.IsNullOrEmpty(alias)) return;
            if (string.IsNullOrWhiteSpace(alias)) return;
            if (alias.All(char.IsLetterOrDigit)) return;
            if (
                alias.StartsWith(C.BEGIN_SQUARE) &&
                alias.EndsWith(C.END_SQUARE)
            )
                return;
            alias = alias.Replace(C.BEGIN_SQUARE.ToString(), "\\" + C.BEGIN_SQUARE);
            alias = alias.Replace(C.END_SQUARE.ToString(), "\\" + C.END_SQUARE);
            alias = string.Concat(C.BEGIN_SQUARE, alias, C.END_SQUARE);
        }
        public ISelectWithoutFromQueryBuilder<TTable> From<TTable>(string tableName,string schema, string alias)
        {
            MutateAliasName(ref alias);
            _mainTableName = tableName;
            _mainTableSchema = schema;
            _mainTableAlias = alias;
            var newBuilder= new SelectQueryBuilder<TTable>(this);
            _actualBuilder = newBuilder;
            return newBuilder;
        }
        public ISelectWithoutFromQueryBuilder From(string tableName, string alias)
        {
            MutateAliasName(ref alias);
            _mainTableName = tableName;
            _mainTableAlias = alias;
            return this;
        }
        public ISelectWithoutFromQueryBuilder From(string tableName)
        {
            _mainTableName = tableName;
            return this;
        }
        public ISelectWithoutFromQueryBuilder Schema(string schema)
        {
            _mainTableSchema = schema;
            return this;
        }
        public ISelectWithoutFromQueryBuilder<TTable> From<TTable>() where TTable : ITable,new()
        {
            using (var table=new TTable())
            {
                _mainTableName = table.Name;
                _mainTableSchema = table.Schema;
                var builder = new SelectQueryBuilder<TTable>(this);
                _actualBuilder = builder;
                return builder;
            }
        }

        public ISelectWithoutFromQueryBuilder<TView> FromView<TView>() where TView : IView, new()
        {
            using (var table=new TView())
            {
                _mainTableName = table.Name;
                _mainTableSchema = table.Schema;
                var builder = new SelectQueryBuilder<TView>(this);
                _actualBuilder = builder;
                return builder;
            }
        }
        public ISelectWithoutFromQueryBuilder<TView> FromView<TView>(string alias) where TView : IView, new()
        {
            using (var table = new TView())
            {
                _mainTableName = table.Name;
                _mainTableSchema = table.Schema;
                _mainTableAlias = alias;
                var builder = new SelectQueryBuilder<TView>(this);
                _actualBuilder = builder;
                return builder;
            }
        }
        public ISelectWithoutFromQueryBuilder<TTable> From<TTable>(string alias) where TTable : ITable,new()
        {
            using (var table = new TTable())
            {
                return From<TTable>(table.Name, table.Schema, alias);
            }
        }

        
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, ISqlExpression right)
        {
            _selectors.Add(new CustomFunctionCallExpressionBuilder().Assign(left, right));
            return this;
        }
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlLiteral literal)
        {
            _selectors.Add(new CustomFunctionCallExpressionBuilder().Assign(left, literal));
            return this;
        }
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left, AbstractSqlColumn column)
        {
            _selectors.Add(new CustomFunctionCallExpressionBuilder().Assign(left, column));
            return this;
        }
        

        public ISelectWithSelectorQueryBuilder Select(ISqlExpression expression)
        {
            _selectors.Add(expression);
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column)
        {
            _selectors.Add(column);
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(string columnName)
        {
            _selectors.Add(new SqlServerColumn(columnName));
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(string columnName,string tableAlias)
        {
            _selectors.Add(new SqlServerColumn(columnName, tableAlias));
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(Action<IAggregateFunctionBuilder> body)
        {
            var a = new AggregateFunctionBuilder();
            body(a);
            _selectors.Add(new OrderByQueryModel(a, false));
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen)
        {
            using (var t=new CaseWhenQueryBuilder())
            {
                caseWhen(t);
                _selectors.Add(t.Build());
                return this;
            }
        }
        public ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFunctionCallExpression)
        {
            using (var t=new CustomFunctionCallExpressionBuilder())
            {
                customFunctionCallExpression(t);
                _selectors.Add(t.Build());
                return this;
            }
        }
        public ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate)
        {
            using (var b = new AggregateFunctionBuilder())
            {
                aggregate(b);
                _selectors.Add(new OrderByQueryModel(b, false));
                return this;
            }
        }

        public ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallNopBuilder> aggregate)
        {
            using (var b = new CustomFunctionCallExpressionBuilder())
            {
                aggregate(b);
                _selectors.Add(new OrderByQueryModel(b));
                return this;
            }
        }



        public ISelectWithSelectorQueryBuilder SelectAs(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFunctionCallExpression, string alias)
        {
            using (var t=new CustomFunctionCallExpressionBuilder())
            {
                customFunctionCallExpression(t);
                _selectors.Add(C.BEGIN_SCOPE+t.Build()+C.END_SCOPE + C.SPACE + C.AS + C.SPACE + alias);
                return this;
            }
        }
        public ISelectWithSelectorQueryBuilder SelectAs(ISqlExpression selector, string alias)
        {
            MutateAliasName(ref alias);
            _selectors.Add($"({selector.ToSqlString()}) {C.AS} {alias}");
            return this;
        }

        public ISelectWithSelectorQueryBuilder SelectAs(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen, string alias)
        {
            MutateAliasName(ref alias);
            using (var t = new CaseWhenQueryBuilder())
            {
                caseWhen(t);
                _selectors.Add($"({t.Build()}) {C.AS} {alias}");
                return this;
            }
        }
        


        public ISelectWithSelectorQueryBuilder SelectLiteral(AbstractSqlLiteral literal)
        {
            _selectors.Add(literal);
            return this;
        }
        public ISelectWithoutWhereQueryBuilder WhereAnd(params AbstractSqlCondition[] conditions)
        {
            _whereClause = string.Join(C.AND,
                conditions
                    .Select(x => x.ToSqlString())
                    .Select(x => string.Concat(C.BEGIN_SCOPE, x, C.END_SCOPE))
            );
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereOr(params AbstractSqlCondition[] conditions)
        {
            _whereClause = string.Join(C.OR,
                conditions
                    .Select(x => x.ToSqlString())
                    .Select(x => string.Concat(C.BEGIN_SCOPE, x, C.END_SCOPE))
            );
            return this;
        }


        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right)
        {
            var col = new SqlServerColumn(columnName);
            _whereClause = string.Concat(col.ToSqlString(), C.EQUALS, right.ToSqlString());
            return this;
        }
        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlLiteral literal)
        {
            var col = new SqlServerColumn(columnName);
            _whereClause = string.Concat(col.ToSqlString(), C.EQUALS, literal.ToSqlString());
            return this;
        }
        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlVariable variable)
        {
            var col = new SqlServerColumn(columnName);
            _whereClause = string.Concat(col.ToSqlString(), C.EQUALS, variable.ToSqlString());
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, ISqlExpression right)
        {
            _whereClause = string.Concat(column.ToSqlString(), C.EQUALS, right.ToSqlString());
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, AbstractSqlLiteral literal)
        {
            _whereClause = string.Concat(column.ToSqlString(), C.EQUALS, literal.ToSqlString());
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(AbstractSqlColumn column, AbstractSqlVariable variable)
        {
            _whereClause = string.Concat(column.ToSqlString(), C.EQUALS, variable.ToSqlString());
            return this;
        }

        public ISelectWithoutWhereQueryBuilder Where(AbstractSqlCondition condition)
        {
            _whereClause = condition.ToSqlString();
            return this;
        }

        public ISelectNoTopQueryBuilder Top(int count)
        {
            _topClause = count;
            return this;
        }

        public ISelectQueryBuilder Distinct()
        {
            _hasDistinct = true;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            if (_actualBuilder != null)
            {
                _actualBuilder.Build(writer);
                return;
            }
            ValidateAndThrow();
            writer.Write(C.SELECT);
            writer.Write(C.SPACE);
            if (_hasDistinct != null)
            {
                writer.Write(C.DISTINCT);
                writer.Write2();
            }
            if (_topClause != null)
            {
                writer.Write(C.TOP);
                writer.WriteScoped(_topClause.Value.ToString());
                writer.Write2();
            }

            var hasSelector = _selectors != null && _selectors.Count > 0;
            if (!hasSelector)
            {
                //no selector then select *
                writer.Write2(C.WILCARD);
            }
            else
            {
                writer.WriteJoined(_selectors.ToArray());
            }

            //simple select -> select 1 as A 'test' as B
            if (string.IsNullOrWhiteSpace(_mainTableName))
            {
                writer.WriteLine();
                return;
            }

            writer.WriteLine();
            writer.Indent++;
            writer.Write(C.FROM);
            writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_mainTableName))
            {
                if (!string.IsNullOrWhiteSpace(_mainTableSchema))
                {
                    writer.Write(_mainTableSchema);
                    writer.Write(C.DOT);
                }
                writer.Write(Query.Settings.EscapeStrategy.Escape(_mainTableName));
            }
            writer.Indent--;
            if (!string.IsNullOrEmpty(_mainTableAlias))
            {
                writer.Write2(C.AS);
                writer.Write(_mainTableAlias);
            }

            if (_joinsList != null)
            {
                foreach (var joinModel in _joinsList)
                {
                    writer.WriteNewLine();
                    writer.Write(joinModel.JoinQuery());
                }
            }

            WhereClause(writer);
            GroupByClause(writer);
            HavingClause(writer);
            OrderByClause(writer);
            writer.WriteLine();
        }

        #region helper methods

        

        private void WhereClause(ISqlWriter writer)
        {

            if (!string.IsNullOrEmpty(_whereClause))
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.WHERE);
                writer.Write(C.SPACE);
                writer.Write(_whereClause);
                writer.Indent--;
            }

        }

        private void HavingClause(ISqlWriter writer)
        {
            if (!string.IsNullOrEmpty(_having))
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.HAVING);
                writer.Write(C.SPACE);
                writer.Write(_having);
                writer.Indent--;
            }
        }

        private void GroupByClause(ISqlWriter writer)
        {
            if (_groupByClauses.Any())
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.GROUP);
                writer.Write(C.SPACE);
                writer.Write(C.BY);
                writer.Write(C.SPACE);
                bool first = true;
                foreach (var groupByClause in _groupByClauses)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        writer.Write(C.COMMA);
                    }
                    writer.Write(groupByClause);
                }
                writer.Write(C.SPACE);

                writer.Indent--;
            }
        }

        private void OrderByClause(ISqlWriter writer)
        {
            if (_orderByClauses != null && _orderByClauses.Any())
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.ORDER);
                writer.Write2(C.BY);
                bool first = true;

                foreach (var orderByClause in _orderByClauses)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        writer.Write(C.COMMA);
                    }

                    orderByClause.Build(writer);
                }
            }
        }
        #endregion

        public ISelectOrderBuilder OrderBy(ISqlExpression expression)
        {
            _orderByClauses.Add(new OrderByQueryModel(expression));
            return this;
        }
        public ISelectOrderBuilder OrderBy(AbstractSqlColumn column)
        {
            _orderByClauses.Add(new OrderByQueryModel(column));
            return this;
        }
        public ISelectOrderBuilder OrderBy(string columnName)
        {
            return OrderBy(new SqlServerColumn(columnName));
        }


        public ISelectOrderBuilder OrderByDesc(string columnName)
        {
            return OrderByDesc(new SqlServerColumn(columnName));
        }
        public ISelectOrderBuilder OrderByDesc(ISqlExpression expression)
        {
            _orderByClauses.Add(new OrderByQueryModel(expression, true));
            return this;
        }
        public ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column)
        {
            _orderByClauses.Add(new OrderByQueryModel(column, true));
            return this;
        }
        

        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(params ISqlExpression[] expressions)
        {
            _groupByClauses.AddRange(expressions.Select(x => x.ToSqlString()));
            return this;
        }
       
      
      
        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(params AbstractSqlColumn[] columns)
        {
            _groupByClauses.AddRange(columns.Select(x => x.ToSqlString()));
            return this;
        }
        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(params SqlServerColumn[] columns)
        {
            _groupByClauses.AddRange(columns.Select(x => x.ToSqlString()));
            return this;
        }
        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(params string[] columnNames)
        {
            var columns= columnNames.Select(columnName => new SqlServerColumn(columnName)).ToArray();
            return GroupBy(columns);
        }

        public ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition)
        {
            _having = condition.ToSqlString();
            return this;
        }



        
        public ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder 
            OrderBy(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate)
        {
            var b = new AggregateFunctionBuilder();
            aggregate(b);
            _orderByClauses.Add(new OrderByQueryModel(b, false));
            return this;
        }

        public ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder OrderByDesc(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate)
        {
            using (var b = new AggregateFunctionBuilder())
            {
                aggregate(b);
                _orderByClauses.Add(new OrderByQueryModel(b, false));
                return this;
            }
        }

        public IJoinedNeedsOnQueryBuilder InnerJoin(string targetTableName,string schema, string targetTableAlias)
        {
            _currentJoinModel.TableName = targetTableName;
            _currentJoinModel.TableAlias = targetTableAlias;
            _currentJoinModel.TableSchema = schema;
            _currentJoinModel.JoinType = SqlServerJoinTypes.InnerJoin;
            return this;
        }

        public IJoinedNeedsOnQueryBuilder InnerJoin<TTable>(string targetTableAlias) where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return InnerJoin(table.Name,table.Schema, targetTableAlias);
            }
        } 
        public IJoinedNeedsOnQueryBuilder RightJoin(string targetTableName, string schema, string targetTableAlias)
        {
            _currentJoinModel.TableName = targetTableName;
            _currentJoinModel.TableAlias = targetTableAlias;
            _currentJoinModel.TableSchema = schema;
            _currentJoinModel.JoinType = SqlServerJoinTypes.RightJoin;
            return this;
        }

        public IJoinedNeedsOnQueryBuilder RightJoin<TTable>(string targetTableAlias) where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return RightJoin(table.Name,table.Schema, targetTableAlias);
            }
        } 
        
        
        public IJoinedNeedsOnQueryBuilder LeftJoin(string targetTableName,string targetTableSchema, string targetTableAlias)
        {
            _currentJoinModel.TableName = targetTableName;
            _currentJoinModel.TableAlias = targetTableAlias;
            _currentJoinModel.TableSchema = targetTableSchema;
            _currentJoinModel.JoinType = SqlServerJoinTypes.LeftJoin;
            return this;
        }

        public IJoinedNeedsOnQueryBuilder LeftJoin<TTable>(string targetTableAlias) where TTable : ITable, new()
        {
            using (var table=new TTable())
            {
                return LeftJoin(table.Name,table.Schema, targetTableAlias);
            }
        }

        public IJoinedQueryBuilder On(AbstractSqlCondition condition)
        {
            _currentJoinModel.Condition = condition;
            _joinsList.Add(_currentJoinModel);
            _currentJoinModel = new JoinModel();
            return this;
        }

        private string _targetTableColumn;
        private string _targetTableAlias;

        public IJoinedNeedsOnEqualsToQueryBuilder OnColumn(string targetTableColumn, string targetTableAlias)
        {
            _targetTableColumn = targetTableColumn;
            _targetTableAlias = targetTableAlias;
            return this;
        }
        public IJoinedNeedsOnEqualsToQueryBuilder OnColumn(string targetTableColumn)
        {
            _targetTableColumn = targetTableColumn;
            _targetTableAlias = _currentJoinModel.TableAlias;
            return this;
        }

        public IJoinedQueryBuilder IsEqualsTo(string sourceTableColumnName)
        {
            return IsEqualsTo(sourceTableColumnName, _mainTableAlias);
        }
        public IJoinedQueryBuilder IsEqualsTo(string sourceTableColumnName, string sourceTableAlias)
        {
            using (var q = Query.New)
            {
                var colTarget = q.Column(_targetTableColumn, _targetTableAlias);
                var colSource = q.Column(sourceTableColumnName, sourceTableAlias);
                _currentJoinModel.Condition = colTarget == colSource;
                _joinsList.Add(_currentJoinModel);
                _currentJoinModel = new JoinModel();
                return this;
            }
        }
    }
}