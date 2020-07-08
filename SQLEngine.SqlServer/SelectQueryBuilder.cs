using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class SelectQueryBuilder : SqlServerAbstractQueryBuilder, ISelectQueryBuilder, ISelectNoTopQueryBuilder,
        ISelectWithoutFromQueryBuilder,
        ISelectWithoutWhereQueryBuilder

    {
        private sealed class JoinModel
        {
            public string TableName { get; set; }
            public string Alias { get; set; }
            public string JoinType { get; set; }
            public string ReferenceTableColumnName { get; set; }
            public string MainTableColumnName { get; set; }
            public string RawCondition { get; set; }
        }

        private string _mainTableName;
        private string _mainTableQuery;
        private string _mainTableAliasName;
        private List<string> _selectors;
        private string _whereClause;
        private int? _topClause;
        private bool? _hasDistinct;
        private List<JoinModel> _joinsList;

        private string _groupBy;
        private string _having;
        private readonly List<Tuple<bool, string>> _orderByClauses = new List<Tuple<bool, string>>();

        private static void MutateAliasName(ref string alias)
        {
            if (string.IsNullOrEmpty(alias)) return;
            if (string.IsNullOrWhiteSpace(alias)) return;
            if (alias.All(char.IsLetterOrDigit)) return;
            if (
                alias.StartsWith(SQLKeywords.BEGIN_SQUARE) &&
                alias.EndsWith(SQLKeywords.END_SQUARE)
            )
                return;
            alias = alias.Replace(SQLKeywords.BEGIN_SQUARE, "\\" + SQLKeywords.BEGIN_SQUARE);
            alias = alias.Replace(SQLKeywords.END_SQUARE, "\\" + SQLKeywords.END_SQUARE);
            alias = string.Concat(SQLKeywords.BEGIN_SQUARE, alias, SQLKeywords.END_SQUARE);
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

        public SelectQueryBuilder GroupBy(string groupBy)
        {
            _groupBy = groupBy;
            return this;
        }
        public SelectQueryBuilder Having(string having)
        {
            _having = having;
            return this;
        }

        public ISelectWithSelectorQueryBuilder SelectorAssign(string left, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right)
        {
            using (var q = new BinaryExpressionBuilder())
            {
                return SelectorAssign(left, right(q).Build());
            }
        }
        public ISelectWithSelectorQueryBuilder SelectorAssign(string left, string right)
        {
            if (_selectors == null) _selectors = new List<string>();
            var query = left + " = " + right;
            _selectors.Add(query);
            return this;
        }
        public ISelectWithSelectorQueryBuilder Selector(string selector)
        {
            if (_selectors == null) _selectors = new List<string>();
            _selectors.Add(selector);
            return this;
        }

        public ISelectWithSelectorQueryBuilder Selector(Func<ICaseWhenNeedWhenQueryBuilder, ICaseWhenNeedWhenQueryBuilder> selectorBuilder, string alias)
        {
            MutateAliasName(ref alias);
            var selection = selectorBuilder(GetDefault<CaseWhenQueryBuilder>()).Build();
            return Selector(selection, alias);
        }

        public ISelectWithSelectorQueryBuilder Selector(string selector, string alias)
        {
            MutateAliasName(ref alias);
            if (_selectors == null) _selectors = new List<string>();
            if (SQLKeywords.GetAll().Any(k => k == alias.ToUpperInvariant()))
            {
                alias = SQLKeywords.BEGIN_SCOPE + alias + SQLKeywords.END_SCOPE;
            }
            _selectors.Add($"{selector} {SQLKeywords.AS} {alias}" );
            return this;
        }

        public ISelectWithSelectorQueryBuilder Selector(Func<IConditionFilterQueryHelper, string> helperBuilder)
        {
            using (var builder = QueryBuilderFactory.New)
            {
                return Selector(helperBuilder(builder.Helper));
            }
        }

        public ISelectWithSelectorQueryBuilder Selector(Func<IConditionFilterQueryHelper, string> helperBuilder, string alias)
        {
            MutateAliasName(ref alias);
            using (var builder = QueryBuilderFactory.New)
            {
                return Selector(helperBuilder(builder.Helper), alias);
            }
        }

        public ISelectWithSelectorQueryBuilder SelectorCol(string tableAlias, string columnName, string alias = null)
        {
            MutateAliasName(ref alias);
            if (_selectors == null) _selectors = new List<string>();
            _selectors.Add(string.IsNullOrEmpty(alias)
                ? $"{tableAlias}.{columnName}"
                : $"{tableAlias}.{columnName} {SQLKeywords.AS} {alias}");
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
        public ISelectWithoutWhereQueryBuilder WhereEquals(string left, string right)
        {
            using (var b = QueryBuilderFactory.New)
                _whereClause = b.Helper.Equal(left, right);
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereIDIs(long id)
        {
            using (var b = QueryBuilderFactory.New)
                _whereClause = b.Helper.Equal("ID", id.ToSQL());
            return this;
        }

        public ISelectWithoutWhereQueryBuilder WhereIDIs(string sqlVariable)
        {
            using (var b = QueryBuilderFactory.New)
                _whereClause = b.Helper.Equal("ID", sqlVariable);
            return this;
        }
        //public ISelectWithoutWhereQueryBuilder Where(Func<ConditionBuilder, ConditionBuilder> builder)
        //{
        //    _whereClause = builder.Invoke(GetDefault<ConditionBuilder>()).Build();
        //    return this;
        //}

        public ISelectWithoutWhereQueryBuilder WhereAnd(params string[] filters)
        {
            using (var b = QueryBuilderFactory.New)
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
                JoinType = SQLKeywords.INNERJOIN
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
                JoinType = SQLKeywords.INNERJOIN
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
                JoinType = SQLKeywords.RIGHTJOIN,

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
                JoinType = SQLKeywords.LEFTJOIN
            });
            return this;
        }

        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write(SQLKeywords.SELECT);
            Writer.Write(SQLKeywords.SPACE);
            if (_hasDistinct != null)
            {
                Writer.Write(SQLKeywords.DISTINCT);
                Writer.Write2();
            }
            if (_topClause != null)
            {
                Writer.Write(SQLKeywords.TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write2();
            }

            var hasSelector = _selectors != null && _selectors.Count > 0;
            if (!hasSelector)
            {
                //no selector then select *
                Writer.Write2(SQLKeywords.WILCARD);
            }
            else
            {
                Writer.WriteJoined(_selectors.ToArray());
            }

            //simple select -> select 1 as A 'test' as B
            if (string.IsNullOrWhiteSpace(_mainTableName))
            {
                if (string.IsNullOrWhiteSpace(_mainTableQuery))
                    return base.Build();
            }

            Writer.WriteLine();
            Writer.Indent++;
            Writer.Write(SQLKeywords.FROM);
            Writer.Write(SQLKeywords.SPACE);
            if (string.IsNullOrWhiteSpace(_mainTableQuery))
            {
                Writer.Write(I(_mainTableName));
            }
            else
            {
                Writer.Write(SQLKeywords.BEGIN_SCOPE);
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(_mainTableQuery);
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(SQLKeywords.END_SCOPE);
            }
            Writer.Indent--;
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                Writer.Write2(SQLKeywords.AS);
                Writer.Write(_mainTableAliasName);
            }

            if (_joinsList != null)
            {
                foreach (var joinModel in _joinsList)
                {
                    Writer.WriteNewLine();
                    Writer.Write(JoinQuery(joinModel));
                }
            }

            if (!string.IsNullOrEmpty(_whereClause))
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.Write(SQLKeywords.WHERE);
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(_whereClause);
                Writer.Indent--;
            }

            if (_orderByClauses != null && _orderByClauses.Any())
            {
                Writer.Write(SQLKeywords.ORDER);
                Writer.Write2(SQLKeywords.BY);
                var orderByClauses = _orderByClauses.Select(tuple =>
                    I(tuple.Item2) + SQLKeywords.SPACE + (tuple.Item1 ? SQLKeywords.ASC : SQLKeywords.DESC));
                Writer.Write(orderByClauses.JoinWith());
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.Write(SQLKeywords.GROUPBY);
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(_groupBy);
                Writer.Indent--;
            }

            if (!string.IsNullOrEmpty(_having))
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.Write(SQLKeywords.HAVING);
                Writer.Write(SQLKeywords.SPACE);
                Writer.Write(_having);
                Writer.Indent--;
            }

            return base.Build();
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
                            $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.AS} {model.Alias} {SQLKeywords.ON} {model.RawCondition}";
                    }
                    else
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.ON} {model.RawCondition}";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.Alias))
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.AS} {model.Alias} {SQLKeywords.ON} {model.RawCondition}";
                    }
                    else
                    {
                        return
                            $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.ON} {model.RawCondition}";
                    }
                }
            }


            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.AS} {model.Alias} {SQLKeywords.ON} {_mainTableAliasName}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.ON} {I(_mainTableName)}.{model.MainTableColumnName} = {I(model.TableName)}.{model.ReferenceTableColumnName}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.AS} {model.Alias} {SQLKeywords.ON} {I(_mainTableName)}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{I(model.TableName)} {SQLKeywords.ON} {_mainTableName}.{model.MainTableColumnName} = {I(model.TableName)}.{model.ReferenceTableColumnName}";
                }
            }
        }

        public ISelectOrderBuilder OrderBy(string orderFieldName)
        {
            _orderByClauses.Add(new Tuple<bool, string>(true, orderFieldName));
            return this;
        }
        public ISelectOrderBuilder OrderByDesc(string orderFieldName)
        {
            _orderByClauses.Add(new Tuple<bool, string>(false, orderFieldName));
            return this;
        }
    }
}