using DomainLogicExt.Data;
using DomainLogicExt.SalesDataInfo;
using LanguageExt;

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
                    .Map(GetTopPerformance)
                    .Map(BuildKeySummary)
                    .Match(
                        keySummary => keySummary,
                        () => "Error occur while getting the top performance"
                    );
        }

        private static string Prepare(this string productName) => productName.ToLower();
        private static Option<Product> GetProduct(this string productName) => ProductRepository.GetProduct(productName);
        private static SalesDatum GetTopPerformance(this Product product) => SalesDataInfoProvider.GetTopPerformance(product);
        private static string BuildKeySummary(this SalesDatum salesDatum)
            => $"The top performing month is {salesDatum.Month} with sales at ${salesDatum.Amount}";
    }

}