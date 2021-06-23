using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class CaseWhenQueryBuilder : AbstractQueryBuilder,
        ICaseWhenNeedWhenQueryBuilder, 
        ICaseWhenNeedThenQueryBuilder
    {
        private readonly List<string> _casesList = new();
        private string _currentWhen = string.Empty;
        private string _currentThen = string.Empty;
        private string _elseCase;

        
        public ICaseWhenNeedThenQueryBuilder When(AbstractSqlCondition condition)
        {
            _currentWhen = condition.ToSqlString();
            return this;
        }

        public ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, ISqlExpression expression)
        {
            _currentWhen = columnName + C.SPACE + C.EQUALS + C.SPACE + expression.ToSqlString();
            return this;
        }

        public ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias, string columnName, ISqlExpression expression)
        {
            _currentWhen = tableAlias + C.DOT + columnName + C.SPACE + C.EQUALS + C.SPACE + expression.ToSqlString();
            return this;
        }

        public ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string columnName, AbstractSqlLiteral literal)
        {
            _currentWhen = columnName + C.SPACE + C.EQUALS + C.SPACE + literal.ToSqlString();
            return this;
        }

        public ICaseWhenNeedThenQueryBuilder WhenColumnEquals(string tableAlias, string columnName, AbstractSqlLiteral literal)
        {
            _currentWhen = tableAlias + C.DOT + columnName + C.SPACE + C.EQUALS + C.SPACE + literal.ToSqlString();
            return this;
        }

        public ICaseWhenQueryBuilder Else(AbstractSqlLiteral literal)
        {
            _elseCase = literal.ToSqlString();
            return this;
        }

        public ICaseWhenQueryBuilder Else(ISqlExpression expression)
        {
            _elseCase = expression.ToSqlString();
            return this;
        }


        public ICaseWhenNeedWhenQueryBuilder Then(ISqlExpression then)
        {
            _currentThen = then.ToSqlString();
            Add();
            return this;
        }

        public ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlColumn column)
        {
            _currentThen = column.ToSqlString();
            Add();
            return this;
        }

        public ICaseWhenNeedWhenQueryBuilder Then(AbstractSqlLiteral then)
        {
            _currentThen = then.ToSqlString();
            Add();
            return this;
        }


        public ICaseWhenNeedWhenQueryBuilder ThenColumn(string @then)
        {
            return Then(@then);
        }
        public ICaseWhenNeedWhenQueryBuilder ThenColumn(string tableAlias, string columnName)
        {
            return Then(tableAlias + C.DOT + columnName);
        }

        private void Add()
        {
            string @case = $"{C.WHEN} {_currentWhen} {C.THEN} {_currentThen}";
            _casesList.Add(@case);
            _currentWhen = string.Empty;
            _currentThen = string.Empty;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.WriteLine();
            writer.Write(C.BEGIN_SCOPE);
            writer.Write(C.CASE);
            writer.Write(C.SPACE);
            foreach (string @case in _casesList)
            {
                writer.WriteLine(@case);
            }

            if (!string.IsNullOrWhiteSpace(_elseCase))
            {
                writer.Write(C.SPACE);
                writer.Write(C.ELSE);
                writer.Write(C.SPACE);
                writer.Write(_elseCase);
                writer.Write(C.SPACE);
            }
            writer.Write(C.END);
            writer.Write(C.END_SCOPE);
        }
    }
}