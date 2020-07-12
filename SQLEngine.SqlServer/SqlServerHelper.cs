using System;

namespace SQLEngine.SqlServer
{
    public static class SqlServerHelper 
    {
        public static void ValidateConstraintName(ref string constraintName)
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
    }
}
