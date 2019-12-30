using DomainLogicExt.Data;
using DomainLogicExt.SalesDataInfo;

namespace DomainLogicExt
{
    public static class GetTopMonthWorkflow
    {
        public static string Process(string productName)
        {
            return
                productName
                    .Prepare()
                    .GetProduct()
                    .GetTopPerformance()
                    .BuildKeySummary();
        }

        private static string Prepare(this string productName) => productName.ToLower();
        private static Product GetProduct(this string productName) => ProductRepository.GetProduct(productName);
        private static SalesDatum GetTopPerformance(this Product product) => SalesDataInfoProvider.GetTopPerformance(product);
        private static string BuildKeySummary(this SalesDatum salesDatum)
            => $"The top performing month is {salesDatum.Month} with sales at ${salesDatum.Amount}";
    }

}