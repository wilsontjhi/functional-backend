using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAttempt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimateController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok(new { KeySummary = "The estimate for the upcoming month is $300." });
        }
    }
}