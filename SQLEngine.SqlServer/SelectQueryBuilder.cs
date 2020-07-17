using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class SelectQueryBuilder : AbstractQueryBuilder, ISelectQueryBuilder, ISelectNoTopQueryBuilder,
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

        public ISelectWithSelectorQueryBuilder SelectAssign(string left, Func<IBinaryExpressionBuilder, IBinaryExpressionNopBuilder> right)
        {
            using (var q = new BinaryExpressionBuilder())
            {
                return SelectAssign(left, right(q).Build());
            }
        }
        public ISelectWithSelectorQueryBuilder SelectAssign(string left, string right)
        {
            if (_selectors == null) _selectors = new List<string>();
            var query = left + " = " + right;
            _selectors.Add(query);
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(ISqlExpression expression)
        {
            if (_selectors == null) _selectors = new List<string>();
            _selectors.Add(expression.ToSqlString());
            return this;
        }
        public ISelectWithSelectorQueryBuilder Select(AbstractSqlColumn column)
        {
            if (_selectors == null) _selectors = new List<string>();
            _selectors.Add(column.ToSqlString());
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
            if (_selectors == null) _selectors = new List<string>();
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
            if (_selectors == null) _selectors = new List<string>();
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

        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write(C.SELECT);
            Writer.Write(C.SPACE);
            if (_hasDistinct != null)
            {
                Writer.Write(C.DISTINCT);
                Writer.Write2();
            }
            if (_topClause != null)
            {
                Writer.Write(C.TOP);
                Writer.WriteScoped(_topClause.Value.ToString());
                Writer.Write2();
            }

            var hasSelector = _selectors != null && _selectors.Count > 0;
            if (!hasSelector)
            {
                //no selector then select *
                Writer.Write2(C.WILCARD);
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
            Writer.Write(C.FROM);
            Writer.Write(C.SPACE);
            if (string.IsNullOrWhiteSpace(_mainTableQuery))
            {
                Writer.Write(I(_mainTableName));
            }
            else
            {
                Writer.Write(C.BEGIN_SCOPE);
                Writer.Write(C.SPACE);
                Writer.Write(_mainTableQuery);
                Writer.Write(C.SPACE);
                Writer.Write(C.END_SCOPE);
            }
            Writer.Indent--;
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                Writer.Write2(C.AS);
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
                Writer.Write(C.WHERE);
                Writer.Write(C.SPACE);
                Writer.Write(_whereClause);
                Writer.Indent--;
            }

            if (_orderByClauses != null && _orderByClauses.Any())
            {
                Writer.Write(C.ORDER);
                Writer.Write2(C.BY);
                var orderByClauses = _orderByClauses.Select(tuple =>
                    I(tuple.Item2) + C.SPACE + (tuple.Item1 ? C.ASC : C.DESC));
                Writer.Write(orderByClauses.JoinWith());
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.Write(C.GROUPBY);
                Writer.Write(C.SPACE);
                Writer.Write(_groupBy);
                Writer.Indent--;
            }

            if (!string.IsNullOrEmpty(_having))
            {
                Writer.WriteLine();
                Writer.Indent++;
                Writer.Write(C.HAVING);
                Writer.Write(C.SPACE);
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