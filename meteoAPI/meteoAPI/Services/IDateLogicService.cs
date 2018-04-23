using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IDateLogicService
    {
        Task<IEnumerable<WeatherRecord>> GetWeatherRecordsInRange(DateTimeOffset start, DateTimeOffset end,CancellationToken ct);
        bool DoesConflict( DateTimeOffset start, DateTimeOffset end);
    }
}
