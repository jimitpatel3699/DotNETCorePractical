using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practical16.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("HelloController Get method called.");
            return Ok("Hello, World!");
        }
    }
}
