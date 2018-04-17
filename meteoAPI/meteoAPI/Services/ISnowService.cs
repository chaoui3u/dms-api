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
        Task<Snow> GetSnowAsync(int id, CancellationToken ct);

        Task<IEnumerable<Snow>> GetSnowAsync(CancellationToken ct);
    }
}
