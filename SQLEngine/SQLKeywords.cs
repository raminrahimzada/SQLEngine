using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SQLEngine
{
    public static class SQLKeywords
    {
        public const string DATETIME = "DATETIME";
        public const string INT = "INT";
        public const string BIGINT = "BIGINT";
        public const string NVARCHAR = "NVARCHAR";
        public const string VARCHAR = "VARCHAR";
        public const string CHAR = "CHAR";
        public const string NCHAR = "NCHAR";
        public const string DECIMAL = "DECIMAL";
        public const string BIT = "BIT";
        public const string DATE = "DATE";
        public const string TINYINT = "TINYINT";


        public const string DEFAULT_PK_OPTIONS = "PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF,IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON";
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
        public const string VALUES = "VALUES";
        public const string ALTER = "ALTER";
        public const string CREATE = "CREATE";
        public const string TABLE = "TABLE";
        public const string INSERT_INTO = "INSERT INTO";
        public const string OR = "OR";
        public const string SPACE = " ";
        public const string BEGIN_SCOPE = "(";
        public const string BEGIN_SQUARE = "[";
        public const string END_SQUARE = "]";
        public const string END_SCOPE = ")";
        public const string DECLARE = "DECLARE";
        public const string DELETE = "DELETE";
        public const string VARIABLE_HEADER = "@";
        public const string BEGIN = "BEGIN";
        public const string END = "END";
        public const string IF = "IF";
        public const string GROUPBY = "GROUP BY";
        public const string INNERJOIN = "INNER JOIN";
        public const string LEFTJOIN = "LEFT JOIN";
        public const string RIGHTJOIN = "RIGHT JOIN";
        public const string AS = "AS";
        public const string PERSISTED = "PERSISTED";
        public const string ON = "ON";
        public const string HAVING = "HAVING";
        public const string EQUALS = "=";
        public const string COMMA = ",";
        public const string SEMICOLON = ";";
        public const string UPDATE = "UPDATE";
        public const string NOTEQUALS = "<>";
        public const string LESS = "<";
        public const string LESSOREQUAL = "<=";
        public const string GREATOREQUAL = ">=";
        public const string GREAT= ">";
        public const string ELSEIF = "ELSE IF";
        public const string ELSE = "ELSE";
        public const string EXISTS = "EXISTS";
        public const string SET = "SET";
        public const string BETWEEN = "BETWEEN";
        public const string AND = "AND";
        public const string ISNULL = "IS NULL";
        public const string ISNOTNULL = "IS NOT NULL";
        public const string IN = "IN";
        public const string NOTIN = "NOT IN";
        public const string TOP = "TOP";
        public const string FROM = "FROM";
        public const string SELECT = "SELECT";
        public const string WILCARD = "*";
        public const string WHERE = "WHERE";
        public const string EXECUTE = "EXECUTE";

        public static List<string> AllKeywords { get;  }
        static SQLKeywords()
        {
            AllKeywords = GetAll().ToList();
        }
        public static IEnumerable<string> GetAll()
        {
            ArrayList constants = new ArrayList();
            var type = typeof(SQLKeywords);
            FieldInfo[] fieldInfos = type.GetFields(
                // Gets all public and static fields

                BindingFlags.Public | BindingFlags.Static |
                // This tells it to get the fields from all base types as well

                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach (FieldInfo fi in fieldInfos)
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determines if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if (fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi);

            // Return an array of FieldInfos
            var fields = (FieldInfo[]) constants.ToArray(typeof(FieldInfo));
            return fields.Select(f => f.GetValue(null)).ToArray().OfType<string>();
        }
    }
}