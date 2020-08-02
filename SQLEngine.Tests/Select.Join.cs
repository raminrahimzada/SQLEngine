using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public partial class Test_Query_Builder_Sql_Server
    {

        [TestMethod]
        public void Test_Select_Simple_Join_With_Raw_Condition()
        {
            using (var q = Query.New)
            {
                var col1 = q.Column("CustomerId", "O");
                var col2 = q.Column("Id", "C");

                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .InnerJoin("Orders", "O")
                    .On(col1 == col2)
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Select_Simple_Join_1()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .InnerJoin("Orders", "O")
                            .OnColumn("CustomerId","O")
                            .IsEqualsTo("Id","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_Simple_Join_2()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin("Orders", "O")
                            .OnColumn("CustomerId", "O")
                            .IsEqualsTo("Id", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Simple_Join_3()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                        .OnColumn("CustomerId","O")
                        .IsEqualsTo("Id","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_Simple_Join_4()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                        .OnColumn("PartnerId", "C")
                        .IsEqualsTo("PartnerId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON C.PartnerId = O.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_Simple_Join_5()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Multiple_Join_1()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .InnerJoin("Orders", "O")
                            .OnColumn("PartnerId","O")
                            .IsEqualsTo("PartnerId","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.PartnerId = C.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Multiple_Join_11()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .InnerJoin("Orders", "O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("PartnerId", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	INNER JOIN Orders AS O ON O.PartnerId = C.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Multiple_Join_2()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                            .OnColumn("PartnerId", "O")
                            .IsEqualsTo("CustomerPartnerId", "C")
                    .InnerJoin<UserTable>("U")
                        .OnColumn("Id", "U")
                        .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    INNER JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    INNER JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Select_Multiple_Join_3()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    .InnerJoin<UserTable>("U")
                    .OnColumn("Id", "U")
                    .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    INNER JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    INNER JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Select_Multiple_Join_4()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .InnerJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    .InnerJoin<UserTable>("U")
                    .OnColumn("Id")
                    .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    INNER JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    INNER JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Simple_Join_With_Raw_Condition()
        {
            using (var q = Query.New)
            {
                var col1 = q.Column("CustomerId", "O");
                var col2 = q.Column("Id", "C");

                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .LeftJoin("Orders", "O")
                    .On(col1 == col2)
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


        [TestMethod]
        public void Test_Left_Select_Simple_Join_1()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .LeftJoin("Orders", "O")
                            .OnColumn("CustomerId","O")
                            .IsEqualsTo("Id","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Left_Select_Simple_Join_2()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin("Orders", "O")
                            .OnColumn("CustomerId", "O")
                            .IsEqualsTo("Id", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Simple_Join_3()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                        .OnColumn("CustomerId","O")
                        .IsEqualsTo("Id","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.CustomerId = C.Id

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Left_Select_Simple_Join_4()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                        .OnColumn("PartnerId", "C")
                        .IsEqualsTo("PartnerId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON C.PartnerId = O.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Left_Select_Simple_Join_5()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Multiple_Join_1()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .LeftJoin("Orders", "O")
                            .OnColumn("PartnerId","O")
                            .IsEqualsTo("PartnerId","C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.PartnerId = C.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Multiple_Join_11()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From("Customers", "C")
                    .LeftJoin("Orders", "O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("PartnerId", "C")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	Left JOIN Orders AS O ON O.PartnerId = C.PartnerId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Multiple_Join_2()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                            .OnColumn("PartnerId", "O")
                            .IsEqualsTo("CustomerPartnerId", "C")
                    .LeftJoin<UserTable>("U")
                        .OnColumn("Id", "U")
                        .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    Left JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    Left JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Left_Select_Multiple_Join_3()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    .LeftJoin<UserTable>("U")
                    .OnColumn("Id", "U")
                    .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    Left JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    Left JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
        [TestMethod]
        public void Test_Left_Select_Multiple_Join_4()
        {
            using (var q = Query.New)
            {
                q
                    .Select
                    .Top(1)
                    .From<CustomerTable>("C")
                    .LeftJoin<OrderTable>("O")
                    .OnColumn("PartnerId")
                    .IsEqualsTo("CustomerPartnerId", "C")
                    .LeftJoin<UserTable>("U")
                    .OnColumn("Id")
                    .IsEqualsTo("ExecutorUserId", "O")
                    ;

                const string query = @" 
SELECT TOP(1)   * 
    FROM Customers AS C
	    Left JOIN Orders AS O ON O.PartnerId = C.CustomerPartnerId
	    Left JOIN Users AS U ON U.Id = O.ExecutorUserId

";

                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }


       
    }
}