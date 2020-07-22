using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            private readonly bool? isDesc;
            public OrderByQueryModel(AggregateFunctionBuilder aggregateFunction,bool? isDesc=false)
            {
                _internalBuilder = aggregateFunction;
                this.isDesc = isDesc;
            }

            public OrderByQueryModel(ISqlExpression expression, bool? isDesc = false)
            {
                string expressionSql = expression.ToSqlString();
                _internalBuilder = new RawStringQueryBuilder(w => w.Write(expressionSql));
                this.isDesc = isDesc;
            }

            public void Build(ISqlWriter Writer)
            {
                _internalBuilder.Build(Writer);
                if (isDesc??false)
                {
                    Writer.Write(C.SPACE);
                    Writer.Write(C.DESC);
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

            public void Add(ISqlExpression expression)
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

        public ISelectWithoutFromQueryBuilder FromSubQuery(string query, string alias)
        {
            MutateAliasName(ref alias);
            _mainTableQuery = query;
            _mainTableAliasName = alias;
            return this;
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
        
        

        //public ISelectWithSelectorQueryBuilder SelectAssign(string left, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right)
        //{
        //    using (var q = new BinaryExpressionBuilder())
        //    {
        //        return SelectAssign(left, right(q).Build());
        //    }
        //}
        public ISelectWithSelectorQueryBuilder SelectAssign(AbstractSqlVariable left,
            ISqlExpression right)
        {
            _selectors.Add(new BinaryExpressionBuilder().Assign(left,right));
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
        public ISelectWithSelectorQueryBuilder Select(Action<IAggregateFunctionBuilder> body)
        {
            var a = new AggregateFunctionBuilder();
            body(a);
            _selectors.Add(new OrderByQueryModel(a));
            return this;
        }

        //public ISelectWithSelectorQueryBuilder Select(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenNeedWhenQueryBuilder> selectorBuilder, string alias)
        //{
        //    MutateAliasName(ref alias);
        //    var selection = selectorBuilder(GetDefault<CaseWhenQueryBuilder>()).Build();
        //    return Select(selection, alias);
        //}

        public ISelectWithSelectorQueryBuilder Select(ISqlExpression selector, string alias)
        {
            MutateAliasName(ref alias);
            //if (SQLKeywords.GetAll().Any(k => k == alias.ToUpperInvariant()))
            //{
            //    alias = SQLKeywords.BEGIN_SCOPE + alias + SQLKeywords.END_SCOPE;
            //}
            _selectors.Add($"{selector} {C.AS} {alias}" );
            return this;
        }

        //public ISelectWithSelectorQueryBuilder Select(Func<IConditionFilterQueryHelper, string> helperBuilder)
        //{
        //    using (var builder = Query.New)
        //    {
        //        return Select(helperBuilder(builder.Helper));
        //    }
        //}

        //public ISelectWithSelectorQueryBuilder Select(Func<IConditionFilterQueryHelper, string> helperBuilder, string alias)
        //{
        //    MutateAliasName(ref alias);
        //    using (var builder = Query.New)
        //    {
        //        return Select(helperBuilder(builder.Helper), alias);
        //    }
        //}

        public ISelectWithSelectorQueryBuilder SelectCol(string tableAlias, string columnName, string alias = null)
        {
            MutateAliasName(ref alias);
            _selectors.Add(string.IsNullOrEmpty(alias)
                ? $"{tableAlias}.{columnName}"
                : $"{tableAlias}.{columnName} {C.AS} {alias}");
            return this;
        }

        //public ISelectWithoutWhereQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        //{
        //    _whereClause = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
        //    return this;
        //}
        //public ISelectWithoutWhereQueryBuilder Where(Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder> builder)
        //{
        //    _whereClause = builder.Invoke(GetDefault<BinaryConditionExpressionBuilder>()).Build();
        //    return this;
        //}
        public ISelectWithoutWhereQueryBuilder WhereColumnEquals(string columnName, ISqlExpression right)
        {
            var col = new SqlServerColumn(columnName);
            using (var b = Query.New)
                _whereClause = b.Helper.Equal(col, right);
            return this;
        }

        //public ISelectWithoutWhereQueryBuilder WhereIDIs(long id)
        //{
        //    using (var b = Query.New)
        //        _whereClause = b.Helper.Equal("ID", id.ToSQL());
        //    return this;
        //}

        //public ISelectWithoutWhereQueryBuilder WhereIDIs(string sqlVariable)
        //{
        //    using (var b = Query.New)
        //        _whereClause = b.Helper.Equal("ID", sqlVariable);
        //    return this;
        //}
        //public ISelectWithoutWhereQueryBuilder Where(Func<ConditionBuilder, ConditionBuilder> builder)
        //{
        //    _whereClause = builder.Invoke(GetDefault<ConditionBuilder>()).Build();
        //    return this;
        //}

        public ISelectWithoutWhereQueryBuilder WhereAnd(params string[] filters)
        {
            using (var b = Query.New)
            {
                _whereClause = b.Helper.And(filters);
            }

            return this;
        }

        //public ISelectWithoutWhereQueryBuilder WhereAnd(params Func<BinaryConditionExpressionBuilder, BinaryConditionExpressionBuilder>[] builders)
        //{
        //    _whereClause = builders.Select(builder => builder.Invoke(GetDefault<BinaryConditionExpressionBuilder>()).Build())
        //        .JoinWith(SQLKeywords.AND);
        //    return this;
        //}
        public ISelectWithoutWhereQueryBuilder Where(string condition)
        {
            _whereClause = condition;
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
        public ISelectOrderBuilder OrderByDesc(ISqlExpression expression)
        {
            _orderByClauses.Add(new OrderByQueryModel(expression, true));
            return this;
        }

        public ISelectOrderBuilder OrderBy(AbstractSqlColumn column)
        {
            _orderByClauses.Add(new OrderByQueryModel(column, false));
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