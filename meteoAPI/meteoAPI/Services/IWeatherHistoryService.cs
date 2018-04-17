using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IWeatherHistoryService
    {
        Task<WeatherHistory> GetWeatherHistoryAsync(int id, CancellationToken ct);

        Task<IEnumerable<WeatherHistory>> GetWeatherHistoryAsync(CancellationToken ct);
    }
}
