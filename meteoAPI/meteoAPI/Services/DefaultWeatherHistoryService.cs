using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace meteoAPI.Services
{
    public class DefaultWeatherHistoryService : IWeatherHistoryService
    {
        private readonly MeteoApiContext _context;

        public DefaultWeatherHistoryService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<WeatherHistory> GetWeatherHistoryAsync(int id, CancellationToken ct)
        {
            var entity = await _context.WeatherHistory.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<WeatherHistory>(entity);

        }

        public async Task<IEnumerable<WeatherHistory>> GetAllWeatherHistoryAsync(CancellationToken ct)
        {
            var query = _context.WeatherHistory.ProjectTo<WeatherHistory>();

            return await query.ToArrayAsync();
        }
    }
}
