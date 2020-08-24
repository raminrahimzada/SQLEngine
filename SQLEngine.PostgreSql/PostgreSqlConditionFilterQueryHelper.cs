using System;

namespace SQLEngine.PostgreSql
{
    internal class PostgreSqlConditionFilterQueryHelper : IConditionFilterQueryHelper
    { 
        public AbstractSqlCondition Exists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func)
        {
            using (var writer=SqlWriter.New)
            {
                writer.Write(C.EXISTS);
                writer.Write(C.BEGIN_SCOPE);
                using (var s=new SelectQueryBuilder())
                {
                    func(s);
                    s.Build(writer);
                }
                writer.Write(C.END_SCOPE);
                return new PostgreSqlCondition(writer.Build());
            }
        }

        public AbstractSqlCondition NotExists(Func<ISelectQueryBuilder, IAbstractSelectQueryBuilder> func)
        {
            using (var writer = SqlWriter.New)
            {
                writer.Write(C.NOT);
                writer.Write(C.SPACE);
                writer.Write(C.EXISTS);
                writer.Write(C.BEGIN_SCOPE);
                using (var s = new SelectQueryBuilder())
                {
                    func(s);
                    s.Build(writer);
                }
                writer.Write(C.END_SCOPE);
                return new PostgreSqlCondition(writer.Build());
            }
        }

        private AbstractSqlExpression _null;
        public AbstractSqlExpression Null
        {
            get
            {
                if (_null != null) return _null;
                _null = new PostgreSqlRawExpression(C.NULL);
                return _null;
            }
        }
        private AbstractSqlExpression _now;
        public AbstractSqlExpression Now
        {
            get
            {
                if (_now != null) return _now;
                _now = new PostgreSqlRawExpression("GETDATE()");
                return _now;
            }
        }
    }
}