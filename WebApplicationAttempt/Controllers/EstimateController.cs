using DomainLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAttempt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimateController : Controller
    {
        public IActionResult Index()
            => Ok(new { KeySummary = "The estimate for the upcoming month is $300." });

        [HttpGet("{name}")]
        public ActionResult<string> Get(string name)
            => GetEstimateWorkflowBare.Process(name);
    }
}