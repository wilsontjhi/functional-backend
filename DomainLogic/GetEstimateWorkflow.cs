using DomainLogic.Data;
using DomainLogic.Estimator;

namespace DomainLogic
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

        public static string Prepare(this string productName) => productName.ToLower();
        public static Product GetProduct(this string productName) => ProductRepository.GetProduct(productName);
        public static float GetEstimate(this Product product) => SsaEstimator.GetEstimate(product);
        public static string BuildKeySummary(this float estimate)
            => $"It is estimated that next month the sales value will be ${estimate}";
    }

}