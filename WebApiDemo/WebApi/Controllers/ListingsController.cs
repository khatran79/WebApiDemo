using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection;
using WebApi.Contracts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        private readonly JayrideOptions _options;

        public ListingsController(ILogger<ListingsController> logger, IOptions<JayrideOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        [HttpGet("{numPassengers}")]
        public async Task<IActionResult> Get(int numPassengers)
        {
            try
            {
                string url = $"{_options.ApiUrl}/QuoteRequest";
                var result = await url.GetJsonAsync<QuoteRequest>();

                var filter = result.Listings
                    .Where(listing => listing.VehicleType.MaxPassengers >= numPassengers)
                    .Select(listing =>
                    {
                        listing.TotalPrice = listing.PricePerPassenger * numPassengers;
                        return listing;
                    })
                    .OrderBy(listing => listing.TotalPrice)
                    .ToList();

                return Ok(filter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}