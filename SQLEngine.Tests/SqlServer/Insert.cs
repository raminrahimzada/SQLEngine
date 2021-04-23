using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLEngine.SqlServer;

namespace SQLEngine.Tests.SqlServer
{
    public partial class AllTests
    {
        [TestMethod]
        public void Test_Insert_By_Value()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into("Users")
                    .Value("Name", "Ramin")
                    .Value("Surname", "Rahimzada")
                    .Value("Age", 26)
                    .Value("Height", 1.84)
                    ;
                const string query =
                    "INSERT INTO Users (Name,Surname,Age,Height) VALUES (N'Ramin' , N'Rahimzada' , 26, 1.84)";
                
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Dictionary()
        {
            using (var q = Query.New)
            {
                var dict = new Dictionary<string,AbstractSqlLiteral>
                {
                    {"Name", "Ramin"},
                    {"Surname", "Rahimzada"},
                    {"Age", 26},
                    {"Id", Guid.Empty},
                };
                q.Insert
                    .Into("Users")
                    .Values(dict)
                    ;

                const string query = "INSERT INTO Users(Name , Surname , Age, Id) VALUES (N'Ramin' , N'Rahimzada' , 26 ,'00000000-0000-0000-0000-000000000000')";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }
        [TestMethod]
        public void Test_Insert_By_Columns_And_Values()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into("Users")
                    .Columns("Name", "Surname", "Age")
                    .Values("Tracey", "McBean", 9)
                    ;

                const string query = "INSERT INTO Users(Name , Surname , Age) VALUES (N'Tracey' , N'McBean' , 9)";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Only_Values()
        {
            var datetime = DateTime.MinValue;
            var date = SqlServerLiteral.From(datetime, includeTime: false);

            using (var q = Query.New)
            {
                q.Insert
                    .Into("People")
                    .Values("Satoshi", "Nakamoto", 1024, datetime,date)
                    ;
                const string query =
                    "INSERT INTO People VALUES (N'Satoshi',N'Nakamoto',1024,'0001-01-01 00:00:00.000','0001-01-01')";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }


        [TestMethod]
        public void Test_Insert_MultipleValues_1()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into("GOT")
                    .Values("Daenerys", "Targaryen")
                    .Values("John", "Snow")
                    .Values("Illyrio", "Mopatis")
                    ;
                
                const string query =
                    @"INSERT INTO GOT VALUES (N'Daenerys',N'Targaryen'),(N'John',N'Snow'),(N'Illyrio',N'Mopatis')";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_MultipleValues_2()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into("GOT")
                    .Columns("Name","Surname")
                    .Values("Daenerys", "Targaryen")
                    .Values("John", "Snow")
                    .Values("Illyrio", "Mopatis")
                    ;

                const string query =
                    @"INSERT INTO GOT(Name,Surname) VALUES (N'Daenerys',N'Targaryen'),(N'John',N'Snow'),(N'Illyrio',N'Mopatis')";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Select()
        {
            using (var q = Query.New)
            {
                q
                    .Insert
                    .Into("Users")
                    .Values(
                        select => select.From("Users_Backup")
                    )
                    ;
                const string query = "INSERT INTO Users  SELECT  *  FROM Users_Backup";
                SqlAssert.AreEqualQuery(q.Build(), query);
            }
        }

        [TestMethod]
        public void Test_Insert_By_Select_Strong_Typed()
        {
            using (var q = Query.New)
            {
                q.Insert
                    .Into<UserTable>()
                    .Values(select =>
                        select
                            .From<AnotherUsersTable>()
                    );

                const string query = "INSERT INTO Users  SELECT  *  FROM AnotherUsers";
                SqlAssert.AreEqualQuery(q.ToString(), query);
            }
        }
    }
}
