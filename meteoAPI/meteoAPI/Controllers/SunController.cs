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
    public class SunController :Controller
    {
        private readonly ISunService _sunService;

        public SunController(ISunService sunService)
        {
            _sunService = sunService;
        }

        [HttpGet(Name = nameof(GetAllSunAsync))]
        public async Task<IActionResult> GetAllSunAsync(CancellationToken ct)
        {
            var Sun = await _sunService.GetAllSunAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetAllSunAsync));
            var collection = new Collection<Sun>
            {
                Self = collectionLink,
                Value = Sun.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{sunId}", Name = nameof(GetSunByIdAsync))]
        public async Task<IActionResult> GetSunByIdAsync(Guid sunId, CancellationToken ct)
        {
            var sun = await _sunService.GetSunAsync(sunId, ct);
            if (sun == null) return NotFound();

            return Ok(sun);
        }
    }
}
