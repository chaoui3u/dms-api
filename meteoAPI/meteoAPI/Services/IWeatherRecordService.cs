using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IWeatherRecordService
    {
        Task<WeatherRecord> GetWeatherRecordAsync(Guid id, CancellationToken ct);

        Task<IEnumerable<WeatherRecord>> GetAllWeatherRecordAsync(CancellationToken ct);
    }
}
