using System;
using System.Collections.Generic;
using SQLEngine.Builders;
namespace SQLEngine
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using (var t = new IfElseQueryBuilder())
            {
                t.If(
                        x => x.Less("@i", "@j")
                    )
                    .Then("SET @max = @j;")
                    .ElseIf(
                        x => x.Greater("@i", "@j")
                    )
                    .Then("SET @max = @i;")
                    .ElseIf(x => x.Exists(rr => rr.From("Users")))
                    .Then("SET @max = 888")
                    .Else("SET @max = 444;");
                Console.WriteLine(t.Build());
                ;
            }
        }
    }
}