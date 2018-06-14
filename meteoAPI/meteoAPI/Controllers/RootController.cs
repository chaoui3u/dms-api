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
                WeatherRecord = Link.To(nameof(WeatherRecordController.GetAllWeatherRecordAsync)),
                Signup = Link.To(nameof(AuthentificationController.RegisterUserAsync)),
                Users = Link.To(nameof(AuthentificationController.GetVisibleUsersAsync)),
                Profile = Link.To(nameof(AuthentificationController.GetMyUserAsync)),
                Login = Link.To(nameof(TokenController.TokenExchangeAsync))
            };
            return Ok(Response);
        }

    }
}
