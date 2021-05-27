using System;

namespace SQLEngine.SqlServer
{
    internal class CreateTriggerQueryBuilder : AbstractQueryBuilder, ICreateTriggerNoNameQueryBuilder
    {
        private  string _triggerName;
        private  string _specification;
        private  Action<ITriggerBodyQueryBuilder> _body;
        private string _tableName;
        private string _tableSchema;
        private string _triggerSchemaName;

        public ICreateTriggerNoNameQueryBuilder Body(Action<ITriggerBodyQueryBuilder> body)
        {
            _body = body;
            return this;
        }

        public ICreateTriggerNoNameQueryBuilder On(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public ICreateTriggerNoNameQueryBuilder On(string tableName,string tableSchema)
        {
            _tableName = tableName;
            _tableSchema = tableSchema;
            return this;
        }

        public ICreateTriggerNoNameQueryBuilder ForDelete()
        {
            _specification = C.FOR + C.SPACE + C.DELETE;
            return this;
        }
        public ICreateTriggerNoNameQueryBuilder ForInsert()
        {
            _specification = C.FOR + C.SPACE + C.INSERT;
            return this;
        }
        public ICreateTriggerNoNameQueryBuilder AfterUpdate()
        {
            _specification = C.AFTER + C.SPACE + C.UPDATE;
            return this;
        }

        public ICreateTriggerNoNameQueryBuilder Name(string triggerName)
        {
            _triggerName = triggerName;
            return this;
        }
        public ICreateTriggerNoNameQueryBuilder Schema(string triggerSchemaName)
        {
            _triggerSchemaName = triggerSchemaName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.CREATE);
            writer.Write(C.SPACE);
            writer.Write(C.TRIGGER);
            writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_triggerSchemaName))
            {
                writer.Write(I(_triggerSchemaName));
                writer.Write(C.DOT);
            }
            writer.Write(I(_triggerName));
            writer.Write(C.SPACE);
            writer.Write(C.ON);
            writer.Write(C.SPACE);
            if (!string.IsNullOrWhiteSpace(_tableSchema))
            {
                writer.Write(I(_tableSchema));
                writer.Write(C.DOT);
            }
            writer.Write(I(_tableName));
            writer.Write(C.SPACE);
            writer.Write(_specification);
            writer.WriteLine(C.SPACE);
            writer.Write(C.AS);
            writer.WriteLine(C.SPACE);

            using (var x = new TriggerBodyQueryBuilder())
            {
                _body(x);
                x.Build(writer);
            }
        }
    }
}