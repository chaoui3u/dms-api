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
    public class DefaultWeatherRecordService : IWeatherRecordService ,IDateLogicService
    {
        private readonly MeteoApiContext _context;

        public DefaultWeatherRecordService(MeteoApiContext context)
        {
            _context = context;
        }

        public bool DoesConflict(DateTimeOffset start, DateTimeOffset end)
        {
            return (start == end || start > end);
        }

        public async Task<IEnumerable<WeatherRecord>> GetAllWeatherRecordAsync(CancellationToken ct)
        {
            var query = _context.WeatherRecords
                .Include(b => b.MainData)
                .Include(b => b.Wind)
                .OrderBy(b => b.CurrentTime)
                .ProjectTo<WeatherRecord>();

            return await query.ToArrayAsync();
        }

        public async Task<WeatherRecord> GetWeatherRecordAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context.WeatherRecords.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<WeatherRecord>(entity);

        }

        public async Task<IEnumerable<WeatherRecord>> GetWeatherRecordsInRange(DateTimeOffset start, DateTimeOffset end,CancellationToken ct)
        {

            var query = _context.WeatherRecords
                .Include(b=> b.MainData)
               .Include(b => b.Wind)
               .Where(b => (b.CurrentTime >= start && b.CurrentTime <= end))
               .OrderBy(b => b.CurrentTime)
               .ProjectTo<WeatherRecord>();

            return await query.ToArrayAsync();
        }

       
    }
}
