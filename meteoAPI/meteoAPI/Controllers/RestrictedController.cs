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
    public class RestrictedController :Controller
    {
        private readonly ISiteService _siteService;
        private readonly IMesureService _mesureService;

        public RestrictedController(ISiteService siteService ,IMesureService mesureService)
        {
            _siteService = siteService;
            _mesureService = mesureService;
        }

        [HttpGet("sites/", Name = nameof(GetSitesAsync))]
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
        [HttpGet("sites/{siteId}/", Name = nameof(GetSitesByIdAsync))]
        public async Task<IActionResult> GetSitesByIdAsync(string siteId, CancellationToken ct)
        {
            var site = await _siteService.GetSiteAsync(siteId, ct);
            if (site == null) return NotFound();

            return Ok(site);
        }

        [HttpGet("mesures/", Name = nameof(GetMesuresAsync))]
        public async Task<IActionResult> GetMesuresAsync(CancellationToken ct)
        {
            var mesures = await _mesureService.GetMesuresAsync(ct);

            var collectionLink = Link.ToCollection(nameof(GetMesuresAsync));
            var collection = new Collection<Mesure>
            {
                Self = collectionLink,
                Value = mesures.ToArray()
            };
            return Ok(collection);
        }

        [HttpGet("mesures/{mesureId}/", Name = nameof(GetMesuresByIdAsync))]
        public async Task<IActionResult> GetMesuresByIdAsync(string mesureId, CancellationToken ct)
        {
            var mesure = await _mesureService.GetMesureAsync(mesureId, ct);
            if (mesure == null) return NotFound();

            return Ok(mesure);
        }
    }
}
