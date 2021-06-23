using System;

using SQLEngine.SqlServer;
using Xunit;

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

        [Fact]
        public void Test_All_Literals_Conversion_AbstractSqlCondition_With_Literal()
        {
            AbstractSqlCondition condition = true;
            
            //Sql has no literal so we use bool literals like that
            Assert.Equal(C.TRUE, condition.ToSqlString());

            condition = false;
            Assert.Equal(C.FALSE, condition.ToSqlString());

            condition = (bool?) null;
            Assert.Equal(C.NULL, condition.ToSqlString());
        }

        [Fact]
        public void Test_All_Literals_Conversion_AbstractSqlLiteral()
        {
            using (var q=Query.New)
            {
                AbstractSqlLiteral literal;


                literal = 1;
                Assert.Equal(literal.ToSqlString(), "1");
                
                literal = InputOutput.Input;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = InputOutput.Output;
                Assert.Equal(literal.ToSqlString(), "2");

                literal = (uint)1;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = 1L;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = (ulong)1;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = (byte)1;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = (sbyte)1;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = (short)1;
                Assert.Equal(literal.ToSqlString(), "1");

                literal = (ushort)1;
                Assert.Equal(literal.ToSqlString(), "1");
                
                literal = 1.5D;
                Assert.Equal(literal.ToSqlString(), "1.5");

                literal = true;
                Assert.Equal(literal.ToSqlString(), "1");
                
                literal = false;
                Assert.Equal(literal.ToSqlString(), "0");

                literal = 1.5F;
                Assert.Equal(literal.ToSqlString(), "1.5");

                literal = 1.5M;
                Assert.Equal(literal.ToSqlString(), "1.5");

                literal = 'A';
                Assert.Equal(literal.ToSqlString(), "N'A'");

                literal = DateTime.Parse("01/01/2020");
                Assert.Equal(literal.ToSqlString(), "'2020-01-01 00:00:00.000'");

                literal = q.Literal(DateTime.Parse("01/01/2020"), false);
                Assert.Equal(literal.ToSqlString(), "'2020-01-01'");

                literal = q.Literal(DateTimeOffset.Parse("2021-02-06 09:10:56.777 +04:00"));
                Assert.Equal(literal.ToSqlString(), "'2021-02-06 09:10:56.777 +04:00'");

                literal = Guid.Empty;
                Assert.Equal(literal.ToSqlString(), "'00000000-0000-0000-0000-000000000000'");

                literal = "Hey";
                Assert.Equal(literal.ToSqlString(), "N'Hey'");

                literal = q.Literal("Hey");
                Assert.Equal(literal.ToSqlString(), "N'Hey'");

                literal = q.Literal("Hey", isUniCode: false);
                Assert.Equal(literal.ToSqlString(), "'Hey'");



                literal = new byte[] {0, 1, 2};
                Assert.Equal(literal.ToSqlString(), "0x000102");


                var dto = new DateTimeOffset(2021, 12, 13, 15, 30, 44, 365, TimeSpan.FromHours(4));
                literal = AbstractSqlLiteral.From(dto);
                Assert.Equal(literal.ToSqlString(), "'2021-12-13 15:30:44.365 +04:00'");

                literal = AbstractSqlLiteral.From((DateTimeOffset?) dto);
                Assert.Equal(literal.ToSqlString(), "'2021-12-13 15:30:44.365 +04:00'");


                literal = (int?) null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (byte?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (short?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (sbyte?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (ushort?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (long?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (ulong?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (char?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (string)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (Guid?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (DateTime?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);
                
                literal = (DateTimeOffset?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);
                
                
                literal = (InputOutput?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);
                
                literal = (bool?)null;
                Assert.Equal(literal.ToSqlString(), C.NULL);

                literal = (byte[])null;
                Assert.Equal(literal.ToSqlString(), C.NULL);


            }
        }
        
        [Fact]
        public void Test_All_Literals_Conversion_AbstractSqlExpression()
        {
            using (var q=Query.New)
            {
                AbstractSqlExpression expression;


                expression = 1;
                Assert.Equal(expression.ToSqlString(), "1");


                expression= true;
                Assert.Equal(expression.ToSqlString(), "1");


                expression = (uint)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = 1L;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = (ulong)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = (byte)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = (sbyte)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = (short)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = (ushort)1;
                Assert.Equal(expression.ToSqlString(), "1");

                expression = InputOutput.Input;
                Assert.Equal(expression.ToSqlString(), "1");


                expression = InputOutput.Output;
                Assert.Equal(expression.ToSqlString(), "2");




                expression = 1.5D;
                Assert.Equal(expression.ToSqlString(), "1.5");

                expression = 1.5F;
                Assert.Equal(expression.ToSqlString(), "1.5");

                expression = 1.5M;
                Assert.Equal(expression.ToSqlString(), "1.5");

                expression = 'A';
                Assert.Equal(expression.ToSqlString(), "N'A'");

                expression = DateTime.Parse("01/01/2020");
                Assert.Equal(expression.ToSqlString(), "'2020-01-01 00:00:00.000'");

                expression = q.Literal(DateTime.Parse("01/01/2020"), false);
                Assert.Equal(expression.ToSqlString(), "'2020-01-01'");

                expression = Guid.Empty;
                Assert.Equal(expression.ToSqlString(), "'00000000-0000-0000-0000-000000000000'");

                expression = "Hey";
                Assert.Equal(expression.ToSqlString(), "N'Hey'");


                expression = q.Literal("Hey", false);
                Assert.Equal(expression.ToSqlString(), "'Hey'");



                expression = new byte[] {0, 1, 2};
                Assert.Equal(expression.ToSqlString(), "0x000102");

                

                expression = (int?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (byte?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (short?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (sbyte?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (ushort?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (long?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (ulong?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (char?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (string)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (Guid?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (DateTime?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

                expression = (byte[])null;
                Assert.Equal(expression.ToSqlString(), C.NULL);
                
                expression = (InputOutput?)null;
                Assert.Equal(expression.ToSqlString(), C.NULL);


                expression = (bool?) null;
                Assert.Equal(expression.ToSqlString(), C.NULL);

            }
        }
    }
}