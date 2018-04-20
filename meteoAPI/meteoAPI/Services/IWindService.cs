using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IWindService
    {
        Task<Wind> GetWindAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Wind>> GetAllWindAsync(CancellationToken ct);
    }
}
