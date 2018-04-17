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
    public class MainDataController :Controller
    {
        private readonly IMainDataService _mainDataService;

        public MainDataController(IMainDataService mainDataService)
        {
            _mainDataService = mainDataService;
        }

        [HttpGet(Name = nameof(GetMainDataAsync))]
        public async Task<IActionResult> GetMainDataAsync(CancellationToken ct)
        {
            var mainData = await _mainDataService.GetMainDataAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetMainDataAsync));
            var collection = new Collection<MainData>
            {
                Self = collectionLink,
                Value = mainData.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("{mainDataId}", Name = nameof(GetMainDataByIdAsync))]
        public async Task<IActionResult> GetMainDataByIdAsync(int mainDataId, CancellationToken ct)
        {
            var mainData = await _mainDataService.GetMainDataAsync(mainDataId, ct);
            if (mainData == null) return NotFound();

            return Ok(mainData);
        }
    }
}
