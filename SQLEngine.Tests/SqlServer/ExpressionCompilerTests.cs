using System;
using System.Linq;
using SQLEngine.SqlServer;
using Xunit;

namespace SQLEngine.Tests.SqlServer;

public partial class AllTests
{
    private readonly IExpressionCompiler _compiler=new SqlExpressionCompiler();

    #region Equals

    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} = 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger == 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} = 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger == 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} = 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong == 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} = 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong == (int)3);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Short_1()
    {
        var expected = $"{nameof(UserTable.IdShort)} = 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdShort == (short)3);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Byte_1()
    {
        byte b = 3;
        var expected = $"{nameof(UserTable.IdByte)} = {b}";
        var actual = _compiler.Compile<UserTable>(x => x.IdByte == b);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Equal_Guid()
    {
        var guid = Guid.NewGuid();
        var expected = $"{nameof(UserTable.IdGuid)} = '{guid}'";
        var actual = _compiler.Compile<UserTable>(x => x.IdGuid == guid);
        Assert.Equal(expected, actual);
    }
    #endregion

    #region Not Equals

    [Fact]
    public void Test_Expression_Compiler_Simple_NotEqual_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} <> 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger != 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_NotEqual_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} <> 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger != 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_NotEqual_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} <> 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong != 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_NotEqual_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} <> 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong != (int)3);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_NotEqual_Guid()
    {
        var guid = Guid.NewGuid();
        var expected = $"{nameof(UserTable.IdGuid)} <> '{guid}'";
        var actual = _compiler.Compile<UserTable>(x => x.IdGuid != guid);
        Assert.Equal(expected, actual);
    }

    #endregion

    #region Greater

    [Fact]
    public void Test_Expression_Compiler_Simple_Greater_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} > 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger > 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Greater_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} > 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger > 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Greater_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} > 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong > 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Greater_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} > 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong > (int)3);
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Greater_Guid()
    //{
    //    var guid = Guid.NewGuid();
    //    var expected = $"{nameof(UserTable.IdGuid)} > '{guid}'";
    //    var actual = _compiler.Compile<UserTable>(x => x.IdGuid > guid);
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region GreaterEqual

    [Fact]
    public void Test_Expression_Compiler_Simple_GreaterEqual_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} >= 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger >= 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_GreaterEqual_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} >= 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger >= 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_GreaterEqual_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} >= 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong >= 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_GreaterEqual_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} >= 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong >= (int)3);
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_GreaterEqual_Guid()
    //{
    //    var guid = Guid.NewGuid();
    //    var expected = $"{nameof(UserTable.IdGuid)} >= '{guid}'";
    //    var actual = _compiler.Compile<UserTable>(x => x.IdGuid >= guid);
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region Less

    [Fact]
    public void Test_Expression_Compiler_Simple_Less_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} < 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger < 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Less_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} < 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger < 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Less_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} < 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong < 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Less_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} < 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong < (int)3);
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Less_Guid()
    //{
    //    var guid = Guid.NewGuid();
    //    var expected = $"{nameof(UserTable.IdGuid)} < '{guid}'";
    //    var actual = _compiler.Compile<UserTable>(x => x.IdGuid < guid);
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region LessEqual

    [Fact]
    public void Test_Expression_Compiler_Simple_LessEqual_Integer()
    {
        var expected = $"{nameof(UserTable.IdInteger)} <= 1";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger <= 1);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_LessEqual_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} <= 10";
        var actual = _compiler.Compile<UserTable>(x => x.IdInteger <= 10L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_LessEqual_Long()
    {
        var expected = $"{nameof(UserTable.IdLong)} <= 2";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong <= 2L);
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_LessEqual_Long_1()
    {
        var expected = $"{nameof(UserTable.IdLong)} <= 3";
        var actual = _compiler.Compile<UserTable>(x => x.IdLong <= (int)3);
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_LessEqual_Guid()
    //{
    //    var guid = Guid.NewGuid();
    //    var expected = $"{nameof(UserTable.IdGuid)} <= '{guid}'";
    //    var actual = _compiler.Compile<UserTable>(x => x.IdGuid > guid);
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region Integer Contains
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Integer_1()
    {
        var expected = $"{nameof(UserTable.IdInteger)} IN (1,2,3)";
        var arr = new [] {1, 2, 3};
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdInteger));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Integer_2()
    {
        var expected = $"CAST({nameof(UserTable.IdInteger)} AS bigint) IN (1,2,3)";
        var arr = new long[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdInteger));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Integer_3()
    {
        var expected = $"CAST({nameof(UserTable.IdInteger)} AS double) IN (1,2,3)";
        var arr = new double[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdInteger));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Integer_4()
    {
        var expected = $"CAST({nameof(UserTable.IdInteger)} AS float) IN (1,2,3)";
        var arr = new float[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdInteger));
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Contains_Integer_5()
    //{
    //    var expected = $"{nameof(UserTable.IdInteger)} IN (1,2,3)";
    //    var arr = new decimal[ { 1, 2, 3 };
    //    var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdInteger));
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region Short Contains
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Short_1()
    {
        var expected = $"CAST({nameof(UserTable.IdShort)} AS int) IN (1,2,3)";
        var arr = new [] {1, 2, 3};
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Short_1_1()
    {
        var expected = $"{nameof(UserTable.IdShort)} IN (1,2,3)";
        var arr = new short[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Short_2()
    {
        var expected = $"CAST({nameof(UserTable.IdShort)} AS bigint) IN (1,2,3)";
        var arr = new long[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Short_3()
    {
        var expected = $"CAST({nameof(UserTable.IdShort)} AS double) IN (1,2,3)";
        var arr = new double[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Short_4()
    {
        var expected = $"CAST({nameof(UserTable.IdShort)} AS float) IN (1,2,3)";
        var arr = new float[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Contains_Short_5()
    //{
    //    var expected = $"{nameof(UserTable.IdShort)} IN (1,2,3)";
    //    var arr = new decimal[ { 1, 2, 3 };
    //    var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdShort));
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region Byte Contains
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_1()
    {
        var expected = $"CAST({nameof(UserTable.IdByte)} AS int) IN (1,2,3)";
        var arr = new [] {1, 2, 3};
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_1_1()
    {
        var expected = $"{nameof(UserTable.IdByte)} IN (1,2,3)";
        var arr = new byte[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_1_3()
    {
        var expected = $"CAST({nameof(UserTable.IdByte)} AS smallint) IN (1,2,3)";
        var arr = new short[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_2()
    {
        var expected = $"CAST({nameof(UserTable.IdByte)} AS bigint) IN (1,2,3)";
        var arr = new long[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_3()
    {
        var expected = $"CAST({nameof(UserTable.IdByte)} AS double) IN (1,2,3)";
        var arr = new double[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Byte_4()
    {
        var expected = $"CAST({nameof(UserTable.IdByte)} AS float) IN (1,2,3)";
        var arr = new float[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
        Assert.Equal(expected, actual);
    }
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Contains_Byte_5()
    //{
    //    var expected = $"CAST({nameof(UserTable.IdByte)} AS double) IN (1,2,3)";
    //    var arr = new decimal[ { 1, 2, 3 };
    //    var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdByte));
    //    Assert.Equal(expected, actual);
    //}

    #endregion

    #region Long Contains
    //[Fact]
    //public void Test_Expression_Compiler_Simple_Contains_Long_1()
    //{
    //    var expected = $"{nameof(UserTable.IdLong)} IN (1,2,3)";
    //    var arr = new [ {1, 2, 3};
    //    var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdLong));
    //    Assert.Equal(expected, actual);
    //}
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Long_2()
    {
        var expected = $"{nameof(UserTable.IdLong)} IN (1,2,3)";
        var arr = new long[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdLong));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Long_3()
    {
        var expected = $"CAST({nameof(UserTable.IdLong)} AS double) IN (1,2,3)";
        var arr = new double[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdLong));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Long_4()
    {
        var expected = $"CAST({nameof(UserTable.IdLong)} AS float) IN (1,2,3)";
        var arr = new float[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdLong));
        Assert.Equal(expected, actual);
    }
    [Fact]
    public void Test_Expression_Compiler_Simple_Contains_Long_5()
    {
        var expected = $"CAST({nameof(UserTable.IdLong)} AS decimal(19,4)) IN (1,2,3)";
        var arr = new decimal[] { 1, 2, 3 };
        var actual = _compiler.Compile<UserTable>(x => arr.Contains(x.IdLong));
        Assert.Equal(expected, actual);
    }

    #endregion
}