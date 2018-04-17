﻿using meteoAPI.Models;
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
    public class SitesController :Controller
    {
        private readonly ISiteService _siteService;

        public SitesController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpGet(Name = nameof(GetSitesAsync))]
        public async Task<IActionResult> GetSitesAsync(CancellationToken ct)
        {
            var sites = await _siteService.GetSitesAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetSitesAsync));
            var collection = new Collection<Site>
            {
                Self = collectionLink,
                Value = sites.ToArray()
            };
            return Ok(collection);
        }

        // /sites/{siteID}
        [HttpGet("{siteId}", Name = nameof(GetSitesByIdAsync))]
        public async Task<IActionResult> GetSitesByIdAsync(string siteId, CancellationToken ct)
        {
            var site = await _siteService.GetSiteAsync(siteId, ct);
            if (site == null) return NotFound();

            return Ok(site);
        }
    }
}
