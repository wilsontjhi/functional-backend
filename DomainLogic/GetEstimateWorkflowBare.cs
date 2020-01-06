using DomainLogic.Data;
using DomainLogic.Estimator;
using LanguageExt;

namespace DomainLogic
{
    public static class GetEstimateWorkflowBare
    {
        public static string Process(string productName)
        {
            return
                productName
                    .Prepare()
                    .GetProduct()
                    .Convert()
                    .Map(GetEstimate)
                    .Map(BuildStatement)
                    .Match(
                        val => val,
                        () => "Something went wrong"
                    );
            
        }

        private static string Prepare(this string productName) => productName.ToLower();

        private static Product GetProduct(this string productName) => ProductRepository.GetProduct(productName);

        private static Option<ProductWithData> Convert(this Product product) => ProductWithData.Create(product);

        private static float GetEstimate(this ProductWithData product) => SsaEstimator.GetEstimate(product);

        private static string BuildStatement(this float estimate) => $"The estimate for the next month is {estimate}";
    }
}
