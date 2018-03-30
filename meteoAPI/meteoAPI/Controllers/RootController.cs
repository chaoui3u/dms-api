using meteoAPI.Models;
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
            var Response = new RootResponse
            {
                Self= Link.To(nameof(GetRoot)), //Url.Link(nameof(GetRoot), null),
                Logout = Link.To(nameof(AuthentificationController.GetLogOut)), //new { href = Url.Link(nameof(AuthentificationController.GetLogOut), null)},
                Sites = Link.To(nameof(RestrictedController.GetSitesAsync)) ,  //new { href = Url.Link(nameof(PublicController.GetSites),null)}
                Mesures = Link.To(nameof(RestrictedController.GetMesuresAsync)),
            };
            return Ok(Response);
        }

    }
}
