using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogV4.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Home()
        {
            return Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/swagger");
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            var response = new { Environment.MachineName };
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}
