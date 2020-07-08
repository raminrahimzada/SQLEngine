using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text;

namespace SQLEngine.SqlServer
{
    public abstract class SqlServerAbstractQueryBuilder : AbstractQueryBuilder
    {

        protected static void ValidateConstraintName(ref string constraintName)
        {
            if (string.IsNullOrEmpty(constraintName)) constraintName = Guid.NewGuid().ToString().RemoveString("-");
            if (constraintName.Length >= 120)
            {
                constraintName = constraintName.RemoveString("[", "]", " ", ".dbo.");
                if (constraintName.Length >= 120)
                {
                    constraintName = constraintName.Substring(0, 120);
                }
            }
        }

        public string I(string s)
        {
            return s;
        }
    }
}
