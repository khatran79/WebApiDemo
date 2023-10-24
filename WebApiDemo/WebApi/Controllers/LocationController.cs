using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IpstackOptions _options;

        public LocationController(ILogger<LocationController> logger, IOptions<IpstackOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        [HttpGet("{ip}")]
        public async Task<IActionResult> Get(string ip)
        {
            try
            {
                string url = $"{_options.ApiUrl}/{ip}?access_key={_options.AccessCode}&fields=location.capital";

                var response = await url.GetJsonAsync<IpstackResponse>();
                
                if (string.IsNullOrEmpty(response?.Location?.Capital)) return NotFound();

                return Ok(response?.Location?.Capital);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}