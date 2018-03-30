using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IMesureService
    {
        Task<Mesure> GetMesureAsync(string id, CancellationToken ct);

        Task<IEnumerable<Mesure>> GetMesuresAsync(CancellationToken ct);
    }
}
