using System.Linq;
using DomainLogicExt.Data;
using Microsoft.AspNetCore.Mvc;
using static DomainLogicExt.Data.ProductRepository;
using static DomainLogicExt.Estimator.SsaEstimator;

namespace WebApplicationAttempt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok(new
            {
                KeySummary = "The estimate for the upcoming month is $300.",
                SalesData = new []
                {
                    new SalesData{Name = "Month 1", Historical = 4000f},
                    new SalesData{Name = "Month 2", Historical = 3000f},
                    new SalesData{Name = "Month 3", Historical = 2000f},
                    new SalesData{Name = "Month 4", Historical = 2780f},
                    new SalesData{Name = "Month 5", Historical = 1890f},
                    new SalesData{Name = "Month 6", Historical = 2390f, Estimate = 2390f},
                    new SalesData{Name = "Month 7", Estimate = 3490f}
                }
            });
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var productName = name.ToLower();
            return
                GetProduct(productName)
                    .Match<IActionResult>(
                        product => Ok(BuildSummaryAndChart(product)),
                        error => NotFound()
                    );
        }

        private object BuildSummaryAndChart(Product product)
            =>
                ProductWithData
                    .Create(product)
                    .Map(GetEstimate)
                    .Match(
                        estimate => PopulateDtoWithEstimate(product, estimate),
                        error => PopulateDtoWithoutEstimate(product, error.ToString())
                    );

        private object PopulateDtoWithEstimate(Product product, float estimate)
            => new
            {
                KeySummary = $"It is estimated that next month the sales value will be ${estimate}",
                SalesData = BuildHistoricalChart(product)
                                .SkipLast()
                                .Concat(BuildEstimateChart(product, estimate))
            };

        private object PopulateDtoWithoutEstimate(Product product, string error)
            => new
            {
                KeySummary = error,
                SalesData = BuildHistoricalChart(product)
            };

        private SalesData[] BuildHistoricalChart(Product product)
            => product.SalesData
                .Select(s => new SalesData {Name = $"Month {s.Month}", Historical = s.Amount})
                .ToArray();

        private SalesData[] BuildEstimateChart(Product product, float? estimate = null)
            => new[]
                {
                    new SalesData
                    {
                        Name = $"Month {product.SalesData.Last().Month}",
                        Historical = product.SalesData.Last().Amount,
                        Estimate = product.SalesData.Last().Amount
                    },
                    new SalesData {Name = $"Next Month", Estimate = estimate},
                };
    }
}