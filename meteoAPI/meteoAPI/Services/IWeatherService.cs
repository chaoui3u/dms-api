using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<Weather>> GetAllWeatherAsync(CancellationToken ct);
    }
}
