using System.Collections.Generic;

namespace SQLEngine.SqlServer
{
    internal class CaseWhenQueryBuilder : SqlServerAbstractQueryBuilder, ICaseWhenNeedWhenQueryBuilder, ICaseWhenNeedThenQueryBuilder
    {
        private readonly List<string> _casesList = new List<string>();
        private string _currentWhen = string.Empty;
        private string _currentThen = string.Empty;

        public ICaseWhenNeedThenQueryBuilder When(string @when)
        {
            _currentWhen = @when;
            return this;
        }
        public ICaseWhenNeedThenQueryBuilder WhenEquals(string columnName, string expression)
        {
            _currentWhen = columnName + " " + SQLKeywords.EQUALS + " " + expression;
            return this;
        }
        public ICaseWhenNeedWhenQueryBuilder Then(string @then)
        {
            _currentThen = @then;
            Add();
            return this;
        }
        public ICaseWhenNeedWhenQueryBuilder ThenColumn(string @then)
        {
            return Then(@then);
        }
        public ICaseWhenNeedWhenQueryBuilder ThenColumn(string tableAlias, string columnName)
        {
            return Then(tableAlias + SQLKeywords.DOT + columnName);
        }

        private void Add()
        {
            string @case = $"{SQLKeywords.WHEN} {_currentWhen} {SQLKeywords.THEN} {_currentThen}";
            _casesList.Add(@case);
            _currentWhen = string.Empty;
            _currentThen = string.Empty;
        }

        public override string Build()
        {
            Writer.WriteLine();
            Writer.Write(SQLKeywords.BEGIN_SCOPE);
            Writer.Write(SQLKeywords.CASE);
            Writer.Write(SQLKeywords.SPACE);
            foreach (string @case in _casesList)
            {
                Writer.WriteLine(@case);
            }
            Writer.Write(SQLKeywords.END);
            Writer.Write(SQLKeywords.END_SCOPE);
            return base.Build();
        }
    }
}