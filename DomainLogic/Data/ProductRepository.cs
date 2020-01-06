using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DomainLogic.Data
{
    public static class ProductRepository
    {
        public static Product GetProduct(string name)
            => new Product(name, GetSalesDataByProductName(name));

        private static IEnumerable<SalesDatum> GetSalesDataByProductName(string productName)
        {
            using (IDbConnection db = new SqlConnection("Server= localhost\\sqlexpress; Database= functional; Integrated Security=True;"))
            {
                return db.Query<SalesDatum>($"SELECT [month], [amount] FROM [sales] WHERE [product] = '{productName}'").ToList();
            }
        }
    }
}