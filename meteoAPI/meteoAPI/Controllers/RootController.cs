using meteoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("/")]
    [ApiVersion("2.0")]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var Response = new RootResponse
            {
                Self= Link.To(nameof(GetRoot)),
                Signup = Link.To(nameof(AuthentificationController.RegisterUserAsync)),
                Logout = Link.To(nameof(AuthentificationController.GetLogOut)),
                Users = Link.To(nameof(AuthentificationController.GetVisibleUsersAsync)),
                Sites = Link.To(nameof(SitesController.GetSitesAsync)) ,  //new { href = Url.Link(nameof(PublicController.GetSites),null)}
                Mesures = Link.To(nameof(MesuresController.GetMesuresAsync)),
            };
            return Ok(Response);
        }

    }
}
