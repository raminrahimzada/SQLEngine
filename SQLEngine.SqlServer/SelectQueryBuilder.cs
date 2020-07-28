using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class SelectQueryBuilder : AbstractQueryBuilder, 
        ISelectQueryBuilder, 
        ISelectNoTopQueryBuilder,
        ISelectWithoutFromQueryBuilder,
        ISelectWithoutWhereQueryBuilder,
        ISelectWithoutFromAndGroupQueryBuilder,
        ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder,
        ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder
    {
        internal sealed class OrderByQueryModel
        {
            private readonly IAbstractQueryBuilder _internalBuilder;
            private readonly bool? _isDesc;
            public OrderByQueryModel(AggregateFunctionBuilder aggregateFunction,bool? isDesc=false)
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
        private sealed class JoinModel
        {
            public string TableName { get; set; }
            public string Alias { get; set; }
            public string JoinType { get; set; }
            public string ReferenceTableColumnName { get; set; }
            public string MainTableColumnName { get; set; }
            public string RawCondition { get; set; }
        }
        internal class SelectorCollection
        {
            private readonly List<string> _rawSqlQueryList = new List<string>();

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

            public int Count => _rawSqlQueryList.Count;

            public string[] ToArray()
            {
                return _rawSqlQueryList.ToArray();
            }

            public void Add(AbstractSqlColumn column)
            {
                _rawSqlQueryList.Add(column.ToSqlString());
            }

            public void Add(ISqlExpression expression)
            {
                _rawSqlQueryList.Add(expression.ToSqlString());
            }
        }
        private string _mainTableName;
        private string _mainTableQuery;
        private string _mainTableAliasName;
        private readonly SelectorCollection _selectors = new SelectorCollection();
        private string _whereClause;
        private int? _topClause;
        private bool? _hasDistinct;
        private List<JoinModel> _joinsList;

        private string _having;
        private readonly List<OrderByQueryModel> _orderByClauses = new List<OrderByQueryModel>();
        private readonly List<string> _groupByClauses=new List<string>();

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
            alias = alias.Replace(C.BEGIN_SQUARE, "\\" + C.BEGIN_SQUARE);
            alias = alias.Replace(C.END_SQUARE, "\\" + C.END_SQUARE);
            alias = string.Concat(C.BEGIN_SQUARE, alias, C.END_SQUARE);
        }

        public ISelectWithoutFromQueryBuilder From(string tableName, string alias)
        {
            MutateAliasName(ref alias);
            _mainTableName = tableName;
            _mainTableAliasName = alias;
            return this;
        }
        public ISelectWithoutFromQueryBuilder From(string tableName)
        {
            _mainTableName = tableName;
            return this;
        }
        
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left,
            AbstractSqlExpression right)
        {
            _selectors.Add(new CustomFunctionCallExpressionBuilder().Assign(left,right));
            return this;
        }
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left,
            AbstractSqlLiteral literal)
        {
            _selectors.Add(new CustomFunctionCallExpressionBuilder().Assign(left, literal));
            return this;
        }
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left,
            AbstractSqlColumn column)
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
        public ISelectWithSelectorQueryBuilder Select(AbstractSqlLiteral literal)
        {
            _selectors.Add(literal);
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(Action<IAggregateFunctionBuilder> body)
        {
            var a = new AggregateFunctionBuilder();
            body(a);
            _selectors.Add(new OrderByQueryModel(a));
            return this;
        }

        public ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias)
        {
            MutateAliasName(ref alias);
            _selectors.Add($"{selector} {C.AS} {alias}");
            return this;
        }
        public ISelectWithSelectorQueryBuilder SelectAs(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenQueryBuilder> caseWhen, string alias)
        {
            MutateAliasName(ref alias);
            using (var t=new CaseWhenQueryBuilder())
            {
                caseWhen(t);
                _selectors.Add($"{t.Build()} {C.AS} {alias}");
                return this;
            }
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
        public ISelectWithSelectorQueryBuilder Select(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFuncExp)
        {
            using (var t=new CustomFunctionCallExpressionBuilder())
            {
                customFuncExp(t);
                _selectors.Add(t.Build());
                return this;
            }
        }
        public ISelectWithSelectorQueryBuilder SelectAs(Func<ICustomFunctionCallExpressionBuilder, ICustomFunctionCallExpressionBuilder> customFuncExp, string asName)
        {
            using (var t=new CustomFunctionCallExpressionBuilder())
            {
                customFuncExp(t);
                _selectors.Add(t.Build() + C.SPACE + C.AS + C.SPACE + asName);
                return this;
            }
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

        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, AbstractSqlExpression right)
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

        public IJoinedQueryBuilder InnerJoin(string alias, string tableName, string mainTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = Query.Settings.DefaultIdColumnName,
                JoinType = C.INNERJOIN
            });
            return this;
        }

        public IJoinedQueryBuilder InnerJoin(string alias, string tableName
            , string mainTableColumnName, string referenceTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = referenceTableColumnName,
                JoinType = C.INNERJOIN
            });
            return this;
        }

        public IJoinedQueryBuilder InnerJoinRaw(string alias, string tableName, string condition)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                RawCondition=condition,
                JoinType = C.INNERJOIN
            });
            return this;
        }

        public IJoinedQueryBuilder RightJoin(string alias, string tableName, string mainTableColumnName,
            string referenceTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = referenceTableColumnName,
                JoinType = C.RIGHTJOIN,

            });
            return this;
        }

        public IJoinedQueryBuilder RightJoin(string alias, string tableName, string mainTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = Query.Settings.DefaultIdColumnName,
                JoinType = C.RIGHTJOIN,
            });
            return this;
        }

        public IJoinedQueryBuilder LeftJoin(string alias, string tableName,
            string mainTableColumnName, string referenceTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = referenceTableColumnName,
                JoinType = C.LEFTJOIN
            });
            return this;
        }
        public IJoinedQueryBuilder LeftJoin(string alias, string tableName,
            string mainTableColumnName)
        {
            MutateAliasName(ref alias);
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = Query.Settings.DefaultIdColumnName,
                JoinType = C.LEFTJOIN
            });
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
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
                if (string.IsNullOrWhiteSpace(_mainTableQuery))
                    return;
            }

            writer.WriteLine();
            writer.Indent++;
            writer.Write(C.FROM);
            writer.Write(C.SPACE);
            if (string.IsNullOrWhiteSpace(_mainTableQuery))
            {
                writer.Write(I(_mainTableName));
            }
            else
            {
                writer.Write(C.BEGIN_SCOPE);
                writer.Write(C.SPACE);
                writer.Write(_mainTableQuery);
                writer.Write(C.SPACE);
                writer.Write(C.END_SCOPE);
            }
            writer.Indent--;
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                writer.Write2(C.AS);
                writer.Write(_mainTableAliasName);
            }

            if (_joinsList != null)
            {
                foreach (var joinModel in _joinsList)
                {
                    writer.WriteNewLine();
                    writer.Write(JoinQuery(joinModel));
                }
            }

            if (!string.IsNullOrEmpty(_whereClause))
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.WHERE);
                writer.Write(C.SPACE);
                writer.Write(_whereClause);
                writer.Indent--;
            }

            

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

            if (!string.IsNullOrEmpty(_having))
            {
                writer.WriteLine();
                writer.Indent++;
                writer.Write(C.HAVING);
                writer.Write(C.SPACE);
                writer.Write(_having);
                writer.Indent--;
            }
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

        public ISelectOrderBuilder OrderBy(ISqlExpression expression)
        {
            _orderByClauses.Add(new OrderByQueryModel(expression));
            return this;
        }
        //public ISelectOrderBuilder OrderBy(AbstractSqlColumn column)
        //{
        //    _orderByClauses.Add(new OrderByQueryModel(column));
        //    return this;
        //}
        public ISelectOrderBuilder OrderByDesc(ISqlExpression expression)
        {
            _orderByClauses.Add(new OrderByQueryModel(expression, true));
            return this;
        }

        public ISelectOrderBuilder OrderBy(AbstractSqlColumn column)
        {
            _orderByClauses.Add(new OrderByQueryModel(column));
            return this;
        }

        public ISelectOrderBuilder OrderByDesc(AbstractSqlColumn column)
        {
            _orderByClauses.Add(new OrderByQueryModel(column, true));
            return this;
        }

        

        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(ISqlExpression expression)
        {
            _groupByClauses.Add(expression.ToSqlString());
            return this;
        }
        //public ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlExpression expression)
        //{
        //    _groupByClauses.Add(expression.ToSqlString());
        //    return this;
        //}
       
        public ISelectWithoutFromAndGroupNeedHavingConditionQueryBuilder Having(AbstractSqlCondition condition)
        {
            _having = condition.ToSqlString();
            return this;
        }

        public ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(ISqlExpression expression)
        {
            _groupByClauses.Add(expression.ToSqlString() + C.SPACE + C.DESC);
            return this;
        }

        public ISelectWithoutFromAndGroupQueryBuilder GroupBy(AbstractSqlColumn column)
        {
            _groupByClauses.Add(column.ToSqlString());
            return this;
        }

        public ISelectWithoutFromAndGroupQueryBuilder GroupByDesc(AbstractSqlColumn column)
        {
            _groupByClauses.Add(column.ToSqlString() + C.SPACE + C.DESC);
            return this;
        }


        public ISelectWithSelectorQueryBuilder Select(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate)
        {
            using (var b=new AggregateFunctionBuilder())
            {
                aggregate(b);
                _selectors.Add(new OrderByQueryModel(b));
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

        private string JoinQuery(JoinModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.RawCondition))
            {
                if (!string.IsNullOrEmpty(_mainTableAliasName))
                {
                    if (!string.IsNullOrEmpty(model.Alias))
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {C.AS} {model.Alias} {C.ON} {model.RawCondition}";
                    }
                    else
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {C.ON} {model.RawCondition}";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.Alias))
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {C.AS} {model.Alias} {C.ON} {model.RawCondition}";
                    }
                    else
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {C.ON} {model.RawCondition}";
                    }
                }
            }


            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {C.AS} {model.Alias} {C.ON} {_mainTableAliasName}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {C.ON} {I(_mainTableName)}.{model.MainTableColumnName} = {I(model.TableName)}.{model.ReferenceTableColumnName}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {C.AS} {model.Alias} {C.ON} {I(_mainTableName)}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {C.ON} {_mainTableName}.{model.MainTableColumnName} = {I(model.TableName)}.{model.ReferenceTableColumnName}";
                }
            }
        }

        public ISelectWithoutFromAndGroupNoNeedHavingConditionNeedOrderByQueryBuilder 
            OrderBy(Func<IAggregateFunctionBuilder, IAggregateFunctionBuilder> aggregate)
        {
            var b = new AggregateFunctionBuilder();
            aggregate(b);
            _orderByClauses.Add(new OrderByQueryModel(b));
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
    }
}