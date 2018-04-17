using meteoAPI.Models;
using meteoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/")]
    public class WeatherHistoryController : Controller
    {
        private readonly IWeatherHistoryService _weatherHistoryService;

        public WeatherHistoryController(IWeatherHistoryService weatherHistoryService)
        {
            _weatherHistoryService = weatherHistoryService;
        }

        [HttpGet(Name = nameof(GetWeatherHistoryAsync))]
        public async Task<IActionResult> GetWeatherHistoryAsync(CancellationToken ct)
        {
            var weatherHistory = await _weatherHistoryService.GetWeatherHistoryAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetWeatherHistoryAsync));
            var collection = new Collection<WeatherHistory>
            {
                Self = collectionLink,
                Value = weatherHistory.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{weatherHistoryId}", Name = nameof(GetWeatherHistoryByIdAsync))]
        public async Task<IActionResult> GetWeatherHistoryByIdAsync(int weatherHistoryId, CancellationToken ct)
        {
            var weatherHistory = await _weatherHistoryService.GetWeatherHistoryAsync(weatherHistoryId, ct);
            if (weatherHistory == null) return NotFound();

            return Ok(weatherHistory);
        }
    }
}
