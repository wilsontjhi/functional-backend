using System.Linq;
using DomainLogic.Data;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace DomainLogic.Estimator
{
    public static class SsaEstimator
    {
        public static float GetEstimate(ProductWithData product)
        {
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