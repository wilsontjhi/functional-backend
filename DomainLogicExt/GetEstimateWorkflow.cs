using DomainLogicExt.Data;
using DomainLogicExt.Estimator;

namespace DomainLogicExt
{
    public static class GetEstimateWorkflow
    {
        public static string Process(string productName)
        {
            return
                productName
                    .Prepare()
                    .GetProduct()
                    .GetEstimate()
                    .BuildKeySummary();
        }

        private static string Prepare(this string productName) => productName.ToLower();
        private static Product GetProduct(this string productName) => ProductRepository.GetProduct(productName);
        private static float GetEstimate(this Product product) => SsaEstimator.GetEstimate(product);
        private static string BuildKeySummary(this float estimate)
            => $"It is estimated that next month the sales value will be ${estimate}";
    }

}