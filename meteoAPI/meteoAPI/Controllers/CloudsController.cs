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
    public class CloudsController : Controller
    {
        private readonly ICloudsService _cloudsService;

        public CloudsController(ICloudsService cloudsService)
        {
            _cloudsService = cloudsService;
        }

        [HttpGet(Name = nameof(GetAllCloudsAsync))]
        public async Task<IActionResult> GetAllCloudsAsync(CancellationToken ct)
        {
            var clouds = await _cloudsService.GetAllCloudsAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetAllCloudsAsync));
            var collection = new Collection<Clouds>
            {
                Self = collectionLink,
                Value = clouds.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{cloudsId}", Name = nameof(GetCloudsByIdAsync))]
        public async Task<IActionResult> GetCloudsByIdAsync(int cloudsId, CancellationToken ct)
        {
            var clouds = await _cloudsService.GetCloudsAsync(cloudsId, ct);
            if (clouds == null) return NotFound();

            return Ok(clouds);
        }
    }
}
