using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface ICloudsService
    {
        Task<Clouds> GetCloudsAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Clouds>> GetAllCloudsAsync(CancellationToken ct);
    }
}
