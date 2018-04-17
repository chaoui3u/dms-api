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
    public class MesuresController : Controller
    {
        private readonly IMesureService _mesureService;

        public MesuresController(IMesureService mesureService)
        {
            _mesureService = mesureService;
        }

        [HttpGet(Name = nameof(GetMesuresAsync))]
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

        [HttpGet("{mesureId}", Name = nameof(GetMesuresByIdAsync))]
        public async Task<IActionResult> GetMesuresByIdAsync(string mesureId, CancellationToken ct)
        {
            var mesure = await _mesureService.GetMesureAsync(mesureId, ct);
            if (mesure == null) return NotFound();

            return Ok(mesure);
        }
    }
}
