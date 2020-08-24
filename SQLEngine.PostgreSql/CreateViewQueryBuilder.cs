using System;

namespace SQLEngine.PostgreSql
{
    internal class CreateViewQueryBuilder : AbstractQueryBuilder, ICreateViewNoNameQueryBuilder,
        ICreateViewNoNameNoBodyQueryBuilder
    {
        private string _viewName;
        private Action<ISelectQueryBuilder> _selectionBuilder;
        public ICreateViewNoNameNoBodyQueryBuilder As(Action<ISelectQueryBuilder> selectionBuilder)
        {
            _selectionBuilder = selectionBuilder;
            return this;
        }

        public ICreateViewNoNameQueryBuilder Name(string viewName)
        {
            _viewName = viewName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.CREATE);
            writer.Write(C.SPACE);
            writer.Write(C.VIEW);
            writer.Write(C.SPACE);
            writer.Write(_viewName);
            writer.Write(C.SPACE);
            writer.Write(C.AS);
            writer.Write(C.SPACE);
            using (var sb=new SelectQueryBuilder())
            {
                _selectionBuilder(sb);
                sb.Build(writer);
            }
        }
    }
}