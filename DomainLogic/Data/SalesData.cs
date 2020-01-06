using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace DomainLogic.Data
{
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

        public static Option<ProductWithData> Create(Product product)
            => product.SalesData.Count() > 24
                ? (Option<ProductWithData>)new ProductWithData(product)
                : None;

        public override string ToString()
            => $"{Name}: {Environment.NewLine} {string.Join(',', SalesData)}";
    }
}
