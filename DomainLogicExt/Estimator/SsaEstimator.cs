using System.Linq;
using DomainLogicExt.Data;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace DomainLogicExt.Estimator
{
    public static class SsaEstimator
    {
        public static float GetEstimate(Product product)
        {
            return 100f;
            MLContext mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromEnumerable(product.SalesData);

            var estimate =
                mlContext.Forecasting.ForecastBySsa(
                        outputColumnName: nameof(SalesEstimate.ForecastAmount),
                        inputColumnName: nameof(SalesDatum.Amount),
                        windowSize: 3,
                        seriesLength: 24,
                        trainSize: 24,
                        horizon: 1)
                    .Fit(dataView)
                    .CreateTimeSeriesEngine<SalesDatum, SalesEstimate>(mlContext)
                    .Predict();

            return estimate.ForecastAmount.First();
        }
    }

    public class SalesEstimate
    {
        public float[] ForecastAmount { get; set; }
    }
}