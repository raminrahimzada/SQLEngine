using System.Linq;

namespace SQLEngine.SqlServer
{
    public class SqlServerColumn : AbstractSqlExpression
    {
        public string Name { get; set; }

        public SqlServerColumn(string name)
        {
            Name = name;
        }

        public override string ToSqlString()
        {
            if (!Name.All(char.IsLetterOrDigit)) return "[" + Name + "]";
            return Name;
        }
        
    }
}