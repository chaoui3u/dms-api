using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("[controller]/")]
    public class AuthentificationController : Controller
    {
        [HttpGet("logout",Name = nameof(GetLogOut))]
        public IActionResult GetLogOut()
        {
            throw new NotImplementedException();
        }
    }
}
