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
    public class DefaultWeatherRecordService : IWeatherRecordService
    {
        private readonly MeteoApiContext _context;

        public DefaultWeatherRecordService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeatherRecord>> GetAllWeatherRecordAsync(CancellationToken ct)
        {
            var query = _context.WeatherRecords
                .Include(b => b.Clouds)
                .Include(b => b.Rain)
                .Include(b => b.Snow)
                .Include(b => b.Sun)
                .Include(b => b.Weather)
                .Include(b => b.Wind)
                .ProjectTo<WeatherRecord>();

            return await query.ToArrayAsync();
        }

        public async Task<WeatherRecord> GetWeatherRecordAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context.WeatherRecords.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<WeatherRecord>(entity);

        }
    }
}
