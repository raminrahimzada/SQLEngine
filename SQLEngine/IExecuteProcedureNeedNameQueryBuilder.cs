namespace SQLEngine
{
    public interface IExecuteProcedureNeedNameQueryBuilder : IExecuteProcedureQueryBuilder
    {
        IExecuteProcedureNeedArgQueryBuilder Name(string procedureName, bool useScoping = false);
    }
}