namespace SQLEngine.SqlServer
{
    internal class DropTriggerQueryBuilder : AbstractQueryBuilder,
        IDropTriggerNoNameQueryBuilder, IDropTriggerNoNameIfExistsQueryBuilder
    {
        private bool _checkIfExists;
        private string _triggerName;

        public IDropTriggerNoNameQueryBuilder Trigger(string triggerName)
        {
            _triggerName = triggerName;
            return this;
        }

        public override void Build(ISqlWriter writer)
        {
            writer.Write(C.DROP);
            writer.Write(C.SPACE);
            writer.Write(C.TRIGGER);
            writer.Write(C.SPACE);
            if (_checkIfExists)
            {
                writer.Write(C.IF);
                writer.Write(C.SPACE);
                writer.Write(C.EXISTS);
                writer.Write(C.SPACE);
            }

            writer.Write(_triggerName);
        }

        public IDropTriggerNoNameIfExistsQueryBuilder IfExists()
        {
            _checkIfExists = true;
            return this;
        }
    }
}