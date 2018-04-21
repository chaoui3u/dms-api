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
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/")]
    public class WeatherRecordController : Controller
    {
        private readonly IWeatherRecordService _weatherRecordService;

        public WeatherRecordController(IWeatherRecordService weatherRecordService)
        {
            _weatherRecordService = weatherRecordService;
        }

        [HttpGet(Name = nameof(GetAllWeatherRecordAsync))]
        public async Task<IActionResult> GetAllWeatherRecordAsync(CancellationToken ct)
        {
            var weatherRecord = await _weatherRecordService.GetAllWeatherRecordAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetAllWeatherRecordAsync));
            var collection = new Collection<WeatherRecord>
            {
                Self = collectionLink,
                Value = weatherRecord.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{weatherRecordId}", Name = nameof(GetWeatherRecordByIdAsync))]
        public async Task<IActionResult> GetWeatherRecordByIdAsync(Guid weatherRecordId, CancellationToken ct)
        {
            var weatherRecord = await _weatherRecordService.GetWeatherRecordAsync(weatherRecordId, ct);
            if (weatherRecord == null) return NotFound();

            return Ok(weatherRecord);
        }
    }
}
