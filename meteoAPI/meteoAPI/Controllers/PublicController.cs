using meteoAPI.Models;
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
        private readonly MeteoApiContext _context;

        public PublicController(MeteoApiContext context)
        {
            _context = context;
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
            var entity = await _context.Sites.SingleOrDefaultAsync(s => s.Id == siteId, ct);
            if (entity == null) return NotFound();

            var resource = new Site
            {
                Href = Url.Link(nameof(GetSitesByIdAsync), new { siteId = entity.Id }),
                Refrence = entity.Refrence,
                Label = entity.Label,
                Latitude = entity.Latitude,
                Logitude = entity.Logitude,
                Type = entity.Type,
                Classification = entity.Classification,
                Area = entity.Area
            };

            return Ok(resource);
        }
    }
}
