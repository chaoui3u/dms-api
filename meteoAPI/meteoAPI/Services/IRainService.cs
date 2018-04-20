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
        Task<Rain> GetRainAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Rain>> GetAllRainAsync(CancellationToken ct);
    }
}
