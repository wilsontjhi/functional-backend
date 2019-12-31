using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace DomainLogicExt.Data
{
    public static class ProductRepository
    {
        public static Either<Error, Product> GetProduct(string name)
        {
            var salesData = GetSalesDataByProductName(name);
            if (salesData.Any())
            {
                return new Product(name, salesData);
            }
            return Error.New($"Product {name} is not in the database");
        }

        private static IEnumerable<SalesDatum> GetSalesDataByProductName(string productName)
        {
            using (IDbConnection db = new SqlConnection("Server=127.0.0.1,1433; Database=functional; User Id=SA; Password=yourStrong(!)Password; Connection Timeout=60"))
            {
                return db.Query<SalesDatum>($"SELECT [month], [amount] FROM [functional].[sales] WHERE [product] = '{productName}'").ToList();
            }
        }

    }

    public class SalesDatum
    {
        public int Month { get; set; }
        public float Amount { get; set; }

        public override string ToString()
            => $"{Month}: {Amount}";
    }

    public class Product
    {
        public string Name { get; }
        public IEnumerable<SalesDatum> SalesData { get; }

        public Product(string name, IEnumerable<SalesDatum> salesData)
        {
            Name = name;
            SalesData = salesData;
        }

        public override string ToString()
            => $"{Name}: {Environment.NewLine} {string.Join(',', SalesData)}";
    }

    public class ProductWithData
    {
        public string Name { get; }
        public IEnumerable<SalesDatum> SalesData { get; }

        private ProductWithData(Product product)
        {
            Name = product.Name;
            SalesData = product.SalesData;
        }

        public static Either<Error, ProductWithData> Create(Product product)
            => product.SalesData.Count() > 24
                ? (Either<Error, ProductWithData>)new ProductWithData(product)
                : Error.New($"Product {product.Name} does not have enough sales data for estimation");

        public override string ToString()
            => $"{Name}: {Environment.NewLine} {string.Join(',', SalesData)}";
    }
}