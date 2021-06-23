namespace SQLEngine.SqlServer
{
    /// <summary>
    /// Represents constant strings in Sql Server 
    /// </summary>
    public static class C
    {
        //There is no sql bool literal so that is another solution
        public const string TRUE = "(1=1)";
        public const string FALSE = "(1=0)";

        public const char SPACE = ' ';
        public const char PLUS = '+';
        public const char MINUS = '-';
        public const char MULTIPLY = '*';
        public const char DIVIDE = '/';
        public const char DOT = '.';
        public const char BEGIN_SQUARE = '[';
        public const char END_SQUARE = ']';
        public const char WILCARD = '*';
        public const char EQUALS = '=';
        public const char COMMA = ',';
        public const char SEMICOLON = ';';
        public const char BEGIN_SCOPE = '(';
        public const char END_SCOPE = ')';

        public const string AND="AND";
        public const string TRIGGER = "TRIGGER";
        public const string MIN = "MIN";
        public const string AVG = "AVG";
        public const string SUM = "SUM";
        public const string COUNT = "COUNT";
        
        public const string AFTER = "AFTER";
        public const string ROLLBACK = "ROLLBACK";
        public const string TRANSACTION = "TRANSACTION";
        public const string COMMIT = "COMMIT";
        public const string TRY = "TRY";
        public const string CATCH = "CATCH";

        public const string DELETE = "DELETE";
        public const string IF = "IF";
        public const string INDEX = "INDEX";
        public const string UNIQUEIDENTIFIER = "UNIQUEIDENTIFIER";
        public const string WHILE = "WHILE";
        public const string CURSOR = "CURSOR";
        public const string FETCH = "FETCH";
        public const string NEXT = "NEXT";
        public const string FETCH_STATUS = "@@FETCH_STATUS";
        public const string UNION = "UNION";
        public const string ALL = "ALL";
        public const string FORMATMESSAGE = "FORMATMESSAGE";
        public const string RAISERROR = "RAISERROR";
        public const string NOWAIT = "NOWAIT";
        public const string LIKE = "LIKE";
        public const string ESCAPE = "ESCAPE";
        public const string CASE = "CASE";
        public const string WHEN = "WHEN";
        public const string THEN = "THEN";
        public const string OUTPUT = "OUTPUT";
        public const string NAME = "NAME";
        public const string RETURN = "RETURN";
        public const string DATETIME = "DATETIME";
        public const string DROP = "DROP";
        public const string DATABASE = "DATABASE";
        public const string INT = "INT";
        public const string BIGINT = "BIGINT";
        public const string NVARCHAR = "NVARCHAR";
        public const string NVARCHARMAX = "NVARCHAR(MAX)";
        public const string VARCHAR = "VARCHAR";
        public const string CHAR = "CHAR";
        public const string NCHAR = "NCHAR";
        public const string DECIMAL = "DECIMAL";
        public const string BIT = "BIT";
        public const string DATE = "DATE";
        public const string TINYINT = "TINYINT";
        public const string VARBINARYMAX = "VARBINARY(MAX)";
        public const string SMALLINT = "SMALLINT";

        public const string NONCLUSTERED = "NONCLUSTERED";
        public const string MAX = "MAX";
        public const string FOR = "FOR";
        public const string IDENTITY = "IDENTITY";
        public const string NOT = "NOT";
        public const string NULL = "NULL";
        public const string UNIQUE = "UNIQUE";
        public const string CHECK = "CHECK";
        public const string ADD = "ADD";
        
        public const string DESC = "DESC";
        public const string ASC = "ASC";
        public const string KEY = "KEY";
        public const string FOREIGN = "FOREIGN";
        public const string REFERENCES = "REFERENCES";
        public const string CLUSTERED = "CLUSTERED";
        public const string PRIMARY = "PRIMARY";
        public const string CONSTRAINT = "CONSTRAINT";
        public const string DEFAULT = "DEFAULT";
        public const string WITH = "WITH";
        public const string SCHEMABINDING = "SCHEMABINDING";
        public const string VALUES = "VALUES";
        public const string ALTER = "ALTER";
        public const string CREATE = "CREATE";
        public const string VIEW = "VIEW";
        public const string PROCEDURE = "PROCEDURE";
        public const string FUNCTION = "FUNCTION";
        public const string TABLE = "TABLE";
        public const string TRUNCATE = "TRUNCATE";
        public const string COLUMN = "COLUMN";
        public const string INSERT = "INSERT";
        public const string INTO = "INTO";
        public const string OR = "OR";
        public const string DISTINCT = "DISTINCT";
        public const string CLOSE = "CLOSE";
        public const string DEALLOCATE = "DEALLOCATE";
   
        public const string RETURNS = "RETURNS";
        public const string DECLARE = "DECLARE";
        public const string OPEN = "OPEN";
        public const string CAST = "CAST";
        public const string VARIABLE_HEADER = "@";
        public const string BEGIN = "BEGIN";
        public const string END = "END";
        public const string GROUP = "GROUP";
        public const string AS = "AS";
        public const string PERSISTED = "PERSISTED";
        public const string ON = "ON";
        public const string HAVING = "HAVING";


        public const string UPDATE = "UPDATE";
        public const string ELSE = "ELSE";
        public const string EXISTS = "EXISTS";
        public const string SET = "SET";
        public const string ABS = "ABS";
        public const string IS = "IS";
        public const string IN = "IN";
        public const string TOP = "TOP";
        public const string FROM = "FROM";
        public const string SELECT = "SELECT";
        
        public const string WHERE = "WHERE";
        public const string EXECUTE = "EXECUTE";
        public const string ORDER = "ORDER";
        public const string BY = "BY";
    }
}