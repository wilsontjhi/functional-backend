using System;
using System.IO;
using System.Linq;
using DomainLogic;
using Xunit;
using Xunit.Abstractions;

namespace DomainLogicTest
{
    public class DataLoaderTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DataLoaderTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            TopProductWorkflow
                .GetSalesData("singapore")
                .ToList()
                .ForEach(data => _testOutputHelper.WriteLine(data.ProductName));
        }
    }
}