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
    public class SnowController :Controller
    {
        private readonly ISnowService _snowService;

        public SnowController(ISnowService snowService)
        {
            _snowService = snowService;
        }

        [HttpGet(Name = nameof(GetSnowAsync))]
        public async Task<IActionResult> GetSnowAsync(CancellationToken ct)
        {
            var snow = await _snowService.GetSnowAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetSnowAsync));
            var collection = new Collection<Snow>
            {
                Self = collectionLink,
                Value = snow.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{snowId}", Name = nameof(GetSnowByIdAsync))]
        public async Task<IActionResult> GetSnowByIdAsync(int snowId, CancellationToken ct)
        {
            var snow = await _snowService.GetSnowAsync(snowId, ct);
            if (snow == null) return NotFound();

            return Ok(snow);
        }
    }
}
