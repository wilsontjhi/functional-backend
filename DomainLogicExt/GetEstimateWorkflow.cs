using DomainLogicExt.Data;
using DomainLogicExt.Estimator;
using LanguageExt;

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
                    .Bind(ValidProduct)
                    .Map(GetEstimate)
                    .Map(BuildKeySummary)
                    .Match(
                        keySummary => keySummary,
                        () => "Error while getting estimate"
                        );
        }

        private static string Prepare(this string productName) => productName.ToLower();
        private static Option<Product> GetProduct(this string productName) => ProductRepository.GetProduct(productName);
        private static Option<ProductWithData> ValidProduct(this Product product) => ProductWithData.Create(product);
        private static float GetEstimate(this ProductWithData product) => SsaEstimator.GetEstimate(product);
        private static string BuildKeySummary(this float estimate)
            => $"It is estimated that next month the sales value will be ${estimate}";
    }
}