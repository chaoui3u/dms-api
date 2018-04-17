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
    public class RainController :Controller
    {
        private readonly IRainService _rainService;

        public RainController(IRainService rainService)
        {
            _rainService = rainService;
        }

        [HttpGet(Name = nameof(GetRainAsync))]
        public async Task<IActionResult> GetRainAsync(CancellationToken ct)
        {
            var rain = await _rainService.GetRainAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetRainAsync));
            var collection = new Collection<Rain>
            {
                Self = collectionLink,
                Value = rain.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{rainId}", Name = nameof(GetRainByIdAsync))]
        public async Task<IActionResult> GetRainByIdAsync(int rainId, CancellationToken ct)
        {
            var rain = await _rainService.GetRainAsync(rainId, ct);
            if (rain == null) return NotFound();

            return Ok(rain);
        }
    }
}
