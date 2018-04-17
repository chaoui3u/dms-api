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
        Task<Weather> GetWeatherAsync(int id, CancellationToken ct);

        Task<IEnumerable<Weather>> GetWeatherAsync(CancellationToken ct);
    }
}
