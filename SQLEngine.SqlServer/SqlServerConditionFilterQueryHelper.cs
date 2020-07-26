using System;
    
namespace SQLEngine.SqlServer
{
    internal class SqlServerConditionFilterQueryHelper : IConditionFilterQueryHelper
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
                return new SqlServerCondition(writer.Build());
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
                return new SqlServerCondition(writer.Build());
            }
        }
    }
}