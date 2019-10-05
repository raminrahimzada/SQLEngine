using System;
using System.Collections.Generic;
using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public  class SelectQueryBuilder: AbstractQueryBuilder
    {
        private sealed class JoinModel
        {
            public string TableName { get; set; }
            public string Alias { get; set; }
            public string JoinType { get; set; }
            public string ReferenceTableColumnName { get; set; }
            public string MainTableColumnName { get; set; }
        }

        private string _mainTableName;
        private string _mainTableAliasName;
        private List<string> _selectors;
        private string _whereClause;
        private int? _topClause;
        private List<JoinModel> _joinsList;

        private string _groupBy;
        private string _having;
      
        public SelectQueryBuilder From(string alias, string tableName)
        {
            _mainTableName = tableName;
            _mainTableAliasName = alias;
            return this;
        }
        public SelectQueryBuilder From(string tableName)
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
        public SelectQueryBuilder Selector(string selector)
        {
            if (_selectors == null) _selectors = new List<string>();
            _selectors.Add(selector);
            return this;
        }
        public SelectQueryBuilder Selector(string selector, string alias)
        {
            if (_selectors == null) _selectors = new List<string>();

            _selectors.Add(selector + $" {AS} " + alias);
            return this;
        }
        public SelectQueryBuilder Where(Func<AbstractConditionBuilder, AbstractConditionBuilder> builder)
        {
            _whereClause = builder.Invoke(GetDefault<AbstractConditionBuilder>()).Build();
            return this;
        }
        public SelectQueryBuilder Where(Func<BinaryExpressionBuilder, BinaryExpressionBuilder> builder)
        {
            _whereClause = builder.Invoke(GetDefault<BinaryExpressionBuilder>()).Build();
            return this;
        }
        public SelectQueryBuilder Where(Func<IfConditionBuilder, IfConditionBuilder> builder)
        {
            _whereClause = builder.Invoke(GetDefault<IfConditionBuilder>()).Build();
            return this;
        }
        public SelectQueryBuilder Where(string condition)
        {
            _whereClause = condition;
            return this;
        }

        public SelectQueryBuilder Top(int count)
        {
            _topClause = count;
            return this;
        }
        public SelectQueryBuilder InnerJoin(string alias, string tableName
            , string referenceTableColumnName,string mainTableColumnName)
        {
            if (_joinsList == null) _joinsList =new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName=mainTableColumnName,
                ReferenceTableColumnName=referenceTableColumnName,
                JoinType = INNERJOIN
            });
            return this;
        }
       
        public SelectQueryBuilder RightJoin(string alias, string tableName,
            string referenceTableColumnName,string mainTableColumnName)
        {
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = referenceTableColumnName,
                JoinType = RIGHTJOIN,
                
            });
            return this;
        }
        public SelectQueryBuilder LeftJoin(string alias, string tableName,
            string referenceTableColumnName,string mainTableColumnName)
        {
            if (_joinsList == null) _joinsList = new List<JoinModel>();
            _joinsList.Add(new JoinModel
            {
                TableName = tableName,
                Alias = alias,
                MainTableColumnName = mainTableColumnName,
                ReferenceTableColumnName = referenceTableColumnName,
                JoinType = LEFTJOIN
            });
            return this;
        }
       
        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write2(SELECT);
            if (_topClause != null)
            {
                Writer.Write(TOP);
                Writer.WriteWithScoped(_topClause.Value.ToString());
                Writer.Write2();
            }

            var hasSelector = _selectors != null && _selectors.Count > 0;
            if (!hasSelector)
            {
                //no selector then select *
                Writer.Write2(WILCARD);
            }
            else
            {
                Writer.WriteJoined(_selectors.ToArray());
            }
            Writer.Write2(FROM);
            Writer.Write(_mainTableName);

            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                Writer.Write2(AS);
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
                Writer.Write2(WHERE);
                Writer.Write(_whereClause);
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                Writer.Write2(GROUPBY);
                Writer.Write(_groupBy);
            } 
            
            if (!string.IsNullOrEmpty(_having))
            {
                Writer.Write2(HAVING);
                Writer.Write(_having);
            }

            return base.Build();
        }

        private string JoinQuery(JoinModel model)
        {
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{model.TableName} {AS} {model.Alias} {ON} {_mainTableAliasName}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{model.TableName} {ON} {_mainTableName}.{model.MainTableColumnName} = {model.TableName}.{model.ReferenceTableColumnName}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Alias))
                {
                    return
                        $"{model.JoinType}\t{model.TableName} {AS} {model.Alias} {ON} {_mainTableName}.{model.MainTableColumnName} = {model.Alias}.{model.ReferenceTableColumnName}";
                }
                else
                {
                    return
                        $"{model.JoinType}\t{model.TableName} {ON} {_mainTableName}.{model.MainTableColumnName} = {model.TableName}.{model.ReferenceTableColumnName}";
                }
            }
        }
    }
}