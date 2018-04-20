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
    public class WindController : Controller
    {
        private readonly IWindService _windService;

        public WindController(IWindService windService)
        {
            _windService = windService;
        }

        [HttpGet(Name = nameof(GetAllWindAsync))]
        public async Task<IActionResult> GetAllWindAsync(CancellationToken ct)
        {
            var wind = await _windService.GetAllWindAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetAllWindAsync));
            var collection = new Collection<Wind>
            {
                Self = collectionLink,
                Value = wind.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{windId}", Name = nameof(GetWindByIdAsync))]
        public async Task<IActionResult> GetWindByIdAsync(int windId, CancellationToken ct)
        {
            var wind = await _windService.GetWindAsync(windId, ct);
            if (wind == null) return NotFound();

            return Ok(wind);
        }
    }
}
