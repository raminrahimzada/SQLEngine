﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SQLEngine.Tests
{
    public static class QueryAssert
    {
        public static void AreEqual(string query1, string query2)
        {
            const string splitter = " \r\n\t;()";
            string[] FormatQuery(string query)
            {
                return query
                    .Split(splitter.ToCharArray())
                    .Select(s=>s.ToLower())
                    .Select(s => s.Trim(splitter.ToCharArray()))
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();
            }

            var arr1 = FormatQuery(query1);
            var arr2 = FormatQuery(query2);
            CollectionAssert.AreEqual(arr1, arr2);
        }
    }
}