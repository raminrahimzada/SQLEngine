using System;

namespace SQLEngine.SqlServer
{
    internal class TryCatchQueryBuilder :AbstractQueryBuilder
        , ITryQueryBuilder
        , ITryNoTryQueryBuilder
        , ITryNoCatchQueryBuilder
    {
        private Action<IQueryBuilder> _tryBody;
        private Action<ICatchFunctionQueryBuilder> _catchBody;
        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.BEGIN);
            writer.Write(C.SPACE);
            writer.Write(C.TRY);
            writer.WriteLine();
            
            if (_tryBody!=null)
            {
                using (var q = new SqlServerQueryBuilder())
                {
                    _tryBody.Invoke(q);
                    q.Build(writer);
                }
            }

            writer.WriteLine();
            writer.Write(C.END);
            writer.Write(C.SPACE);
            writer.Write(C.TRY);
            
            writer.WriteLine();
            
            writer.Write(C.BEGIN);
            writer.Write(C.SPACE);
            writer.Write(C.CATCH);
            writer.WriteLine();

            if (_catchBody != null)
            {
                using (var q = new CatchFunctionQueryBuilder())
                {
                    _catchBody.Invoke(q);
                    q.Build(writer);
                }
            }
            
            writer.WriteLine();
            writer.Write(C.END);
            writer.Write(C.SPACE);
            writer.Write(C.CATCH);
        }

        public ITryNoTryQueryBuilder Try(Action<IQueryBuilder> builder)
        {
            _tryBody = builder;
            return this;
        }

        public ITryNoCatchQueryBuilder Catch(Action<ICatchFunctionQueryBuilder> builder)
        {
            _catchBody = builder;
            return this;
        }
    }
    internal class SqlServerCondition : AbstractSqlCondition
    {
        private readonly string _rawSqlString;

        public SqlServerCondition(string rawSqlString)
        {
            _rawSqlString = rawSqlString;
        }
        public SqlServerCondition(params string[] rawSqlStringParts)
        {
            _rawSqlString = string.Concat(rawSqlStringParts);
        }

        public override string ToSqlString()
        {
            return _rawSqlString;
        }

        public override AbstractSqlCondition And(AbstractSqlCondition condition)
        {
            var result = "(" + ToSqlString() + ") AND (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public override AbstractSqlCondition Or(AbstractSqlCondition condition)
        {
            var result = "(" + ToSqlString() + ") OR (" + condition.ToSqlString() + ")";
            return Raw(result);
        }

        public static SqlServerCondition Raw(string rawSqlString)
        {
            return new SqlServerCondition(rawSqlString);
        }
    }
}