using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IMainDataService
    {
        Task<MainData> GetMainDataAsync(int id, CancellationToken ct);

        Task<IEnumerable<MainData>> GetMainDataAsync(CancellationToken ct);
    }
}
