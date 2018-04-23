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
        private readonly IDateLogicService _dateLogicService;

        public WeatherRecordController(IWeatherRecordService weatherRecordService,
            IDateLogicService dateLogicService)
        {
            _weatherRecordService = weatherRecordService;
            _dateLogicService = dateLogicService;
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

        [HttpGet("{start}/{end}",Name = nameof(GetRangeWeatherRecordsAsync))]
        public async Task<IActionResult> GetRangeWeatherRecordsAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken ct)
        {
            if (_dateLogicService.DoesConflict(start, end)) return NotFound();
            var weatherRecords = await _dateLogicService.GetWeatherRecordsInRange(start, end,ct);

            var collectionLink = Link.ToCollection(nameof(GetRangeWeatherRecordsAsync));
            var collection = new Collection<WeatherRecord>
            {
                Self = collectionLink,
                Value = weatherRecords.ToArray()
            };
            return Ok(collection);
        }
    }
}
