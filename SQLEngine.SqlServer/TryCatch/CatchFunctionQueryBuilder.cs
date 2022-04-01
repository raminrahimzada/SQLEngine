namespace SQLEngine.SqlServer;

public class CatchFunctionQueryBuilder : SqlServerQueryBuilder, ICatchFunctionQueryBuilder
{
    public ISqlExpression ErrorNumber()
    {
        return new SqlServerRawExpression("ERROR_NUMBER()");
    }

    public ISqlExpression ErrorMessage()
    {
        return new SqlServerRawExpression("ERROR_MESSAGE()");
    }
}