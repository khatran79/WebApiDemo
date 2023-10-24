using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;
        
        public CandidateController(ILogger<CandidateController> logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = new Candidate
            {
                Name = "test",
                Phone = "test"
            };

            return Ok(data);
        }
    }
}