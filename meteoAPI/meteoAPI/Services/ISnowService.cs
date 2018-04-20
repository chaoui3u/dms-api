using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface ISnowService
    {
        Task<Snow> GetSnowAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Snow>> GetAllSnowAsync(CancellationToken ct);
    }
}
