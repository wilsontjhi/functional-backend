using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAttempt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricalController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok(new
            {
                SalesData = new SalesData[]
                {
                    new SalesData{Name = "Month 1", Historical = 4000f},
                    new SalesData{Name = "Month 2", Historical = 3000f},
                    new SalesData{Name = "Month 3", Historical = 2000f},
                    new SalesData{Name = "Month 4", Historical = 2780f},
                    new SalesData{Name = "Month 5", Historical = 1890f},
                    new SalesData{Name = "Month 6", Historical = 2390f}
                }
            });
        }
    }

    public class SalesData
    {
        public string Name { get; set; }
        public float? Historical { get; set; }
        public float? Estimate { get; set; }

        public SalesData()
        {
        }
    }
}