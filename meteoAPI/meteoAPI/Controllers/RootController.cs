using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("/")]
    [ApiVersion("1.0")]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var Response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                logout = new { href = Url.Link(nameof(AuthentificationController.GetLogOut), null)},
                sites = new { href = Url.Link(nameof(SitesController.GetSites),null)}
            };
            return Ok(Response);
        }

    }
}
