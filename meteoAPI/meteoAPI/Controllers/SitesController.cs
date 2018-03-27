using meteoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("/public/[controller]")]
    public class SitesController : Controller
    {
        private readonly Sites _sites;

        public SitesController(IOptions<Sites> sitesAccesor)
        {
            _sites = sitesAccesor.Value;
        }

        [HttpGet(Name = nameof(GetSites))]
        public IActionResult GetSites()
        {
            _sites.Href = Url.Link(nameof(GetSites), null);

            return Ok(_sites);
        }
    }
}
