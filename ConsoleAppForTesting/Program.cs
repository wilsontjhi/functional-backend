using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace ConsoleAppForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GetProductSales("product1")
                .ToList()
                .ForEach(Console.WriteLine);
        }

        static IEnumerable<SalesDao> GetProductSales(string productName)
        {
            using (IDbConnection db = new SqlConnection("Server=127.0.0.1,1433; Database=functional; User Id=SA; Password=yourStrong(!)Password; Connection Timeout=60"))
            {
                return db.Query<SalesDao>($"SELECT * FROM [functional].[sales] WHERE [product] = '{productName}'").ToList();
            }
        }
    }

    public class SalesDao
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public string Product { get; set; }
        public Single Amount { get; set; }

        public override string ToString()
            => $"{Id} - {Product}: {Month} - {Amount}";
    }

    public class Sales
    {

    }
}