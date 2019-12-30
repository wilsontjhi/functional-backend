using System;
using DomainLogic.Data;
using Xunit;
using Xunit.Abstractions;

namespace DomainLogicTest
{
    public class ProductTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Product_ToString()
        {
            var product = new Product(
                "iPhone",
                new[]
                {
                    new SalesDatum {Month = 1, Amount = 100f},
                    new SalesDatum {Month = 2, Amount = 200f},
                    new SalesDatum {Month = 3, Amount = 300f},
                    new SalesDatum {Month = 4, Amount = 400f}
                });

            _testOutputHelper.WriteLine(product.ToString());
        }
    }
}