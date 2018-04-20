using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface ISunService
    {
        Task<Sun> GetSunAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Sun>> GetAllSunAsync(CancellationToken ct);
    }
}
