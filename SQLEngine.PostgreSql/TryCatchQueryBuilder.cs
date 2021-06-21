using System;

namespace SQLEngine.PostgreSql
{
    [Obsolete("Do Not Use")]
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
                using (var q = new PostgreSqlQueryBuilder())
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
}