using System;

namespace SQLEngine.PostgreSql
{
    public static class PostgreSqlHelper 
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
