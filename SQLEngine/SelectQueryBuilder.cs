using System;
using System.Collections.Generic;

namespace SQLEngine
{
    public  class SelectQueryBuilder: AbstractQueryBuilder
    {
        private string _mainTableName;
        private string _mainTableAliasName;
        private List<string> _selectors;
        private string _whereClause;
        private int? _topClause;
        private List<string> _joinsList;

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

            _selectors.Add(selector + " AS " + alias);
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

        public SelectQueryBuilder InnerJoin(string alias, string tableName, string leftColumn, string rightColumn)
        {
            if (_joinsList == null) _joinsList =new List<string>();
            var line = $" INNER JOIN {tableName} AS {alias} ON {leftColumn} = {rightColumn}";
            _joinsList.Add(line);
            return this;
        }
       
        public SelectQueryBuilder RightJoin(string alias, string tableName, string leftColumn, string rightColumn)
        {
            if (_joinsList == null) _joinsList = new List<string>();
            var line = $" RIGHT JOIN {tableName} AS {alias} ON {leftColumn} = {rightColumn}";
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                line = $" RIGHT JOIN {tableName} AS {alias} ON {alias}.{leftColumn} = {_mainTableAliasName}.{rightColumn}";
            }
            _joinsList.Add( line);
            return this;
        }
        public SelectQueryBuilder LeftJoin(string alias, string tableName, string leftColumn, string rightColumn)
        {
            if (_joinsList == null) _joinsList = new List<string>();
            var line = $" LEFT JOIN {tableName} AS {alias} ON {alias}.{leftColumn} = {rightColumn}";
            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                line = $" LEFT JOIN {tableName} AS {alias} ON {alias}.{leftColumn} = {_mainTableAliasName}.{rightColumn}";
            }
            _joinsList.Add(line);
            return this;
        }
       
        public override string Build()
        {
            ValidateAndThrow();
            Writer.Write("SELECT ");
            if (_topClause != null)
            {
                Writer.Write("TOP");
                Writer.WriteWithScoped(_topClause.Value.ToString());
                Writer.Write(" ");
            }

            var hasSelector = _selectors != null && _selectors.Count > 0;
            if (!hasSelector)
            {
                //no selector then select *
                Writer.Write(" * ");
            }
            else
            {
                var selectorsJoined = string.Join(" , ", _selectors);
                Writer.Write(selectorsJoined);
            }
            Writer.Write(" FROM ");
            Writer.Write(_mainTableName);

            if (!string.IsNullOrEmpty(_mainTableAliasName))
            {
                Writer.Write(" AS ");
                Writer.Write(_mainTableAliasName);
            }

            if (_joinsList != null)
            {
                foreach (var joinQuery in _joinsList)
                {
                    Writer.Write(" \n ");
                    Writer.Write(joinQuery);
                }
            }
           
            if (!string.IsNullOrEmpty(_whereClause))
            {
                Writer.Write(" WHERE ");
                Writer.Write(_whereClause);
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                Writer.Write(" GROUP BY ");
                Writer.Write(_groupBy);
            } 
            
            if (!string.IsNullOrEmpty(_having))
            {
                Writer.Write(" HAVING ");
                Writer.Write(_having);
            }
            return base.Build();
        }
    }
}