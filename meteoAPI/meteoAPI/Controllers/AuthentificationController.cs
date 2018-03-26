using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("/[controller]/logout")]
    public class AuthentificationController : Controller
    {
        [HttpGet(Name = nameof(GetLogOut))]
        public IActionResult GetLogOut()
        {
            throw new NotImplementedException();
        }
    }
}
