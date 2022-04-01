using System;

using SQLEngine.SqlServer;
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    //Demo Enum
    public enum InputOutput
    {
        Input = 1,
        Output = 2
    }

    [Fact]
    public void Test_NewLine_After_Literal_Select()
    {
        using(var q = Query.New)
        {
            var ok1 = q.DeclareNew<bool>(false);
            var ok2 = q.DeclareNew<bool>(false);
            q.Select.Select(ok1);
            q.Delete.Top(1).Table("Users");
            const string expected = @"
DECLARE  @v1 BIT  = 0;
DECLARE  @v2 BIT  = 0;
SELECT @v1
DELETE TOP(1)   FROM Users
";
            var actual = q.Build();
            SqlAssert.EqualQuery(actual, expected);
        }
    }
    [Fact]
    public void Test_All_Literals_Conversion_AbstractSqlCondition_With_Literal()
    {
        AbstractSqlCondition condition = true;

        //Sql has no literal so we use bool literals like that
        Assert.Equal(C.TRUE, condition.ToSqlString());

        condition = false;
        Assert.Equal(C.FALSE, condition.ToSqlString());

        condition = (bool?)null;
        Assert.Equal(C.NULL, condition.ToSqlString());
    }

    [Fact]
    public void Test_All_Literals_Conversion_AbstractSqlLiteral()
    {
        using(var q = Query.New)
        {
            AbstractSqlLiteral literal;


            literal = 1;
            Assert.Equal("1", literal.ToSqlString());

            literal = InputOutput.Input;
            Assert.Equal("1", literal.ToSqlString());

            literal = InputOutput.Output;
            Assert.Equal("2", literal.ToSqlString());

            literal = (uint)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = 1L;
            Assert.Equal("1", literal.ToSqlString());

            literal = (ulong)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = (byte)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = (sbyte)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = (short)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = (ushort)1;
            Assert.Equal("1", literal.ToSqlString());

            literal = 1.5D;
            Assert.Equal("1.5", literal.ToSqlString());

            literal = true;
            Assert.Equal("1", literal.ToSqlString());

            literal = false;
            Assert.Equal("0", literal.ToSqlString());

            literal = 1.5F;
            Assert.Equal("1.5", literal.ToSqlString());

            literal = 1.5M;
            Assert.Equal("1.5", literal.ToSqlString());

            literal = 'A';
            Assert.Equal("N'A'", literal.ToSqlString());

            literal = DateTime.Parse("01/01/2020");
            Assert.Equal("'2020-01-01 00:00:00.000'", literal.ToSqlString());

            literal = q.Literal(DateTime.Parse("01/01/2020"), false);
            Assert.Equal("'2020-01-01'", literal.ToSqlString());

            literal = q.Literal(DateTimeOffset.Parse("2021-02-06 09:10:56.777 +04:00"));
            Assert.Equal("'2021-02-06 09:10:56.777 +04:00'", literal.ToSqlString());

            literal = Guid.Empty;
            Assert.Equal("'00000000-0000-0000-0000-000000000000'", literal.ToSqlString());

            literal = "Hey";
            Assert.Equal("N'Hey'", literal.ToSqlString());

            literal = q.Literal("Hey");
            Assert.Equal("N'Hey'", literal.ToSqlString());

            literal = q.Literal("Hey", isUniCode: false);
            Assert.Equal("'Hey'", literal.ToSqlString());



            literal = new byte[] { 0, 1, 2 };
            Assert.Equal("0x000102", literal.ToSqlString());


            var dto = new DateTimeOffset(2021, 12, 13, 15, 30, 44, 365, TimeSpan.FromHours(4));
            literal = AbstractSqlLiteral.From(dto);
            Assert.Equal("'2021-12-13 15:30:44.365 +04:00'", literal.ToSqlString());

            literal = AbstractSqlLiteral.From((DateTimeOffset?)dto);
            Assert.Equal("'2021-12-13 15:30:44.365 +04:00'", literal.ToSqlString());


            literal = (int?)null;
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


            literal = (bool?)null;
            Assert.Equal(literal.ToSqlString(), C.NULL);

            literal = (byte[])null;
            Assert.Equal(literal.ToSqlString(), C.NULL);

        }
    }

    [Fact]
    public void Test_All_Literals_Conversion_AbstractSqlExpression()
    {
        using(var q = Query.New)
        {
            AbstractSqlExpression expression;


            expression = 1;
            Assert.Equal("1", expression.ToSqlString());


            expression = true;
            Assert.Equal("1", expression.ToSqlString());


            expression = (uint)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = 1L;
            Assert.Equal("1", expression.ToSqlString());

            expression = (ulong)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = (byte)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = (sbyte)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = (short)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = (ushort)1;
            Assert.Equal("1", expression.ToSqlString());

            expression = InputOutput.Input;
            Assert.Equal("1", expression.ToSqlString());


            expression = InputOutput.Output;
            Assert.Equal("2", expression.ToSqlString());




            expression = 1.5D;
            Assert.Equal("1.5", expression.ToSqlString());

            expression = 1.5F;
            Assert.Equal("1.5", expression.ToSqlString());

            expression = 1.5M;
            Assert.Equal("1.5", expression.ToSqlString());

            expression = 'A';
            Assert.Equal("N'A'", expression.ToSqlString());

            expression = DateTime.Parse("01/01/2020");
            Assert.Equal("'2020-01-01 00:00:00.000'", expression.ToSqlString());

            expression = q.Literal(DateTime.Parse("01/01/2020"), false);
            Assert.Equal("'2020-01-01'", expression.ToSqlString());

            expression = Guid.Empty;
            Assert.Equal("'00000000-0000-0000-0000-000000000000'", expression.ToSqlString());

            expression = "Hey";
            Assert.Equal("N'Hey'", expression.ToSqlString());


            expression = q.Literal("Hey", false);
            Assert.Equal("'Hey'", expression.ToSqlString());



            expression = new byte[] { 0, 1, 2 };
            Assert.Equal("0x000102", expression.ToSqlString());



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

            expression = (bool?)null;
            Assert.Equal(expression.ToSqlString(), C.NULL);
        }
    }
}