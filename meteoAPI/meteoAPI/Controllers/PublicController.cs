using meteoAPI.Models;
using meteoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("[controller]/")]
    public class PublicController :Controller
    {
        private readonly ISiteService _siteService;

        public PublicController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet("sites/", Name = nameof(GetSites))]
        public IActionResult GetSites()
        {
            throw new NotImplementedException();
        }

        // /sites/{siteID}
        [HttpGet("sites/{siteId}/", Name = nameof(GetSitesByIdAsync))]
        public async Task<IActionResult> GetSitesByIdAsync(string siteId, CancellationToken ct)
        {
            var site = await _siteService.GetSiteAsync(siteId, ct);
            if (site == null) return NotFound();

            return Ok(site);
        }
    }
}
