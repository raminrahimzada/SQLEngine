using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        //Demo Enum
        public enum InputOutput
        {
            Input=1,
            Output=2
        }

        [TestMethod]
        public void Test_All_Literals_Conversion_AbstractSqlCondition_With_Literal()
        {
            AbstractSqlCondition condition = true;
            
            //Sql has no literal so we use bool literals like that
            Assert.AreEqual(C.TRUE, condition.ToSqlString());

            condition = false;
            Assert.AreEqual(C.FALSE, condition.ToSqlString());

            condition = (bool?) null;
            Assert.AreEqual(C.NULL, condition.ToSqlString());
        }

        [TestMethod]
        public void Test_All_Literals_Conversion_AbstractSqlLiteral()
        {
            using (var q=Query.New)
            {
                AbstractSqlLiteral literal;


                literal = 1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                
                literal = InputOutput.Input;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = InputOutput.Output;
                Assert.AreEqual(literal.ToSqlString(), "2");



                literal = (uint)1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = 1L;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = (ulong)1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = (byte)1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = (sbyte)1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = (short)1;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = (ushort)1;
                Assert.AreEqual(literal.ToSqlString(), "1");





                literal = 1.5D;
                Assert.AreEqual(literal.ToSqlString(), "1.5");

                literal = true;
                Assert.AreEqual(literal.ToSqlString(), "1");

                literal = 1.5F;
                Assert.AreEqual(literal.ToSqlString(), "1.5");

                literal = 1.5M;
                Assert.AreEqual(literal.ToSqlString(), "1.5");

                literal = 'A';
                Assert.AreEqual(literal.ToSqlString(), "N'A'");

                literal = DateTime.Parse("01/01/2020");
                Assert.AreEqual(literal.ToSqlString(), "'2020-01-01 00:00:00.000'");

                literal = q.Literal(DateTime.Parse("01/01/2020"), false);
                Assert.AreEqual(literal.ToSqlString(), "'2020-01-01'");

                literal = q.Literal(DateTimeOffset.Parse("2021-02-06 09:10:56.777 +04:00"));
                Assert.AreEqual(literal.ToSqlString(), "'2021-02-06 09:10:56.777 +04:00'");

                literal = Guid.Empty;
                Assert.AreEqual(literal.ToSqlString(), "'00000000-0000-0000-0000-000000000000'");

                literal = "Hey";
                Assert.AreEqual(literal.ToSqlString(), "N'Hey'");


                literal = q.Literal("Hey", false);
                Assert.AreEqual(literal.ToSqlString(), "'Hey'");



                literal = new byte[] {0, 1, 2};
                Assert.AreEqual(literal.ToSqlString(), "0x000102");

                
                literal = (int?) null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (byte?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (short?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (sbyte?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (ushort?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (long?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (ulong?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (char?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (string)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (Guid?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (DateTime?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);
                
                
                literal = (InputOutput?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);
                
                literal = (bool?)null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);

                literal = (byte[])null;
                Assert.AreEqual(literal.ToSqlString(), C.NULL);
            }
        }
        
        [TestMethod]
        public void Test_All_Literals_Conversion_AbstractSqlExpression()
        {
            using (var q=Query.New)
            {
                AbstractSqlExpression expression;


                expression = 1;
                Assert.AreEqual(expression.ToSqlString(), "1");


                expression= true;
                Assert.AreEqual(expression.ToSqlString(), "1");


                expression = (uint)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = 1L;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = (ulong)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = (byte)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = (sbyte)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = (short)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = (ushort)1;
                Assert.AreEqual(expression.ToSqlString(), "1");

                expression = InputOutput.Input;
                Assert.AreEqual(expression.ToSqlString(), "1");


                expression = InputOutput.Output;
                Assert.AreEqual(expression.ToSqlString(), "2");




                expression = 1.5D;
                Assert.AreEqual(expression.ToSqlString(), "1.5");

                expression = 1.5F;
                Assert.AreEqual(expression.ToSqlString(), "1.5");

                expression = 1.5M;
                Assert.AreEqual(expression.ToSqlString(), "1.5");

                expression = 'A';
                Assert.AreEqual(expression.ToSqlString(), "N'A'");

                expression = DateTime.Parse("01/01/2020");
                Assert.AreEqual(expression.ToSqlString(), "'2020-01-01 00:00:00.000'");

                expression = q.Literal(DateTime.Parse("01/01/2020"), false);
                Assert.AreEqual(expression.ToSqlString(), "'2020-01-01'");

                expression = Guid.Empty;
                Assert.AreEqual(expression.ToSqlString(), "'00000000-0000-0000-0000-000000000000'");

                expression = "Hey";
                Assert.AreEqual(expression.ToSqlString(), "N'Hey'");


                expression = q.Literal("Hey", false);
                Assert.AreEqual(expression.ToSqlString(), "'Hey'");



                expression = new byte[] {0, 1, 2};
                Assert.AreEqual(expression.ToSqlString(), "0x000102");

                

                expression = (int?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (byte?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (short?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (sbyte?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (ushort?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (long?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (ulong?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (char?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (string)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (Guid?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (DateTime?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

                expression = (byte[])null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);
                
                expression = (InputOutput?)null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);


                expression = (bool?) null;
                Assert.AreEqual(expression.ToSqlString(), C.NULL);

            }
        }
    }
}