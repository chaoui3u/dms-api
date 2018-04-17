using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IRainService
    {
        Task<Rain> GetRainAsync(int id, CancellationToken ct);

        Task<IEnumerable<Rain>> GetRainAsync(CancellationToken ct);
    }
}
