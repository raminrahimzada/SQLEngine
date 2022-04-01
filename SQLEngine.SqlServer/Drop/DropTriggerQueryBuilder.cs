namespace SQLEngine.SqlServer;

internal class DropProcedureQueryBuilder : AbstractQueryBuilder, IDropProcedureNoNameQueryBuilder, IDropProcedureNoNameNoSchemaNameQueryBuilder
{
    private string _procedureName;
    private string _schemaName;

    public IAbstractQueryBuilder Procedure(string procedureName)
    {
        _procedureName = procedureName;
        return this;
    }
    public IDropProcedureNoNameNoSchemaNameQueryBuilder Schema(string schemaName)
    {
        _schemaName = schemaName;
        return this;
    }

    public override void Build(ISqlWriter writer)
    {
        writer.Write(C.DROP);
        writer.Write(C.SPACE);
        writer.Write(C.PROCEDURE);
        writer.Write(C.SPACE);
            
        if (!string.IsNullOrWhiteSpace(_schemaName))
        {
            writer.Write(_schemaName);
            writer.Write(C.DOT);
        }
        writer.Write(_procedureName);
    }
}
internal class DropTriggerQueryBuilder : AbstractQueryBuilder,
    IDropTriggerNoNameQueryBuilder
    , IDropTriggerNoNameIfExistsQueryBuilder
    , IDropTriggerNoNameNoSchemaIfExistsQueryBuilder
{
    private bool _checkIfExists;
    private string _triggerName;
    private string _schemaName;

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

        if (!string.IsNullOrWhiteSpace(_schemaName))
        {
            writer.Write(_schemaName);
            writer.Write(C.DOT);
        }
        writer.Write(_triggerName);
    }

    public IDropTriggerNoNameNoSchemaIfExistsQueryBuilder Schema(string schemaName)
    {
        _schemaName = schemaName;
        return this;
    }
    public IDropTriggerNoNameIfExistsQueryBuilder IfExists()
    {
        _checkIfExists = true;
        return this;
    }
}