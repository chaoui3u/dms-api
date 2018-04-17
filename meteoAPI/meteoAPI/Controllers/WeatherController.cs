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
    public class WeatherController :Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet(Name = nameof(GetWeatherAsync))]
        public async Task<IActionResult> GetWeatherAsync(CancellationToken ct)
        {
            var weather = await _weatherService.GetWeatherAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetWeatherAsync));
            var collection = new Collection<Weather>
            {
                Self = collectionLink,
                Value = weather.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{weatherId}", Name = nameof(GetWeatherByIdAsync))]
        public async Task<IActionResult> GetWeatherByIdAsync(int weatherId, CancellationToken ct)
        {
            var weather = await _weatherService.GetWeatherAsync(weatherId, ct);
            if (weather == null) return NotFound();

            return Ok(weather);
        }
    }
}
